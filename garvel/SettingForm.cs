using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using gravel;
using Newtonsoft.Json.Linq;

namespace garvel
{
    public partial class SettingForm : MetroFramework.Forms.MetroForm
    {
        Counter counter;
        MainForm mainform;
        Information infoForm;
        public delegate void ApplyPicboxHandler();
        public event ApplyPicboxHandler ApplyPicboxEvent;
        public bool initflag = true;
        public SettingForm(Counter cobj,MainForm form)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            counter = cobj;
            mainform = form;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            string[] filters = { "No filter", "GaussianBlur", "Blur" ,"BoxFilter", "MedianBlur", "BilateralFilter" };
            comboBox_Filter.Items.AddRange(filters);
            string[] borders = { "Reflect101", "Constant", "Replicate", "Reflect", "Isolated" };
            comboBox_Border.Items.AddRange(borders);
            string[] retrievals = { "Tree", "External", "List", "CComp" }; 
            comboBox_Retrieval.Items.AddRange(retrievals);
            string[] approxis = { "ApproxTC89KCOS", "ApproxNone", "ApproxSimple", "ApproxTC89L1" };
            comboBox_approximation.Items.AddRange(approxis);
            string[] lineTypes = { "AntiAlias", "Link4", "Link8"};
            comboBox_lineType.Items.AddRange(lineTypes);

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainform.Location.X + mainform.Size.Width -75, mainform.Location.Y + 40);
            tb_setname.MaxLength = 30;
            tb_imgSizeWidth.MaxLength = 7;
            tb_imgSizeHeight.MaxLength = 7;
            loadSet();
            initflag = false;
            scrollBar_opacity.Value = 95;
            this.Opacity = 0.95;
        }

        public void loadSet()
        {
            if (mainform.comboBox.Items.Count != 0)
                tb_setname.Text = mainform.comboBox.SelectedItem.ToString();

            checkBox_custom.Checked = (bool)counter.preSet["customSize"];
            tb_imgSizeWidth.Text = (string)counter.preSet["customWidth"];
            tb_imgSizeHeight.Text = (string)counter.preSet["customHeight"];
            tb_bri.Text = (string)counter.preSet["brightness"];
            scrollBar_bir.Value = (int)counter.preSet["brightness"];
            comboBox_Filter.SelectedIndex = (int)counter.preSet["filter"];
            tb_FilterValue.Text = (string)counter.preSet["filterValue"];
            trackbar_filter.Value = (int)counter.preSet["filterValue"];
            comboBox_Border.SelectedIndex = (int)counter.preSet["border"];
            tb_rotate.Text = (string)counter.preSet["rotate"];
            scrollBar_rotate.Value = (int)counter.preSet["rotate"];

            tb_threshold.Text = (string)counter.procSet["threshold"];
            scrollBar_threshold.Value = (int)counter.procSet["threshold"];
            comboBox_approximation.SelectedIndex = (int)counter.procSet["approxi"];
            comboBox_Retrieval.SelectedIndex = (int)counter.procSet["retrieval"];
            tb_areaMin.Text = (string)counter.procSet["areaMin"];
            scrollBar_Min.Value = (int)counter.procSet["areaMin"];
            tb_areaMax.Text = (string)counter.procSet["areaMax"];
            scrollBar_Max.Value = (int)counter.procSet["areaMax"];

            pb_countArea.BackColor = Color.FromArgb((int)counter.postSet["countArea"]["R"], (int)counter.postSet["countArea"]["G"], (int)counter.postSet["countArea"]["B"]);
            pb_exclusion.BackColor = Color.FromArgb((int)counter.postSet["exclusionArea"]["R"], (int)counter.postSet["exclusionArea"]["G"], (int)counter.postSet["exclusionArea"]["B"]);
            tb_countT.Text = (string)counter.postSet["countArea"]["thickness"];
            trackBar_countT.Value = (int)counter.postSet["countArea"]["thickness"];
            tb_exT.Text = (string)counter.postSet["exclusionArea"]["thickness"];
            trackBar_exT.Value = (int)counter.postSet["exclusionArea"]["thickness"];
            checkBox_label.Checked = (bool)counter.postSet["label"]["enable"];
            comboBox_lineType.SelectedIndex = (int)counter.postSet["label"]["lineType"];
            pb_labelcolor.BackColor = Color.FromArgb((int)counter.postSet["label"]["R"], (int)counter.postSet["label"]["G"], (int)counter.postSet["label"]["B"]);
            tb_scaleValue.Text = (string)counter.postSet["label"]["scale"];
            scrollBar_scale.Value = (int)counter.postSet["label"]["scale"];
            tb_decValue.Text = (string)counter.postSet["label"]["decimal"];
            trackBar_dec.Value = (int)counter.postSet["label"]["decimal"];
            checkBox_bound.Checked = (bool)counter.postSet["bound"]["enable"];
            pb_bound.BackColor = Color.FromArgb((int)counter.postSet["bound"]["R"], (int)counter.postSet["bound"]["G"], (int)counter.postSet["bound"]["B"]);

            checkBox_original.Checked = (bool)counter.exportSet["original"];
            checkBox_marked.Checked = (bool)counter.exportSet["marked"];
            checkBox_csv.Checked = (bool)counter.exportSet["csv"];
        }

        private void comboBox_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = comboBox_Filter.SelectedIndex;
            if (selIndex != 0)
            {
                tb_FilterValue.Visible = true;
                trackbar_filter.Visible = true;
                tb_border.Visible = true;
                comboBox_Border.Visible = true;
            }
            else
            {
                tb_FilterValue.Visible = false;
                trackbar_filter.Visible = false;
                tb_border.Visible = false;
                comboBox_Border.Visible = false;
            }
            counter.preSet["filter"] = selIndex;
            ApplyPicboxEvent();
        }
        private void textbox_helper(JToken jt,string kind,TextBox tb, MetroFramework.Controls.MetroScrollBar sb, MetroFramework.Controls.MetroTrackBar trackbar = null)
        {
            int value;
            int min, max;
            if (trackbar != null)
            {
                min = trackbar.Minimum;
                max = trackbar.Maximum;
            }
            else
            {
                min = sb.Minimum;
                max = sb.Maximum;
            }
            
            try
            {
                value = int.Parse(tb.Text);
                if (max < value) value = max;
                else if (min > value) value = min;
            }
            catch
            {
                value = min;
            }
            tb.Text = value.ToString();
            if (trackbar != null) trackbar.Value = value;
            else sb.Value = value;
            
            jt[kind] = value;
            ApplyPicboxEvent();
        }

        private void tb_setname_TextChanged(object sender, EventArgs e)
        {
            tb_setname.Text = Regex.Replace(tb_setname.Text, @"[\\/:*?""<>|]", "", RegexOptions.Singleline); //파일이름으로 불가한 문자 제거
            tb_currSetName.Text = tb_setname.Text;
        }

        private void scrollBar_threshold_Scroll(object sender, ScrollEventArgs e)
        {
            tb_threshold.Text = scrollBar_threshold.Value.ToString();
            textbox_helper(counter.procSet, "threshold", tb_threshold, scrollBar_threshold);
        }

        private void scrollBar_bir_Scroll(object sender, ScrollEventArgs e)
        {
            tb_bri.Text = scrollBar_bir.Value.ToString();
            textbox_helper(counter.preSet, "brightness", tb_bri, scrollBar_bir);
        }

        private void tb_threshold_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.procSet, "threshold", tb_threshold, scrollBar_threshold);
            }
        }

        private void tb_bri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.preSet, "brightness", tb_bri, scrollBar_bir);
            }
        }

        private void tb_FilterValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.preSet, "filterValue", tb_FilterValue, null , trackbar_filter);
            }
        }

        private void trackbar_filter_Scroll(object sender, ScrollEventArgs e)
        {
            tb_FilterValue.Text = trackbar_filter.Value.ToString();
            textbox_helper(counter.preSet, "filterValue", tb_FilterValue, null, trackbar_filter);
        }

        private void comboBox_Border_SelectedIndexChanged(object sender, EventArgs e)
        {
            counter.preSet["border"] = comboBox_Border.SelectedIndex;
            ApplyPicboxEvent();
        }

        private void comboBox_approximation_SelectedIndexChanged(object sender, EventArgs e)
        {
            counter.procSet["approxi"] = comboBox_approximation.SelectedIndex;
            ApplyPicboxEvent();
        }

        private void comboBox_Retrieval_SelectedIndexChanged(object sender, EventArgs e)
        {
            counter.procSet["retrieval"] = comboBox_Retrieval.SelectedIndex;
            ApplyPicboxEvent();
        }

        private void tb_areaMin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.procSet,"areaMin", tb_areaMin, scrollBar_Min);
            }
        }

        private void scrollBar_Min_Scroll(object sender, ScrollEventArgs e)
        {
            tb_areaMin.Text = scrollBar_Min.Value.ToString();
            textbox_helper(counter.procSet,"areaMin", tb_areaMin, scrollBar_Min);
        }

        private void scrollBar_Max_Scroll(object sender, ScrollEventArgs e)
        {
            tb_areaMax.Text = scrollBar_Max.Value.ToString();
            textbox_helper(counter.procSet,"areaMax", tb_areaMax, scrollBar_Max);
        }

        private void tb_areaMax_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.procSet, "areaMax", tb_areaMax, scrollBar_Max);
            }
        }

        private void pb_countArea_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pb_countArea.BackColor = colorDialog1.Color;
                counter.postSet["countArea"]["R"] = colorDialog1.Color.R;
                counter.postSet["countArea"]["G"] = colorDialog1.Color.G;
                counter.postSet["countArea"]["B"] = colorDialog1.Color.B;
                ApplyPicboxEvent();
            }
        }

        private void pb_exclusion_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pb_exclusion.BackColor = colorDialog1.Color;
                counter.postSet["exclusionArea"]["R"] = colorDialog1.Color.R;
                counter.postSet["exclusionArea"]["G"] = colorDialog1.Color.G;
                counter.postSet["exclusionArea"]["B"] = colorDialog1.Color.B;
                ApplyPicboxEvent();
            }
        }

        private void tb_countT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.postSet["countArea"], "thickness", tb_countT, null, trackBar_countT);
            }

        }

        private void trackBar_countT_Scroll(object sender, ScrollEventArgs e)
        {
            tb_countT.Text = trackBar_countT.Value.ToString();
            textbox_helper(counter.postSet["countArea"], "thickness", tb_countT, null, trackBar_countT);
        }

        private void tb_exT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.postSet["exclusionArea"], "thickness", tb_exT, null, trackBar_exT);
            }
        }
        private void trackBar_exT_Scroll(object sender, ScrollEventArgs e)
        {
            tb_exT.Text = trackBar_exT.Value.ToString();
            textbox_helper(counter.postSet["exclusionArea"], "thickness", tb_exT, null, trackBar_exT);
        }

        private void bt_setDefault_Click(object sender, EventArgs e)
        {
            counter.changeMainset();
            ApplyPicboxEvent();
            loadSet();
        }

        private void bt_saveSet_Click(object sender, EventArgs e)
        {
            string fileFullPath = mainform.setDir + "\\" + tb_setname.Text + ".json";
            FileInfo fInfo = new FileInfo(fileFullPath);
            if (fInfo.Exists)
            {
                if (MessageBox.Show(tb_setname.Text + " exists. Overwrite?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return; //No 클릭
                }
            }
            File.WriteAllText(fileFullPath, counter.getMainJS().ToString());
            MessageBox.Show(tb_setname.Text + " saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mainform.SettingListView(mainform.GetCurrentDirecotry(), tb_setname.Text);
        }

        private void bt_deleteSet_Click(object sender, EventArgs e)
        {
            string fileFullPath = mainform.setDir + "\\" + tb_setname.Text + ".json";
            FileInfo fInfo = new FileInfo(fileFullPath);
            if (!fInfo.Exists)
                MessageBox.Show(tb_setname.Text + " does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (MessageBox.Show(tb_setname.Text + " Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Delete(fileFullPath);
                    mainform.SettingListView(mainform.GetCurrentDirecotry());
                }

            }

        }

        private void checkBox_label_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_label.Checked)
            {
                tb_lineType.Visible = true;
                comboBox_lineType.Visible = true;
                pb_labelcolor.Visible = true;
                tb_scale.Visible = true;
                tb_scaleValue.Visible = true;
                scrollBar_scale.Visible = true;
                tb_labelColor.Visible = true;
                tb_decimals.Visible = true;
                tb_decValue.Visible = true;
                trackBar_dec.Visible = true;
            }

            else
            {
                tb_lineType.Visible = false;
                comboBox_lineType.Visible = false;
                pb_labelcolor.Visible = false;
                tb_scale.Visible = false;
                tb_scaleValue.Visible = false;
                scrollBar_scale.Visible = false;
                tb_labelColor.Visible = false;
                tb_decimals.Visible = false;
                tb_decValue.Visible = false;
                trackBar_dec.Visible = false;
            }

            counter.postSet["label"]["enable"] = checkBox_label.Checked;
            ApplyPicboxEvent();
        }

        private void comboBox_lineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            counter.postSet["label"]["lineType"] = comboBox_lineType.SelectedIndex;
            ApplyPicboxEvent();
        }

        private void pb_labelcolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pb_labelcolor.BackColor = colorDialog1.Color;
                counter.postSet["label"]["R"] = colorDialog1.Color.R;
                counter.postSet["label"]["G"] = colorDialog1.Color.G;
                counter.postSet["label"]["B"] = colorDialog1.Color.B;
                ApplyPicboxEvent();
            }
        }

        private void tb_scaleValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.postSet["label"], "scale", tb_scaleValue, scrollBar_scale);
            }
        }

        private void scrollBar_scale_Scroll(object sender, ScrollEventArgs e)
        {
            tb_scaleValue.Text = scrollBar_scale.Value.ToString();
            textbox_helper(counter.postSet["label"], "scale", tb_scaleValue, scrollBar_scale);
        }

        private void checkBox_bound_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_bound.Checked)
            {
                tb_boundColor.Visible = true;
                pb_bound.Visible = true;
            }
            else
            {
                tb_boundColor.Visible = false;
                pb_bound.Visible = false;
            }

            counter.postSet["bound"]["enable"] = checkBox_bound.Checked;
            ApplyPicboxEvent();
        }

        private void pb_bound_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pb_bound.BackColor = colorDialog1.Color;
                counter.postSet["bound"]["R"] = colorDialog1.Color.R;
                counter.postSet["bound"]["G"] = colorDialog1.Color.G;
                counter.postSet["bound"]["B"] = colorDialog1.Color.B;
                ApplyPicboxEvent();
            }
        }

        private void tb_decValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.postSet["label"], "decimal", tb_decValue, null, trackBar_dec);
            }
        }

        private void trackBar_dec_Scroll(object sender, ScrollEventArgs e)
        {
            tb_decValue.Text = trackBar_dec.Value.ToString();
            textbox_helper(counter.postSet["label"], "decimal", tb_decValue, null, trackBar_dec);
        }

        private void tb_rotate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbox_helper(counter.preSet, "rotate", tb_rotate, scrollBar_rotate);
            }
        }

        private void scrollBar_rotate_Scroll(object sender, ScrollEventArgs e)
        {
            tb_rotate.Text = scrollBar_rotate.Value.ToString();
            textbox_helper(counter.preSet, "rotate", tb_rotate, scrollBar_rotate);
        }

        private void SettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            initflag = true;
        }

        private void checkBox_original_CheckedChanged(object sender, EventArgs e)
        {
            counter.exportSet["original"] = checkBox_original.Checked;
        }

        private void checkBox_marked_CheckedChanged(object sender, EventArgs e)
        {
            counter.exportSet["marked"] = checkBox_marked.Checked;
        }

        private void checkBox_csv_CheckedChanged(object sender, EventArgs e)
        {
            counter.exportSet["csv"] = checkBox_csv.Checked;
        }

        private void tb_imgSizeWidth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    counter.preSet["customWidth"] = int.Parse(tb_imgSizeWidth.Text);
                    counter.preSet["customHeight"] = int.Parse(tb_imgSizeHeight.Text);
                    ApplyPicboxEvent();
                }
                catch
                {
                    tb_imgSizeWidth.Text = "0";
                }
            }
                

        }

        private void tb_imgSizeHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    counter.preSet["customWidth"] = int.Parse(tb_imgSizeWidth.Text);
                    counter.preSet["customHeight"] = int.Parse(tb_imgSizeHeight.Text);
                    ApplyPicboxEvent();
                }
                catch
                {
                    tb_imgSizeHeight.Text = "0";
                }
            }
        }

        private void checkBox_actual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_custom.Checked)
            {
                tb_imgSize.Visible = true;
                tb_imgSizeWidth.Visible = true;
                tb_X.Visible = true;
                tb_imgSizeHeight.Visible = true;
                tb_mm.Visible = true;
            }
            else
            {
                tb_imgSize.Visible = false;
                tb_imgSizeWidth.Visible = false;
                tb_X.Visible = false;
                tb_imgSizeHeight.Visible = false;
                tb_mm.Visible = false;
            }

            counter.preSet["customSize"] = checkBox_custom.Checked;
            ApplyPicboxEvent();
        }

        private void scrollBar_opacity_Scroll(object sender, ScrollEventArgs e)
        {
            this.Opacity = ((double)(scrollBar_opacity.Value) / 100.0);
            this.Refresh();
        }

        private void bt_info_Click(object sender, EventArgs e)
        {
            infoForm = new Information();
            infoForm.Show();
        }
    }
}
