using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;




namespace garvel
{
    public partial class MainForm : Form
    {
        public Counter counter = new Counter();
        SettingForm settingForm;
        public Picture picture = new Picture();
        bool mouseIsDown = false, isDraging = false;
        List<string> files;
        int index;
        public static string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Core image analyzer";
        public string setDir = rootDir + @"\settings";
        public string imgPath = "C:\\";
        IniFile config;
        int rotate = 0;

        public MainForm()
        {
            InitializeComponent();
            loadInitSettings();
            SettingListView(GetCurrentDirecotry());
            settingFormLoad();
            if (comboBox.Items.Count != 0)
                loadJSfile(comboBox.SelectedItem.ToString());
            this.MinimumSize = new System.Drawing.Size(600, 650);
            this.AllowDrop = true;
            mainPictureBox.AllowDrop = true;
            pb_view.AllowDrop = true;
        }

        public FileInfo[] GetCurrentDirecotry()
        {
            if (!Directory.Exists(setDir))
            {
                System.IO.Directory.CreateDirectory(setDir);
                File.WriteAllText(setDir + @"\default.json", counter.getMainJS().ToString());
            }
            DirectoryInfo di = new DirectoryInfo(setDir);
            FileInfo[] files = di.GetFiles().OrderBy(p => p.CreationTime).Reverse().ToArray();

            return files;
        }

        public void loadJSfile(string setname)
        {
            using (StreamReader r = new StreamReader(setDir+@"\"+setname+".json"))
            {
                string setjs = r.ReadToEnd();
                counter.changeMainset(setjs);
            }
        }
        
        private void loadInitSettings()
        {
            string configPath = rootDir + @"\config.ini";
            if (!new FileInfo(configPath).Exists)
            {
                System.IO.Directory.CreateDirectory(rootDir);
                File.WriteAllText(configPath, string.Empty);
            }
            config = new IniFile(configPath);

            imgPath = config.Read("imgPath", "Path");
            string savePath = config.Read("savePath", "Path");
            if(savePath.Length == 0 || imgPath.Length == 0)
            {
                File.WriteAllText(configPath, string.Empty);
                config.Write("imgPath", "C:\\", "Path");
                config.Write("savePath", "C:\\", "Path");
                imgPath = config.Read("imgPath", "Path");
                savePath = config.Read("savePath", "Path");
            } 
            tb_savePath.Text = savePath;
        }

        public void SettingListView(FileInfo[] files,string name = null)
        {
            if (files.Length == 0)
                return;
            comboBox.BeginUpdate();
            comboBox.Items.Clear();
            foreach (var fi in files)
            {
                comboBox.Items.Add(fi.Name.Split(new string[] {"."}, StringSplitOptions.None)[0]);
            }

            comboBox.EndUpdate();
            if (name == null)
                comboBox.SelectedIndex = 0;
            else
                comboBox.SelectedItem = name;
            this.Refresh();
        }

        public void settingFormLoad()
        {
            settingForm = new SettingForm(counter, this);
            settingForm.ApplyPicboxEvent += new SettingForm.ApplyPicboxHandler(applyPicbox);
        }

        public void applyPicbox()
        {
            if (counter.markedImg == null || (settingForm.initflag == true))
                return;
            rotate = (int)counter.preSet["rotate"];
            counter.markedImg = counter.proc();
            tx_count.Text = counter.currCellCnt.ToString();
            mainPictureBox.Refresh();
            setMainInfo();
        }
        public void applyPicbox(bool mainflag = false)
        {
            if (counter.markedImg == null || mainflag == false)
                return;
            rotate = (int)counter.preSet["rotate"];
            counter.markedImg = counter.proc();
            tx_count.Text = counter.currCellCnt.ToString();
            mainPictureBox.Refresh();
            setMainInfo();
        }

        public void setMainInfo()
        {
            tb_imginfo.Text = "\r\n" + counter.imgName + "\r\n\r\n" + counter.width.ToString() +
                "x" + counter.height.ToString() + " " + (int)counter.dpiX + "x" + (int)counter.dpiY + " DPI\r\n" +
                string.Format("{0:F2}", counter.mmX) + "x" + string.Format("{0:F2}", counter.mmY) + " mm\r\n" +
                "1pixel = " + string.Format("{0:F4}", counter.pixelSize) + " mm²";
        }
        private void loadImg()
        {
            bool re = counter.loadImgFile();
            if (!re)
                return;

            counter.markedImg = counter.Mat2bm(counter.oriImg);
            picture.Fit(counter.markedImg, ref mainPictureBox);
            this.mainPictureBox.Paint += new PaintEventHandler(mainImage_Paint);
            mainPictureBox.Image = null;
            applyPicbox(true);
            tb_view.Visible = false;
            pb_view.Visible = false;
        }

        private void bt_selectImg_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                counter.imgPath = file[0];
                loadImg();
            }
        }

