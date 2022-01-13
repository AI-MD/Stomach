
namespace Stomach
{
    partial class Main
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
            this.user_convert = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.Label();
            this.filtering_box = new System.Windows.Forms.ComboBox();
            this.stomachTimeView = new System.Windows.Forms.DataGridView();
            this.StomachTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalTimeView = new System.Windows.Forms.DataGridView();
            this.TotalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stop_btn = new System.Windows.Forms.Button();
            this.pause_btn = new System.Windows.Forms.Button();
            this.start_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.S5_button = new System.Windows.Forms.RadioButton();
            this.D2_button = new System.Windows.Forms.RadioButton();
            this.E_button = new System.Windows.Forms.RadioButton();
            this.D1_button = new System.Windows.Forms.RadioButton();
            this.S4_button = new System.Windows.Forms.RadioButton();
            this.S3_button = new System.Windows.Forms.RadioButton();
            this.S2_button = new System.Windows.Forms.RadioButton();
            this.S1_button = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.MainTabelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LeftTabelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.imageDisplayLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LeftTopLayout = new System.Windows.Forms.TableLayoutPanel();
            this.statusImage = new System.Windows.Forms.PictureBox();
            this.filter = new System.Windows.Forms.Label();
            this.RightTabelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.RightTopLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RightBottomLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TimeLayout = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ThreeButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DataGridPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.stomachTimeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTimeView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.MainTabelLayout.SuspendLayout();
            this.LeftTabelLayout.SuspendLayout();
            this.LeftTopLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).BeginInit();
            this.RightTabelLayout.SuspendLayout();
            this.RightTopLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RightBottomLayout.SuspendLayout();
            this.TimeLayout.SuspendLayout();
            this.buttonLayout.SuspendLayout();
            this.ThreeButtonLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.DataGridPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // user_convert
            // 
            this.user_convert.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.user_convert.Location = new System.Drawing.Point(255, 4);
            this.user_convert.Margin = new System.Windows.Forms.Padding(4);
            this.user_convert.Name = "user_convert";
            this.user_convert.Size = new System.Drawing.Size(124, 33);
            this.user_convert.TabIndex = 12;
            this.user_convert.Text = "사용자 전환";
            this.user_convert.UseVisualStyleBackColor = true;
            this.user_convert.Click += new System.EventHandler(this.user_convert_Click);
            // 
            // user
            // 
            this.user.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.user.AutoSize = true;
            this.user.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.user.Location = new System.Drawing.Point(54, 11);
            this.user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(135, 18);
            this.user.TabIndex = 13;
            this.user.Text = "USER : ADMIN";
            // 
            // filtering_box
            // 
            this.filtering_box.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.filtering_box.FormattingEnabled = true;
            this.filtering_box.Items.AddRange(new object[] {
            "ALL",
            "E",
            "S1",
            "S2",
            "S3",
            "S4",
            "S5",
            "S6",
            "D1",
            "D2",
            "X"});
            this.filtering_box.Location = new System.Drawing.Point(810, 24);
            this.filtering_box.Margin = new System.Windows.Forms.Padding(4);
            this.filtering_box.Name = "filtering_box";
            this.filtering_box.Size = new System.Drawing.Size(82, 26);
            this.filtering_box.TabIndex = 7;
            this.filtering_box.Text = "ALL";
            this.filtering_box.SelectedIndexChanged += new System.EventHandler(this.filtering_box_SelectedIndexChanged);
            // 
            // stomachTimeView
            // 
            this.stomachTimeView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.stomachTimeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stomachTimeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StomachTime});
            this.stomachTimeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stomachTimeView.Location = new System.Drawing.Point(3, 140);
            this.stomachTimeView.Name = "stomachTimeView";
            this.stomachTimeView.RowHeadersVisible = false;
            this.stomachTimeView.RowHeadersWidth = 62;
            this.stomachTimeView.RowTemplate.Height = 30;
            this.stomachTimeView.Size = new System.Drawing.Size(460, 132);
            this.stomachTimeView.TabIndex = 15;
            this.stomachTimeView.SizeChanged += new System.EventHandler(this.stomachTimeView_SizeChanged);
            // 
            // StomachTime
            // 
            this.StomachTime.HeaderText = "위 촬영 시간";
            this.StomachTime.MinimumWidth = 8;
            this.StomachTime.Name = "StomachTime";
            // 
            // totalTimeView
            // 
            this.totalTimeView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.totalTimeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.totalTimeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TotalTime});
            this.totalTimeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalTimeView.Location = new System.Drawing.Point(3, 3);
            this.totalTimeView.Name = "totalTimeView";
            this.totalTimeView.RowHeadersVisible = false;
            this.totalTimeView.RowHeadersWidth = 62;
            this.totalTimeView.RowTemplate.Height = 30;
            this.totalTimeView.Size = new System.Drawing.Size(460, 131);
            this.totalTimeView.TabIndex = 14;
            this.totalTimeView.SizeChanged += new System.EventHandler(this.totalTimeView_SizeChanged);
            // 
            // TotalTime
            // 
            this.TotalTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalTime.HeaderText = "전체 시간";
            this.TotalTime.MinimumWidth = 8;
            this.TotalTime.Name = "TotalTime";
            // 
            // stop_btn
            // 
            this.stop_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stop_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stop_btn.Location = new System.Drawing.Point(133, 3);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(124, 118);
            this.stop_btn.TabIndex = 8;
            this.stop_btn.Text = "STOP";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // pause_btn
            // 
            this.pause_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pause_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pause_btn.Location = new System.Drawing.Point(264, 4);
            this.pause_btn.Margin = new System.Windows.Forms.Padding(4);
            this.pause_btn.Name = "pause_btn";
            this.pause_btn.Size = new System.Drawing.Size(123, 116);
            this.pause_btn.TabIndex = 6;
            this.pause_btn.Text = "SAVE";
            this.pause_btn.UseVisualStyleBackColor = true;
            this.pause_btn.Click += new System.EventHandler(this.pause_btn_Click);
            // 
            // start_btn
            // 
            this.start_btn.AutoSize = true;
            this.start_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.start_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.start_btn.Location = new System.Drawing.Point(4, 4);
            this.start_btn.Margin = new System.Windows.Forms.Padding(4);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(122, 116);
            this.start_btn.TabIndex = 0;
            this.start_btn.Text = "START";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.S5_button);
            this.groupBox1.Controls.Add(this.D2_button);
            this.groupBox1.Controls.Add(this.E_button);
            this.groupBox1.Controls.Add(this.D1_button);
            this.groupBox1.Controls.Add(this.S4_button);
            this.groupBox1.Controls.Add(this.S3_button);
            this.groupBox1.Controls.Add(this.S2_button);
            this.groupBox1.Controls.Add(this.S1_button);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(189, 524);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // S5_button
            // 
            this.S5_button.AutoSize = true;
            this.S5_button.Location = new System.Drawing.Point(10, 323);
            this.S5_button.Margin = new System.Windows.Forms.Padding(4);
            this.S5_button.Name = "S5_button";
            this.S5_button.Size = new System.Drawing.Size(54, 22);
            this.S5_button.TabIndex = 15;
            this.S5_button.TabStop = true;
            this.S5_button.Text = "S5";
            this.S5_button.UseVisualStyleBackColor = true;
            // 
            // D2_button
            // 
            this.D2_button.AutoSize = true;
            this.D2_button.Location = new System.Drawing.Point(10, 437);
            this.D2_button.Name = "D2_button";
            this.D2_button.Size = new System.Drawing.Size(54, 22);
            this.D2_button.TabIndex = 14;
            this.D2_button.TabStop = true;
            this.D2_button.Text = "D2";
            this.D2_button.UseVisualStyleBackColor = true;
            // 
            // E_button
            // 
            this.E_button.AutoSize = true;
            this.E_button.Enabled = false;
            this.E_button.Location = new System.Drawing.Point(10, 36);
            this.E_button.Margin = new System.Windows.Forms.Padding(4);
            this.E_button.Name = "E_button";
            this.E_button.Size = new System.Drawing.Size(44, 22);
            this.E_button.TabIndex = 8;
            this.E_button.TabStop = true;
            this.E_button.Text = "E";
            this.E_button.UseVisualStyleBackColor = true;
            // 
            // D1_button
            // 
            this.D1_button.AutoSize = true;
            this.D1_button.Location = new System.Drawing.Point(10, 382);
            this.D1_button.Margin = new System.Windows.Forms.Padding(4);
            this.D1_button.Name = "D1_button";
            this.D1_button.Size = new System.Drawing.Size(54, 22);
            this.D1_button.TabIndex = 7;
            this.D1_button.TabStop = true;
            this.D1_button.Text = "D1";
            this.D1_button.UseVisualStyleBackColor = true;
            // 
            // S4_button
            // 
            this.S4_button.AutoSize = true;
            this.S4_button.Location = new System.Drawing.Point(10, 257);
            this.S4_button.Margin = new System.Windows.Forms.Padding(4);
            this.S4_button.Name = "S4_button";
            this.S4_button.Size = new System.Drawing.Size(54, 22);
            this.S4_button.TabIndex = 3;
            this.S4_button.TabStop = true;
            this.S4_button.Text = "S4";
            this.S4_button.UseVisualStyleBackColor = true;
            // 
            // S3_button
            // 
            this.S3_button.AutoSize = true;
            this.S3_button.Location = new System.Drawing.Point(10, 196);
            this.S3_button.Margin = new System.Windows.Forms.Padding(4);
            this.S3_button.Name = "S3_button";
            this.S3_button.Size = new System.Drawing.Size(54, 22);
            this.S3_button.TabIndex = 2;
            this.S3_button.TabStop = true;
            this.S3_button.Text = "S3";
            this.S3_button.UseVisualStyleBackColor = true;
            // 
            // S2_button
            // 
            this.S2_button.AutoSize = true;
            this.S2_button.Location = new System.Drawing.Point(10, 141);
            this.S2_button.Margin = new System.Windows.Forms.Padding(4);
            this.S2_button.Name = "S2_button";
            this.S2_button.Size = new System.Drawing.Size(54, 22);
            this.S2_button.TabIndex = 1;
            this.S2_button.TabStop = true;
            this.S2_button.Text = "S2";
            this.S2_button.UseVisualStyleBackColor = true;
            // 
            // S1_button
            // 
            this.S1_button.AutoSize = true;
            this.S1_button.Cursor = System.Windows.Forms.Cursors.Default;
            this.S1_button.Location = new System.Drawing.Point(10, 89);
            this.S1_button.Margin = new System.Windows.Forms.Padding(4);
            this.S1_button.Name = "S1_button";
            this.S1_button.Size = new System.Drawing.Size(54, 22);
            this.S1_button.TabIndex = 0;
            this.S1_button.TabStop = true;
            this.S1_button.Text = "S1";
            this.S1_button.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(0, -3);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "CLASS";
            // 
            // save_button
            // 
            this.save_button.Dock = System.Windows.Forms.DockStyle.Left;
            this.save_button.Location = new System.Drawing.Point(3, 133);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(387, 92);
            this.save_button.TabIndex = 14;
            this.save_button.Text = "EXPORT";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // MainTabelLayout
            // 
            this.MainTabelLayout.ColumnCount = 2;
            this.MainTabelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.58496F));
            this.MainTabelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.41504F));
            this.MainTabelLayout.Controls.Add(this.LeftTabelLayout, 0, 0);
            this.MainTabelLayout.Controls.Add(this.RightTabelLayout, 1, 0);
            this.MainTabelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabelLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTabelLayout.Name = "MainTabelLayout";
            this.MainTabelLayout.RowCount = 1;
            this.MainTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTabelLayout.Size = new System.Drawing.Size(1795, 1143);
            this.MainTabelLayout.TabIndex = 21;
            // 
            // LeftTabelLayout
            // 
            this.LeftTabelLayout.ColumnCount = 1;
            this.LeftTabelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTabelLayout.Controls.Add(this.imageDisplayLayout, 0, 1);
            this.LeftTabelLayout.Controls.Add(this.LeftTopLayout, 0, 0);
            this.LeftTabelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTabelLayout.Location = new System.Drawing.Point(3, 3);
            this.LeftTabelLayout.Name = "LeftTabelLayout";
            this.LeftTabelLayout.RowCount = 2;
            this.LeftTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.211961F));
            this.LeftTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.78804F));
            this.LeftTabelLayout.Size = new System.Drawing.Size(902, 1137);
            this.LeftTabelLayout.TabIndex = 0;
            // 
            // imageDisplayLayout
            // 
            this.imageDisplayLayout.AutoScroll = true;
            this.imageDisplayLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.imageDisplayLayout.ColumnCount = 3;
            this.LeftTabelLayout.SetColumnSpan(this.imageDisplayLayout, 6);
            this.imageDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.imageDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.imageDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.imageDisplayLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDisplayLayout.Location = new System.Drawing.Point(3, 84);
            this.imageDisplayLayout.Name = "imageDisplayLayout";
            this.imageDisplayLayout.RowCount = 4;
            this.imageDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.imageDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.imageDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.imageDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.imageDisplayLayout.Size = new System.Drawing.Size(896, 1050);
            this.imageDisplayLayout.TabIndex = 11;
            // 
            // LeftTopLayout
            // 
            this.LeftTopLayout.ColumnCount = 3;
            this.LeftTopLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTopLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTopLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.LeftTopLayout.Controls.Add(this.statusImage, 0, 0);
            this.LeftTopLayout.Controls.Add(this.filtering_box, 2, 0);
            this.LeftTopLayout.Controls.Add(this.filter, 1, 0);
            this.LeftTopLayout.Location = new System.Drawing.Point(3, 3);
            this.LeftTopLayout.Name = "LeftTopLayout";
            this.LeftTopLayout.RowCount = 1;
            this.LeftTopLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTopLayout.Size = new System.Drawing.Size(896, 75);
            this.LeftTopLayout.TabIndex = 12;
            // 
            // statusImage
            // 
            this.statusImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.statusImage.Location = new System.Drawing.Point(3, 24);
            this.statusImage.Name = "statusImage";
            this.statusImage.Size = new System.Drawing.Size(166, 27);
            this.statusImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.statusImage.TabIndex = 20;
            this.statusImage.TabStop = false;
            // 
            // filter
            // 
            this.filter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.filter.AutoSize = true;
            this.filter.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.filter.Location = new System.Drawing.Point(638, 26);
            this.filter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(60, 22);
            this.filter.TabIndex = 10;
            this.filter.Text = "Filter";
            // 
            // RightTabelLayout
            // 
            this.RightTabelLayout.ColumnCount = 1;
            this.RightTabelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightTabelLayout.Controls.Add(this.RightTopLayout, 0, 0);
            this.RightTabelLayout.Controls.Add(this.RightBottomLayout, 0, 2);
            this.RightTabelLayout.Controls.Add(this.DataGridPanel, 0, 1);
            this.RightTabelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightTabelLayout.Location = new System.Drawing.Point(911, 3);
            this.RightTabelLayout.Name = "RightTabelLayout";
            this.RightTabelLayout.RowCount = 3;
            this.RightTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.35154F));
            this.RightTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.64846F));
            this.RightTabelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.RightTabelLayout.Size = new System.Drawing.Size(881, 1137);
            this.RightTabelLayout.TabIndex = 1;
            // 
            // RightTopLayout
            // 
            this.RightTopLayout.ColumnCount = 2;
            this.RightTopLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.6F));
            this.RightTopLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.4F));
            this.RightTopLayout.Controls.Add(this.groupBox1, 0, 0);
            this.RightTopLayout.Controls.Add(this.pictureBox1, 1, 0);
            this.RightTopLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightTopLayout.Location = new System.Drawing.Point(3, 3);
            this.RightTopLayout.Name = "RightTopLayout";
            this.RightTopLayout.RowCount = 1;
            this.RightTopLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightTopLayout.Size = new System.Drawing.Size(875, 524);
            this.RightTopLayout.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(192, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(680, 518);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // RightBottomLayout
            // 
            this.RightBottomLayout.ColumnCount = 2;
            this.RightBottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.05714F));
            this.RightBottomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.94286F));
            this.RightBottomLayout.Controls.Add(this.TimeLayout, 0, 0);
            this.RightBottomLayout.Controls.Add(this.buttonLayout, 1, 0);
            this.RightBottomLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightBottomLayout.Location = new System.Drawing.Point(3, 853);
            this.RightBottomLayout.Name = "RightBottomLayout";
            this.RightBottomLayout.RowCount = 1;
            this.RightBottomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RightBottomLayout.Size = new System.Drawing.Size(875, 281);
            this.RightBottomLayout.TabIndex = 1;
            // 
            // TimeLayout
            // 
            this.TimeLayout.ColumnCount = 1;
            this.TimeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TimeLayout.Controls.Add(this.totalTimeView, 0, 0);
            this.TimeLayout.Controls.Add(this.stomachTimeView, 0, 1);
            this.TimeLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeLayout.Location = new System.Drawing.Point(3, 3);
            this.TimeLayout.Name = "TimeLayout";
            this.TimeLayout.RowCount = 2;
            this.TimeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TimeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.TimeLayout.Size = new System.Drawing.Size(466, 275);
            this.TimeLayout.TabIndex = 0;
            this.TimeLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.TimeLayout_Paint);
            // 
            // buttonLayout
            // 
            this.buttonLayout.ColumnCount = 1;
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayout.Controls.Add(this.ThreeButtonLayout, 0, 0);
            this.buttonLayout.Controls.Add(this.save_button, 0, 1);
            this.buttonLayout.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.buttonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLayout.Location = new System.Drawing.Point(475, 3);
            this.buttonLayout.Name = "buttonLayout";
            this.buttonLayout.RowCount = 3;
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.01754F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.98246F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.buttonLayout.Size = new System.Drawing.Size(397, 275);
            this.buttonLayout.TabIndex = 1;
            // 
            // ThreeButtonLayout
            // 
            this.ThreeButtonLayout.ColumnCount = 3;
            this.ThreeButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ThreeButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ThreeButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ThreeButtonLayout.Controls.Add(this.start_btn, 0, 0);
            this.ThreeButtonLayout.Controls.Add(this.stop_btn, 1, 0);
            this.ThreeButtonLayout.Controls.Add(this.pause_btn, 2, 0);
            this.ThreeButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThreeButtonLayout.Location = new System.Drawing.Point(3, 3);
            this.ThreeButtonLayout.Name = "ThreeButtonLayout";
            this.ThreeButtonLayout.RowCount = 1;
            this.ThreeButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ThreeButtonLayout.Size = new System.Drawing.Size(391, 124);
            this.ThreeButtonLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.40409F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.59591F));
            this.tableLayoutPanel1.Controls.Add(this.user_convert, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.user, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 231);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 41);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // DataGridPanel
            // 
            this.DataGridPanel.Controls.Add(this.textBox1);
            this.DataGridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridPanel.Location = new System.Drawing.Point(3, 533);
            this.DataGridPanel.Name = "DataGridPanel";
            this.DataGridPanel.Size = new System.Drawing.Size(875, 314);
            this.DataGridPanel.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(576, 100);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1795, 1143);
            this.Controls.Add(this.MainTabelLayout);
            this.Name = "Main";
            this.Text = "StomachGuideSystem";
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.stomachTimeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTimeView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MainTabelLayout.ResumeLayout(false);
            this.LeftTabelLayout.ResumeLayout(false);
            this.LeftTopLayout.ResumeLayout(false);
            this.LeftTopLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusImage)).EndInit();
            this.RightTabelLayout.ResumeLayout(false);
            this.RightTopLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.RightBottomLayout.ResumeLayout(false);
            this.TimeLayout.ResumeLayout(false);
            this.buttonLayout.ResumeLayout(false);
            this.ThreeButtonLayout.ResumeLayout(false);
            this.ThreeButtonLayout.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.DataGridPanel.ResumeLayout(false);
            this.DataGridPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button user_convert;
        private System.Windows.Forms.Label user;
        public System.Windows.Forms.Button stop_btn;
        public System.Windows.Forms.Button pause_btn;
        private System.Windows.Forms.Button start_btn;
        protected internal System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton E_button;
        public System.Windows.Forms.RadioButton D1_button;
        public System.Windows.Forms.RadioButton S4_button;
        public System.Windows.Forms.RadioButton S3_button;
        public System.Windows.Forms.RadioButton S2_button;
        public System.Windows.Forms.RadioButton S1_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton D2_button;
        public System.Windows.Forms.RadioButton S5_button;
        public System.Windows.Forms.DataGridView stomachTimeView;
        public System.Windows.Forms.DataGridView totalTimeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn StomachTime;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox statusImage;
        private System.Windows.Forms.ComboBox filtering_box;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TableLayoutPanel MainTabelLayout;
        private System.Windows.Forms.TableLayoutPanel LeftTabelLayout;
        private System.Windows.Forms.Label filter;
        public System.Windows.Forms.TableLayoutPanel imageDisplayLayout;
        private System.Windows.Forms.TableLayoutPanel LeftTopLayout;
        private System.Windows.Forms.TableLayoutPanel RightTabelLayout;
        public System.Windows.Forms.TableLayoutPanel RightTopLayout;
        private System.Windows.Forms.TableLayoutPanel RightBottomLayout;
        private System.Windows.Forms.Panel DataGridPanel;
        private System.Windows.Forms.TableLayoutPanel TimeLayout;
        private System.Windows.Forms.TableLayoutPanel buttonLayout;
        private System.Windows.Forms.TableLayoutPanel ThreeButtonLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalTime;
        private System.Windows.Forms.TextBox textBox1;
    }
}

