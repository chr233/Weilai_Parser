namespace Weilai.Forms
{
    partial class FrmMain
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
            lvFiles = new ListView();
            lvFilesCh1 = new ColumnHeader();
            lvFilesCh2 = new ColumnHeader();
            btnSelectFile = new Button();
            label1 = new Label();
            btnClearFileList = new Button();
            tcMain = new TabControl();
            tcMainTp1 = new TabPage();
            lvCharacter = new ListView();
            lvCharacterCh1 = new ColumnHeader();
            lvCharacterCh2 = new ColumnHeader();
            lvCharacterCh3 = new ColumnHeader();
            lvCharacterCh4 = new ColumnHeader();
            lvCharacterCh5 = new ColumnHeader();
            lvCharacterCh6 = new ColumnHeader();
            tcMainTp2 = new TabPage();
            lvDialog = new ListView();
            lvDialog1 = new ColumnHeader();
            lvDialog2 = new ColumnHeader();
            lvDialog3 = new ColumnHeader();
            lvDialog5 = new ColumnHeader();
            lvDialog6 = new ColumnHeader();
            lvDialog7 = new ColumnHeader();
            tcMainTp3 = new TabPage();
            lvAsset = new ListView();
            lvAssetCh1 = new ColumnHeader();
            lvAssetCh2 = new ColumnHeader();
            lvAssetCh3 = new ColumnHeader();
            tcMainTp4 = new TabPage();
            groupBox1 = new GroupBox();
            btnExportFolder = new Button();
            txtExportFolder = new TextBox();
            btnExport = new Button();
            btnParse = new Button();
            pbProcess = new ProgressBar();
            cbFileFilter = new ComboBox();
            label2 = new Label();
            cbHiddenZero = new CheckBox();
            btnSelectFolder = new Button();
            lvDialog4 = new ColumnHeader();
            tcMain.SuspendLayout();
            tcMainTp1.SuspendLayout();
            tcMainTp2.SuspendLayout();
            tcMainTp3.SuspendLayout();
            tcMainTp4.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lvFiles
            // 
            lvFiles.AllowDrop = true;
            lvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvFiles.Columns.AddRange(new ColumnHeader[] { lvFilesCh1, lvFilesCh2 });
            lvFiles.Location = new Point(131, 12);
            lvFiles.Name = "lvFiles";
            lvFiles.Size = new Size(647, 179);
            lvFiles.TabIndex = 11;
            lvFiles.UseCompatibleStateImageBehavior = false;
            lvFiles.View = View.Details;
            lvFiles.DragDrop += LvFiles_DragDrop;
            lvFiles.DragEnter += LvFiles_DragEnter;
            // 
            // lvFilesCh1
            // 
            lvFilesCh1.Text = "文件名";
            lvFilesCh1.Width = 200;
            // 
            // lvFilesCh2
            // 
            lvFilesCh2.Text = "文件路径";
            lvFilesCh2.Width = 300;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(12, 12);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(113, 28);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "&S. 选择文件";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += BtnSelectFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.AppWorkspace;
            label1.Location = new Point(40, 111);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 3;
            label1.Text = "支持拖拽";
            // 
            // btnClearFileList
            // 
            btnClearFileList.Location = new Point(12, 80);
            btnClearFileList.Name = "btnClearFileList";
            btnClearFileList.Size = new Size(113, 28);
            btnClearFileList.TabIndex = 1;
            btnClearFileList.Text = "&C. 清空列表";
            btnClearFileList.UseVisualStyleBackColor = true;
            btnClearFileList.Click += BtnClearFileList_Click;
            // 
            // tcMain
            // 
            tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tcMain.Controls.Add(tcMainTp1);
            tcMain.Controls.Add(tcMainTp2);
            tcMain.Controls.Add(tcMainTp3);
            tcMain.Controls.Add(tcMainTp4);
            tcMain.Location = new Point(12, 244);
            tcMain.Name = "tcMain";
            tcMain.SelectedIndex = 0;
            tcMain.Size = new Size(766, 341);
            tcMain.TabIndex = 7;
            // 
            // tcMainTp1
            // 
            tcMainTp1.Controls.Add(lvCharacter);
            tcMainTp1.Location = new Point(4, 26);
            tcMainTp1.Name = "tcMainTp1";
            tcMainTp1.Padding = new Padding(3);
            tcMainTp1.Size = new Size(693, 311);
            tcMainTp1.TabIndex = 0;
            tcMainTp1.Text = "角色信息统计";
            tcMainTp1.UseVisualStyleBackColor = true;
            // 
            // lvCharacter
            // 
            lvCharacter.Columns.AddRange(new ColumnHeader[] { lvCharacterCh1, lvCharacterCh2, lvCharacterCh3, lvCharacterCh4, lvCharacterCh5, lvCharacterCh6 });
            lvCharacter.Dock = DockStyle.Fill;
            lvCharacter.FullRowSelect = true;
            lvCharacter.Location = new Point(3, 3);
            lvCharacter.Name = "lvCharacter";
            lvCharacter.Size = new Size(687, 305);
            lvCharacter.TabIndex = 8;
            lvCharacter.UseCompatibleStateImageBehavior = false;
            lvCharacter.View = View.Details;
            // 
            // lvCharacterCh1
            // 
            lvCharacterCh1.Text = "角色名";
            lvCharacterCh1.Width = 100;
            // 
            // lvCharacterCh2
            // 
            lvCharacterCh2.Text = "缩写";
            lvCharacterCh2.Width = 100;
            // 
            // lvCharacterCh3
            // 
            lvCharacterCh3.Text = "字数";
            lvCharacterCh3.Width = 100;
            // 
            // lvCharacterCh4
            // 
            lvCharacterCh4.Text = "字数(不含标点)";
            lvCharacterCh4.Width = 100;
            // 
            // lvCharacterCh5
            // 
            lvCharacterCh5.Text = "句数";
            lvCharacterCh5.Width = 100;
            // 
            // lvCharacterCh6
            // 
            lvCharacterCh6.Text = "句数(不含标点)";
            lvCharacterCh6.Width = 100;
            // 
            // tcMainTp2
            // 
            tcMainTp2.Controls.Add(lvDialog);
            tcMainTp2.Location = new Point(4, 26);
            tcMainTp2.Name = "tcMainTp2";
            tcMainTp2.Padding = new Padding(3);
            tcMainTp2.Size = new Size(758, 311);
            tcMainTp2.TabIndex = 1;
            tcMainTp2.Text = "台词信息";
            tcMainTp2.UseVisualStyleBackColor = true;
            // 
            // lvDialog
            // 
            lvDialog.Columns.AddRange(new ColumnHeader[] { lvDialog1, lvDialog2, lvDialog3, lvDialog4, lvDialog5, lvDialog6, lvDialog7 });
            lvDialog.Dock = DockStyle.Fill;
            lvDialog.FullRowSelect = true;
            lvDialog.Location = new Point(3, 3);
            lvDialog.Name = "lvDialog";
            lvDialog.Size = new Size(752, 305);
            lvDialog.TabIndex = 9;
            lvDialog.UseCompatibleStateImageBehavior = false;
            lvDialog.View = View.Details;
            // 
            // lvDialog1
            // 
            lvDialog1.Text = "角色";
            // 
            // lvDialog2
            // 
            lvDialog2.Text = "代号";
            // 
            // lvDialog3
            // 
            lvDialog3.Text = "行号";
            // 
            // lvDialog5
            // 
            lvDialog5.Text = "文本";
            lvDialog5.Width = 200;
            // 
            // lvDialog6
            // 
            lvDialog6.Text = "文本(去除符号)";
            lvDialog6.Width = 200;
            // 
            // lvDialog7
            // 
            lvDialog7.Text = "字数";
            lvDialog7.Width = 80;
            // 
            // tcMainTp3
            // 
            tcMainTp3.Controls.Add(lvAsset);
            tcMainTp3.Location = new Point(4, 26);
            tcMainTp3.Name = "tcMainTp3";
            tcMainTp3.Padding = new Padding(3);
            tcMainTp3.Size = new Size(693, 311);
            tcMainTp3.TabIndex = 2;
            tcMainTp3.Text = "资源列表";
            tcMainTp3.UseVisualStyleBackColor = true;
            // 
            // lvAsset
            // 
            lvAsset.Columns.AddRange(new ColumnHeader[] { lvAssetCh1, lvAssetCh2, lvAssetCh3 });
            lvAsset.Dock = DockStyle.Fill;
            lvAsset.FullRowSelect = true;
            lvAsset.Location = new Point(3, 3);
            lvAsset.Name = "lvAsset";
            lvAsset.Size = new Size(687, 305);
            lvAsset.TabIndex = 10;
            lvAsset.UseCompatibleStateImageBehavior = false;
            lvAsset.View = View.Details;
            // 
            // lvAssetCh1
            // 
            lvAssetCh1.Text = "类型";
            lvAssetCh1.Width = 100;
            // 
            // lvAssetCh2
            // 
            lvAssetCh2.Text = "名称";
            lvAssetCh2.Width = 200;
            // 
            // lvAssetCh3
            // 
            lvAssetCh3.Text = "出现次数";
            lvAssetCh3.Width = 100;
            // 
            // tcMainTp4
            // 
            tcMainTp4.Controls.Add(groupBox1);
            tcMainTp4.Controls.Add(btnExport);
            tcMainTp4.Location = new Point(4, 26);
            tcMainTp4.Name = "tcMainTp4";
            tcMainTp4.Padding = new Padding(3);
            tcMainTp4.Size = new Size(693, 311);
            tcMainTp4.TabIndex = 3;
            tcMainTp4.Text = "导出";
            tcMainTp4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnExportFolder);
            groupBox1.Controls.Add(txtExportFolder);
            groupBox1.Location = new Point(24, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(636, 53);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "导出路径";
            // 
            // btnExportFolder
            // 
            btnExportFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportFolder.Location = new Point(570, 22);
            btnExportFolder.Name = "btnExportFolder";
            btnExportFolder.Size = new Size(60, 23);
            btnExportFolder.TabIndex = 9;
            btnExportFolder.Text = "...";
            btnExportFolder.UseVisualStyleBackColor = true;
            btnExportFolder.Click += BtnExportFolder_Click;
            // 
            // txtExportFolder
            // 
            txtExportFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtExportFolder.Location = new Point(6, 22);
            txtExportFolder.Name = "txtExportFolder";
            txtExportFolder.Size = new Size(558, 23);
            txtExportFolder.TabIndex = 7;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(528, 97);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(132, 32);
            btnExport.TabIndex = 6;
            btnExport.Text = "&E. 导出结果";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // btnParse
            // 
            btnParse.Location = new Point(12, 150);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(113, 41);
            btnParse.TabIndex = 2;
            btnParse.Text = "&P. 解析文件";
            btnParse.UseVisualStyleBackColor = true;
            btnParse.Click += BtnParse_Click;
            // 
            // pbProcess
            // 
            pbProcess.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbProcess.Location = new Point(12, 197);
            pbProcess.Name = "pbProcess";
            pbProcess.Size = new Size(766, 10);
            pbProcess.TabIndex = 8;
            // 
            // cbFileFilter
            // 
            cbFileFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbFileFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFileFilter.ItemHeight = 17;
            cbFileFilter.Location = new Point(131, 213);
            cbFileFilter.Name = "cbFileFilter";
            cbFileFilter.Size = new Size(533, 25);
            cbFileFilter.TabIndex = 4;
            cbFileFilter.SelectedIndexChanged += CbFileFilter_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 217);
            label2.Name = "label2";
            label2.Size = new Size(81, 17);
            label2.TabIndex = 3;
            label2.Text = "&F. 按文件筛选";
            // 
            // cbHiddenZero
            // 
            cbHiddenZero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbHiddenZero.AutoSize = true;
            cbHiddenZero.Checked = true;
            cbHiddenZero.CheckState = CheckState.Checked;
            cbHiddenZero.Location = new Point(670, 217);
            cbHiddenZero.Name = "cbHiddenZero";
            cbHiddenZero.Size = new Size(108, 21);
            cbHiddenZero.TabIndex = 5;
            cbHiddenZero.Text = "&H. 隐藏\"0\"项目";
            cbHiddenZero.UseVisualStyleBackColor = true;
            cbHiddenZero.CheckedChanged += CbHiddenZero_CheckedChanged;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(12, 46);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(113, 28);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "&F. 选择文件夹";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += BtnSelectFolder_Click;
            // 
            // lvDialog4
            // 
            lvDialog4.Text = "表情";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 597);
            Controls.Add(cbHiddenZero);
            Controls.Add(label2);
            Controls.Add(cbFileFilter);
            Controls.Add(pbProcess);
            Controls.Add(btnParse);
            Controls.Add(tcMain);
            Controls.Add(btnClearFileList);
            Controls.Add(label1);
            Controls.Add(btnSelectFolder);
            Controls.Add(btnSelectFile);
            Controls.Add(lvFiles);
            Name = "FrmMain";
            Text = "FrmMain";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            tcMain.ResumeLayout(false);
            tcMainTp1.ResumeLayout(false);
            tcMainTp2.ResumeLayout(false);
            tcMainTp3.ResumeLayout(false);
            tcMainTp4.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView lvFiles;
        private ColumnHeader lvFilesCh1;
        private ColumnHeader lvFilesCh2;
        private Button btnSelectFile;
        private Label label1;
        private Button btnClearFileList;
        private TabControl tcMain;
        private TabPage tcMainTp1;
        private TabPage tcMainTp2;
        private Button btnExport;
        private Button btnParse;
        private ProgressBar pbProcess;
        private ListView lvCharacter;
        private ColumnHeader lvCharacterCh1;
        private ColumnHeader lvCharacterCh2;
        private ColumnHeader lvCharacterCh3;
        private ColumnHeader lvCharacterCh4;
        private TabPage tcMainTp3;
        private ColumnHeader lvDialog3;
        private ColumnHeader columnHeader4;
        private ColumnHeader lvAssetCh3;
        private ComboBox cbFileFilter;
        private Label label2;
        private ColumnHeader lvCharacterCh5;
        private ColumnHeader lvCharacterCh6;
        private ListView lvAsset;
        private ColumnHeader lvAssetCh1;
        private ColumnHeader lvAssetCh2;
        private CheckBox cbHiddenZero;
        private ListView lvDialog;
        private ColumnHeader lvDialog1;
        private ColumnHeader lvDialog2;
        private ColumnHeader lvDialog5;
        private ColumnHeader lvDialog6;
        private TabPage tcMainTp4;
        private Button btnSelectFolder;
        private GroupBox groupBox1;
        private Button btnExportFolder;
        private TextBox txtExportFolder;
        private ColumnHeader lvDialog7;
        private ColumnHeader lvDialog4;
    }
}
