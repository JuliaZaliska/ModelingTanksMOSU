namespace Lab1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            pbTanks = new PictureBox();
            btnStart = new Button();
            btnPause = new Button();
            tmModel = new System.Windows.Forms.Timer(components);
            tbSetZ1 = new TextBox();
            tbSetZ2 = new TextBox();
            lbInitialZ1 = new Label();
            lbInitialZ2 = new Label();
            chMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lbCurrentZ2 = new Label();
            lbCurrentZ1 = new Label();
            trbXin1 = new TrackBar();
            tplXin1 = new TableLayoutPanel();
            lbXin1 = new Label();
            tplXin2 = new TableLayoutPanel();
            lbXin2 = new Label();
            trbXin2 = new TrackBar();
            tplXout2 = new TableLayoutPanel();
            trbXout2 = new TrackBar();
            lbXout2 = new Label();
            btnReset = new Button();
            btnSpeed1 = new Button();
            btnSpeed10 = new Button();
            ((System.ComponentModel.ISupportInitialize)pbTanks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trbXin1).BeginInit();
            tplXin1.SuspendLayout();
            tplXin2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trbXin2).BeginInit();
            tplXout2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trbXout2).BeginInit();
            SuspendLayout();
            // 
            // pbTanks
            // 
            pbTanks.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbTanks.Image = (Image)resources.GetObject("pbTanks.Image");
            pbTanks.Location = new Point(485, 66);
            pbTanks.Name = "pbTanks";
            pbTanks.Size = new Size(635, 266);
            pbTanks.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTanks.TabIndex = 0;
            pbTanks.TabStop = false;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(27, 18);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(108, 42);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(27, 66);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(108, 42);
            btnPause.TabIndex = 2;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // tmModel
            // 
            tmModel.Interval = 1000;
            tmModel.Tick += tmModel_Tick;
            // 
            // tbSetZ1
            // 
            tbSetZ1.Location = new Point(199, 55);
            tbSetZ1.Name = "tbSetZ1";
            tbSetZ1.Size = new Size(50, 27);
            tbSetZ1.TabIndex = 12;
            // 
            // tbSetZ2
            // 
            tbSetZ2.Location = new Point(199, 88);
            tbSetZ2.Name = "tbSetZ2";
            tbSetZ2.Size = new Size(50, 27);
            tbSetZ2.TabIndex = 13;
            // 
            // lbInitialZ1
            // 
            lbInitialZ1.AutoSize = true;
            lbInitialZ1.Location = new Point(141, 57);
            lbInitialZ1.Name = "lbInitialZ1";
            lbInitialZ1.Size = new Size(52, 20);
            lbInitialZ1.TabIndex = 14;
            lbInitialZ1.Text = "Set z1:";
            // 
            // lbInitialZ2
            // 
            lbInitialZ2.AutoSize = true;
            lbInitialZ2.Location = new Point(141, 90);
            lbInitialZ2.Name = "lbInitialZ2";
            lbInitialZ2.Size = new Size(52, 20);
            lbInitialZ2.TabIndex = 15;
            lbInitialZ2.Text = "Set z2:";
            // 
            // chMain
            // 
            chMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea1.Name = "ChartArea1";
            chMain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chMain.Legends.Add(legend1);
            chMain.Location = new Point(-48, 372);
            chMain.Name = "chMain";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.LegendText = "z1";
            series1.LegendToolTip = "Liquid level in Tank 1";
            series1.Name = "z1Series";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.LegendText = "z2";
            series2.LegendToolTip = "Liquid level in Tank 2";
            series2.Name = "z2Series";
            chMain.Series.Add(series1);
            chMain.Series.Add(series2);
            chMain.Size = new Size(1373, 344);
            chMain.TabIndex = 16;
            chMain.Text = "1";
            // 
            // lbCurrentZ2
            // 
            lbCurrentZ2.AutoSize = true;
            lbCurrentZ2.Location = new Point(255, 90);
            lbCurrentZ2.Name = "lbCurrentZ2";
            lbCurrentZ2.Size = new Size(79, 20);
            lbCurrentZ2.TabIndex = 20;
            lbCurrentZ2.Text = "Current z2:";
            // 
            // lbCurrentZ1
            // 
            lbCurrentZ1.AutoSize = true;
            lbCurrentZ1.Location = new Point(255, 57);
            lbCurrentZ1.Name = "lbCurrentZ1";
            lbCurrentZ1.Size = new Size(79, 20);
            lbCurrentZ1.TabIndex = 19;
            lbCurrentZ1.Text = "Current z1:";
            // 
            // trbXin1
            // 
            trbXin1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trbXin1.BackColor = SystemColors.ButtonHighlight;
            trbXin1.LargeChange = 1;
            trbXin1.Location = new Point(3, 3);
            trbXin1.Name = "trbXin1";
            trbXin1.Size = new Size(194, 46);
            trbXin1.TabIndex = 21;
            trbXin1.UseWaitCursor = true;
            trbXin1.Scroll += trbXin1_Scroll;
            // 
            // tplXin1
            // 
            tplXin1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tplXin1.ColumnCount = 2;
            tplXin1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tplXin1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tplXin1.Controls.Add(lbXin1, 1, 0);
            tplXin1.Controls.Add(trbXin1, 0, 0);
            tplXin1.Location = new Point(485, 15);
            tplXin1.Name = "tplXin1";
            tplXin1.RowCount = 1;
            tplXin1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tplXin1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tplXin1.Size = new Size(250, 52);
            tplXin1.TabIndex = 22;
            // 
            // lbXin1
            // 
            lbXin1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbXin1.AutoSize = true;
            lbXin1.Location = new Point(203, 0);
            lbXin1.Name = "lbXin1";
            lbXin1.Size = new Size(44, 52);
            lbXin1.TabIndex = 14;
            lbXin1.Text = "0";
            lbXin1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tplXin2
            // 
            tplXin2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tplXin2.ColumnCount = 2;
            tplXin2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tplXin2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tplXin2.Controls.Add(lbXin2, 1, 0);
            tplXin2.Controls.Add(trbXin2, 0, 0);
            tplXin2.Location = new Point(793, 15);
            tplXin2.Name = "tplXin2";
            tplXin2.RowCount = 1;
            tplXin2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tplXin2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tplXin2.Size = new Size(250, 52);
            tplXin2.TabIndex = 23;
            // 
            // lbXin2
            // 
            lbXin2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbXin2.AutoSize = true;
            lbXin2.Location = new Point(203, 0);
            lbXin2.Name = "lbXin2";
            lbXin2.Size = new Size(44, 52);
            lbXin2.TabIndex = 14;
            lbXin2.Text = "0";
            lbXin2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trbXin2
            // 
            trbXin2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trbXin2.BackColor = SystemColors.HighlightText;
            trbXin2.LargeChange = 1;
            trbXin2.Location = new Point(3, 3);
            trbXin2.Name = "trbXin2";
            trbXin2.Size = new Size(194, 46);
            trbXin2.TabIndex = 21;
            trbXin2.UseWaitCursor = true;
            trbXin2.Scroll += trbXin2_Scroll;
            // 
            // tplXout2
            // 
            tplXout2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tplXout2.ColumnCount = 2;
            tplXout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tplXout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tplXout2.Controls.Add(trbXout2, 0, 0);
            tplXout2.Controls.Add(lbXout2, 1, 0);
            tplXout2.Location = new Point(913, 314);
            tplXout2.Name = "tplXout2";
            tplXout2.RowCount = 1;
            tplXout2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tplXout2.Size = new Size(240, 52);
            tplXout2.TabIndex = 24;
            // 
            // trbXout2
            // 
            trbXout2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trbXout2.BackColor = SystemColors.ButtonHighlight;
            trbXout2.LargeChange = 1;
            trbXout2.Location = new Point(3, 3);
            trbXout2.Name = "trbXout2";
            trbXout2.Size = new Size(186, 46);
            trbXout2.TabIndex = 21;
            trbXout2.UseWaitCursor = true;
            trbXout2.Scroll += trbXout1_Scroll;
            // 
            // lbXout2
            // 
            lbXout2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbXout2.AutoSize = true;
            lbXout2.Location = new Point(195, 0);
            lbXout2.Name = "lbXout2";
            lbXout2.Size = new Size(42, 52);
            lbXout2.TabIndex = 14;
            lbXout2.Text = "0";
            lbXout2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(27, 114);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(108, 42);
            btnReset.TabIndex = 2;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnSpeed1
            // 
            btnSpeed1.Location = new Point(139, 18);
            btnSpeed1.Name = "btnSpeed1";
            btnSpeed1.Size = new Size(52, 31);
            btnSpeed1.TabIndex = 25;
            btnSpeed1.Text = "x1";
            btnSpeed1.UseVisualStyleBackColor = true;
            btnSpeed1.Click += btnSpeed1_Click;
            // 
            // btnSpeed10
            // 
            btnSpeed10.Location = new Point(197, 18);
            btnSpeed10.Name = "btnSpeed10";
            btnSpeed10.Size = new Size(52, 31);
            btnSpeed10.TabIndex = 25;
            btnSpeed10.Text = "x10";
            btnSpeed10.UseVisualStyleBackColor = true;
            btnSpeed10.Click += btnSpeed10_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.HighlightText;
            ClientSize = new Size(1326, 721);
            Controls.Add(btnSpeed10);
            Controls.Add(btnSpeed1);
            Controls.Add(tplXout2);
            Controls.Add(tplXin2);
            Controls.Add(tplXin1);
            Controls.Add(lbCurrentZ2);
            Controls.Add(lbCurrentZ1);
            Controls.Add(chMain);
            Controls.Add(lbInitialZ2);
            Controls.Add(lbInitialZ1);
            Controls.Add(tbSetZ2);
            Controls.Add(tbSetZ1);
            Controls.Add(btnReset);
            Controls.Add(btnPause);
            Controls.Add(btnStart);
            Controls.Add(pbTanks);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbTanks).EndInit();
            ((System.ComponentModel.ISupportInitialize)chMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)trbXin1).EndInit();
            tplXin1.ResumeLayout(false);
            tplXin1.PerformLayout();
            tplXin2.ResumeLayout(false);
            tplXin2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trbXin2).EndInit();
            tplXout2.ResumeLayout(false);
            tplXout2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trbXout2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbTanks;
        private Button btnStart;
        private Button btnPause;
        private System.Windows.Forms.Timer tmModel;
        private TextBox tbSetZ1;
        private TextBox tbSetZ2;
        private Label lbInitialZ1;
        private Label lbInitialZ2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chMain;
        private Label lbCurrentZ2;
        private Label lbCurrentZ1;
        private Label lb_x_in2;
        private Label lb_x_out1;
        private Label lb_x_in1;
        private Button btn_x_in1_Down;
        private Button btn_x_in1_Up;
        private Button btn_x_in2_Down;
        private Button btn_x_in2_Up;
        private Button btn_x_out1_Up;
        private Button btn_x_out1_Down;
        private TrackBar trbXin1;
        private TableLayoutPanel tplXin1;
        private Label lbXin1;
        private TableLayoutPanel tplXin2;
        private Label lbXin2;
        private TrackBar trbXin2;
        private TableLayoutPanel tplXout2;
        private Label lbXout2;
        private TrackBar trbXout2;
        private Button btnReset;
        private Button btnSpeed1;
        private Button btnSpeed10;
    }
}
