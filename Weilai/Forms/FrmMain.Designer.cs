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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            lvCharacter = new ListView();
            lvCharacterCh1 = new ColumnHeader();
            lvCharacterCh2 = new ColumnHeader();
            lvCharacterCh3 = new ColumnHeader();
            lvCharacterCh4 = new ColumnHeader();
            lvCharacterCh5 = new ColumnHeader();
            lvCharacterCh6 = new ColumnHeader();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            lvAsset = new ListView();
            lvAssetCh1 = new ColumnHeader();
            lvAssetCh2 = new ColumnHeader();
            lvAssetCh3 = new ColumnHeader();
            btnExport = new Button();
            btnParse = new Button();
            pbProcess = new ProgressBar();
            cbFileFilter = new ComboBox();
            label2 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // lvFiles
            // 
            lvFiles.AllowDrop = true;
            lvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvFiles.Columns.AddRange(new ColumnHeader[] { lvFilesCh1, lvFilesCh2 });
            lvFiles.Location = new Point(131, 12);
            lvFiles.Name = "lvFiles";
            lvFiles.Size = new Size(540, 179);
            lvFiles.TabIndex = 1;
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
            btnSelectFile.TabIndex = 2;
            btnSelectFile.Text = "选择文件";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += BtnSelectFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.AppWorkspace;
            label1.Location = new Point(40, 43);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 3;
            label1.Text = "支持拖拽";
            // 
            // btnClearFileList
            // 
            btnClearFileList.Location = new Point(12, 64);
            btnClearFileList.Name = "btnClearFileList";
            btnClearFileList.Size = new Size(113, 28);
            btnClearFileList.TabIndex = 4;
            btnClearFileList.Text = "清空列表";
            btnClearFileList.UseVisualStyleBackColor = true;
            btnClearFileList.Click += BtnClearFileList_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(12, 244);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(659, 341);
            tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lvCharacter);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(651, 311);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "角色信息统计";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvCharacter
            // 
            lvCharacter.Columns.AddRange(new ColumnHeader[] { lvCharacterCh1, lvCharacterCh2, lvCharacterCh3, lvCharacterCh4, lvCharacterCh5, lvCharacterCh6 });
            lvCharacter.Dock = DockStyle.Fill;
            lvCharacter.FullRowSelect = true;
            lvCharacter.Location = new Point(3, 3);
            lvCharacter.Name = "lvCharacter";
            lvCharacter.Size = new Size(645, 305);
            lvCharacter.TabIndex = 0;
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
            lvCharacterCh5.Text = "行数";
            lvCharacterCh5.Width = 100;
            // 
            // lvCharacterCh6
            // 
            lvCharacterCh6.Text = "行数(不含标点)";
            lvCharacterCh6.Width = 100;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(651, 552);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "台词信息";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(lvAsset);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(651, 552);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "资源列表";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvAsset
            // 
            lvAsset.Columns.AddRange(new ColumnHeader[] { lvAssetCh1, lvAssetCh2, lvAssetCh3 });
            lvAsset.Dock = DockStyle.Fill;
            lvAsset.FullRowSelect = true;
            lvAsset.Location = new Point(3, 3);
            lvAsset.Name = "lvAsset";
            lvAsset.Size = new Size(645, 546);
            lvAsset.TabIndex = 0;
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
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(558, 213);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(113, 25);
            btnExport.TabIndex = 6;
            btnExport.Text = "导出结果";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnParse
            // 
            btnParse.Location = new Point(12, 150);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(113, 41);
            btnParse.TabIndex = 7;
            btnParse.Text = "解析文件";
            btnParse.UseVisualStyleBackColor = true;
            btnParse.Click += BtnParse_Click;
            // 
            // pbProcess
            // 
            pbProcess.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbProcess.Location = new Point(12, 197);
            pbProcess.Name = "pbProcess";
            pbProcess.Size = new Size(659, 10);
            pbProcess.TabIndex = 8;
            // 
            // cbFileFilter
            // 
            cbFileFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbFileFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFileFilter.ItemHeight = 17;
            cbFileFilter.Location = new Point(131, 213);
            cbFileFilter.Name = "cbFileFilter";
            cbFileFilter.Size = new Size(421, 25);
            cbFileFilter.TabIndex = 9;
            cbFileFilter.SelectedIndexChanged += CbFileFilter_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 217);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 10;
            label2.Text = "按文件筛选";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 597);
            Controls.Add(label2);
            Controls.Add(cbFileFilter);
            Controls.Add(pbProcess);
            Controls.Add(btnParse);
            Controls.Add(btnExport);
            Controls.Add(tabControl1);
            Controls.Add(btnClearFileList);
            Controls.Add(label1);
            Controls.Add(btnSelectFile);
            Controls.Add(lvFiles);
            Name = "FrmMain";
            Text = "FrmMain";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
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
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnExport;
        private Button btnParse;
        private ProgressBar pbProcess;
        private ListView lvCharacter;
        private ColumnHeader lvCharacterCh1;
        private ColumnHeader lvCharacterCh2;
        private ColumnHeader lvCharacterCh3;
        private ColumnHeader lvCharacterCh4;
        private TabPage tabPage3;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader lvAssetCh3;
        private ComboBox cbFileFilter;
        private Label label2;
        private ColumnHeader lvCharacterCh5;
        private ColumnHeader lvCharacterCh6;
        private ListView lvAsset;
        private ColumnHeader lvAssetCh1;
        private ColumnHeader lvAssetCh2;
    }
}