        private void bt_selectImg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
        }

        private void bt_selectImg_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = imgPath;
                openFileDialog.Filter = "All files (*.*)|*.*|Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    imgPath = filePath;
                    config.Write("imgPath", imgPath, "Path");
                    counter.imgPath = filePath;
                    loadImg();
                }
            }
            
        }

        private void bt_Settings_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms["SettingForm"];
            if (form != null)
                return;
            settingFormLoad();
            settingForm.Show();
        }

        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;

            MouseEventArgs mouse = e as MouseEventArgs;

            picture.InitialiseMousePosition(mouse.X, mouse.Y);
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;

            if (isDraging)
                isDraging = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (counter.markedImg == null)
                return;

                MouseEventArgs mouse = e as MouseEventArgs;

                picture.Zoom(counter.markedImg, ref mainPictureBox, mouse);
                mainPictureBox.Refresh();          
        }

        public void mainImage_Paint(object sender, PaintEventArgs e)
        {
            e.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.TranslateTransform((float)mainPictureBox.Width / 2, (float)mainPictureBox.Height / 2);
            e.Graphics.RotateTransform(rotate);
            e.Graphics.TranslateTransform(-(float)mainPictureBox.Width / 2, -(float)mainPictureBox.Height / 2);
            e.Graphics.DrawImage(counter.markedImg, picture.X, picture.Y, picture.Width, picture.Height); //System.OverflowException 
            
        }

        private void mainPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < mainPictureBox.Width / 2 && isDraging == false)
            {
                if (files != null)
                {
                    if (index > 0)
                    {
                        index -= 1;
                        counter.markedImg = Image.FromFile(files.ElementAt(index));
                    }
                    else if (index == 0)
                    {
                        index = files.Count - 1;
                        counter.markedImg = Image.FromFile(files.ElementAt(index));
                    }

                    picture.Fit(counter.markedImg, ref mainPictureBox);
                    mainPictureBox.Refresh();
                }
            }
            else if (e.X > mainPictureBox.Width / 2 && isDraging == false)
            {
                if (files != null)
                {
                    if (index < files.Count - 1)
                    {
                        index += 1;
                        counter.markedImg = Image.FromFile(files.ElementAt(index));
                    }
                    else if (index == files.Count - 1)
                    {
                        index = 0;
                        counter.markedImg = Image.FromFile(files.ElementAt(index));
                    }
                    picture.Fit(counter.markedImg, ref mainPictureBox);
                    mainPictureBox.Refresh();
                }
            }
        }

        private void MainForm_Resize_1(object sender, EventArgs e)
        {
            if (counter.markedImg == null)
                return;

            picture.Fit(counter.markedImg, ref mainPictureBox);
            mainPictureBox.Refresh();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadJSfile(comboBox.SelectedItem.ToString());
            var form = Application.OpenForms["SettingForm"];
            if (form != null)
                settingForm.loadSet();
            applyPicbox(true);
        }

        private void bt_savePath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_savePath.Text = fbd.SelectedPath;
                }
            }
        }

        private void bt_saveResult_Click(object sender, EventArgs e)
        {
            if (counter.markedImg == null) 
            {
            MessageBox.Show("No results. Start analyzing first.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            }
            string savePath = tb_savePath.Text; 
            bool re = counter.makeResults(savePath);
            if (re)
            {
                config.Write("savePath", savePath, "Path");
                MessageBox.Show("Results saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mainPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                counter.imgPath = file[0];
                loadImg();
            }
        }

        private void mainPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pb_view_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                counter.imgPath = file[0];
                loadImg();
            }
        }

        private void pb_view_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left && mouseIsDown)
            {
                isDraging = true;
                picture.Drag(counter.markedImg, mouse, ref mainPictureBox);
                mainPictureBox.Refresh();
            }
        }
    }
}
