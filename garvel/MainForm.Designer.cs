
namespace garvel
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tx_count = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.bt_Settings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bt_saveResult = new System.Windows.Forms.Button();
            this.tb_savePath = new System.Windows.Forms.TextBox();
            this.bt_savePath = new System.Windows.Forms.Button();
            this.tb_imginfo = new System.Windows.Forms.TextBox();
            this.bt_selectImg = new System.Windows.Forms.Button();
            this.laytb = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pb_view = new System.Windows.Forms.PictureBox();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.tb_view = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.laytb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.tx_count, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBox4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBox5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.bt_Settings, 3, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 262);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.18882F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.82883F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.91892F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.54054F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(268, 134);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tx_count
            // 
            this.tx_count.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tx_count.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tx_count.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel3.SetColumnSpan(this.tx_count, 4);
            this.tx_count.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tx_count.Font = new System.Drawing.Font("나눔바른고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tx_count.ForeColor = System.Drawing.Color.White;
            this.tx_count.Location = new System.Drawing.Point(39, 81);
            this.tx_count.Margin = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.tx_count.Name = "tx_count";
            this.tx_count.ReadOnly = true;
            this.tx_count.Size = new System.Drawing.Size(194, 37);
            this.tx_count.TabIndex = 4;
            this.tx_count.TabStop = false;
            this.tx_count.Text = "0";
            this.tx_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel3.SetColumnSpan(this.textBox4, 4);
            this.textBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox4.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(96, 57);
            this.textBox4.Margin = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ShortcutsEnabled = false;
            this.textBox4.Size = new System.Drawing.Size(80, 19);
            this.textBox4.TabIndex = 5;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "Count";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel3.SetColumnSpan(this.textBox5, 4);
            this.textBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox5.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox5.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox5.Location = new System.Drawing.Point(7, 2);
            this.textBox5.Margin = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ShortcutsEnabled = false;
            this.textBox5.Size = new System.Drawing.Size(174, 16);
            this.textBox5.TabIndex = 2;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "Select Setting";
            // 
            // comboBox
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.comboBox, 3);
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(2, 19);
            this.comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(196, 22);
            this.comboBox.TabIndex = 2;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // bt_Settings
            // 
            this.bt_Settings.AutoSize = true;
            this.bt_Settings.Dock = System.Windows.Forms.DockStyle.Top;
            this.bt_Settings.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_Settings.Location = new System.Drawing.Point(203, 18);
            this.bt_Settings.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.bt_Settings.Name = "bt_Settings";
            this.bt_Settings.Size = new System.Drawing.Size(63, 24);
            this.bt_Settings.TabIndex = 3;
            this.bt_Settings.Text = "Settings";
            this.bt_Settings.UseVisualStyleBackColor = true;
            this.bt_Settings.Click += new System.EventHandler(this.bt_Settings_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.laytb.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_saveResult, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_savePath, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.bt_savePath, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 467);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.45455F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.06593F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.68132F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 91);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox2.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(7, 2);
            this.textBox2.Margin = new System.Windows.Forms.Padding(7, 2, 2, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ShortcutsEnabled = false;
            this.textBox2.Size = new System.Drawing.Size(169, 16);
            this.textBox2.TabIndex = 7;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Destination Folder";
            // 
            // bt_saveResult
            // 
            this.bt_saveResult.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableLayoutPanel1.SetColumnSpan(this.bt_saveResult, 2);
            this.bt_saveResult.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_saveResult.Location = new System.Drawing.Point(45, 53);
            this.bt_saveResult.Margin = new System.Windows.Forms.Padding(2);
            this.bt_saveResult.Name = "bt_saveResult";
            this.tableLayoutPanel1.SetRowSpan(this.bt_saveResult, 2);
            this.bt_saveResult.Size = new System.Drawing.Size(177, 36);
            this.bt_saveResult.TabIndex = 7;
            this.bt_saveResult.Text = "Save results";
            this.bt_saveResult.UseVisualStyleBackColor = true;
            this.bt_saveResult.Click += new System.EventHandler(this.bt_saveResult_Click);
            // 
            // tb_savePath
            // 
            this.tb_savePath.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_savePath.Location = new System.Drawing.Point(2, 20);
            this.tb_savePath.Margin = new System.Windows.Forms.Padding(2);
            this.tb_savePath.Name = "tb_savePath";
            this.tb_savePath.Size = new System.Drawing.Size(197, 21);
            this.tb_savePath.TabIndex = 5;
            // 
            // bt_savePath
            // 
            this.bt_savePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.bt_savePath.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_savePath.Location = new System.Drawing.Point(203, 20);
            this.bt_savePath.Margin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.bt_savePath.Name = "bt_savePath";
            this.bt_savePath.Size = new System.Drawing.Size(65, 24);
            this.bt_savePath.TabIndex = 6;
            this.bt_savePath.Text = "Browse..";
            this.bt_savePath.UseVisualStyleBackColor = true;
            this.bt_savePath.Click += new System.EventHandler(this.bt_savePath_Click);
            // 
            // tb_imginfo
            // 
            this.tb_imginfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tb_imginfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_imginfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_imginfo.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_imginfo.ForeColor = System.Drawing.Color.White;
            this.tb_imginfo.Location = new System.Drawing.Point(7, 137);
            this.tb_imginfo.Margin = new System.Windows.Forms.Padding(7);
            this.tb_imginfo.Multiline = true;
            this.tb_imginfo.Name = "tb_imginfo";
            this.tb_imginfo.ReadOnly = true;
            this.tb_imginfo.Size = new System.Drawing.Size(258, 116);
            this.tb_imginfo.TabIndex = 0;
            this.tb_imginfo.TabStop = false;
            this.tb_imginfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bt_selectImg
            // 
            this.bt_selectImg.AllowDrop = true;
            this.bt_selectImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bt_selectImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bt_selectImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_selectImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_selectImg.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bt_selectImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_selectImg.Font = new System.Drawing.Font("나눔바른고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_selectImg.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bt_selectImg.Location = new System.Drawing.Point(7, 7);
            this.bt_selectImg.Margin = new System.Windows.Forms.Padding(7);
            this.bt_selectImg.Name = "bt_selectImg";
            this.bt_selectImg.Size = new System.Drawing.Size(258, 116);
            this.bt_selectImg.TabIndex = 9;
            this.bt_selectImg.Text = "Open image file..";
            this.bt_selectImg.UseVisualStyleBackColor = false;
            this.bt_selectImg.Click += new System.EventHandler(this.bt_selectImg_Click);
            this.bt_selectImg.DragDrop += new System.Windows.Forms.DragEventHandler(this.bt_selectImg_DragDrop);
            this.bt_selectImg.DragEnter += new System.Windows.Forms.DragEventHandler(this.bt_selectImg_DragEnter);
            // 
            // laytb
            // 
            this.laytb.AllowDrop = true;
            this.laytb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.laytb.ColumnCount = 1;
            this.laytb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.laytb.Controls.Add(this.pictureBox2, 0, 5);
            this.laytb.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.laytb.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.laytb.Controls.Add(this.tb_imginfo, 0, 1);
            this.laytb.Controls.Add(this.bt_selectImg, 0, 0);
            this.laytb.Dock = System.Windows.Forms.DockStyle.Right;
            this.laytb.Location = new System.Drawing.Point(693, 0);
            this.laytb.Margin = new System.Windows.Forms.Padding(2);
            this.laytb.Name = "laytb";
            this.laytb.RowCount = 6;
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.7685F));
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.7685F));
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.95715F));
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.73094F));
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.18114F));
            this.laytb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.59377F));
            this.laytb.Size = new System.Drawing.Size(272, 629);
            this.laytb.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Enabled = false;
            this.pictureBox2.Image = global::gravel.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(2, 562);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(268, 65);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pb_view
            // 
            this.pb_view.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_view.Image = global::gravel.Properties.Resources.logo21;
            this.pb_view.Location = new System.Drawing.Point(0, 394);
            this.pb_view.Name = "pb_view";
            this.pb_view.Size = new System.Drawing.Size(693, 235);
            this.pb_view.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_view.TabIndex = 3;
            this.pb_view.TabStop = false;
            this.pb_view.DragDrop += new System.Windows.Forms.DragEventHandler(this.pb_view_DragDrop);
            this.pb_view.DragEnter += new System.Windows.Forms.DragEventHandler(this.pb_view_DragEnter);
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackColor = System.Drawing.Color.Silver;
            this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(693, 629);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainPictureBox.TabIndex = 2;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainPictureBox_DragDrop);
            this.mainPictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainPictureBox_DragEnter);
            this.mainPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseClick);
            this.mainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseDown);
            this.mainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseMove);
            this.mainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseUp);
            // 
            // tb_view
            // 
            this.tb_view.BackColor = System.Drawing.Color.Silver;
            this.tb_view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_view.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_view.Enabled = false;
            this.tb_view.Font = new System.Drawing.Font("나눔바른고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_view.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_view.Location = new System.Drawing.Point(0, 0);
            this.tb_view.Margin = new System.Windows.Forms.Padding(3, 100, 3, 3);
            this.tb_view.Multiline = true;
            this.tb_view.Name = "tb_view";
            this.tb_view.Size = new System.Drawing.Size(693, 232);
            this.tb_view.TabIndex = 4;
            this.tb_view.TabStop = false;
            this.tb_view.Text = "\r\n\r\n\r\n\r\n\r\n\r\nDrag & Drop an image file";
            this.tb_view.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(965, 629);
            this.Controls.Add(this.tb_view);
            this.Controls.Add(this.pb_view);
            this.Controls.Add(this.mainPictureBox);
            this.Controls.Add(this.laytb);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "유빙운반역 이미지 분석 프로그램";
            this.Resize += new System.EventHandler(this.MainForm_Resize_1);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.laytb.ResumeLayout(false);
            this.laytb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel laytb;
        public System.Windows.Forms.Button bt_selectImg;
        public System.Windows.Forms.TextBox tb_imginfo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bt_saveResult;
        private System.Windows.Forms.Button bt_savePath;
        private System.Windows.Forms.Button bt_Settings;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.ComboBox comboBox;
        public System.Windows.Forms.PictureBox mainPictureBox;
        public System.Windows.Forms.TextBox tb_savePath;
        private System.Windows.Forms.TextBox tx_count;
        private System.Windows.Forms.PictureBox pb_view;
        private System.Windows.Forms.TextBox tb_view;
    }
}

