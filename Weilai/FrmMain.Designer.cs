namespace Weilai
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
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            tabPage2 = new TabPage();
            btnExport = new Button();
            btnParse = new Button();
            pbProcess = new ProgressBar();
            listView2 = new ListView();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            listView3 = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            listView4 = new ListView();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // lvFiles
            // 
            lvFiles.AllowDrop = true;
            lvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lvFiles.Columns.AddRange(new ColumnHeader[] { lvFilesCh1, lvFilesCh2 });
            lvFiles.Location = new Point(131, 12);
            lvFiles.Name = "lvFiles";
            lvFiles.Size = new Size(683, 179);
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
            lvFilesCh2.Width = 400;
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
            tabControl1.Location = new Point(12, 213);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(802, 644);
            tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listView4);
            tabPage1.Controls.Add(listView3);
            tabPage1.Controls.Add(listView2);
            tabPage1.Controls.Add(listView1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(794, 614);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "角色信息";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.Location = new Point(6, 6);
            listView1.Name = "listView1";
            listView1.Size = new Size(341, 244);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader2
            // 
            columnHeader2.Width = 500;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(794, 614);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "台词";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(12, 129);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(113, 28);
            btnExport.TabIndex = 6;
            btnExport.Text = "导出结果";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnParse
            // 
            btnParse.Location = new Point(12, 163);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(113, 28);
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
            pbProcess.Size = new Size(802, 10);
            pbProcess.TabIndex = 8;
            // 
            // listView2
            // 
            listView2.Columns.AddRange(new ColumnHeader[] { columnHeader3, columnHeader4 });
            listView2.Location = new Point(6, 267);
            listView2.Name = "listView2";
            listView2.Size = new Size(341, 244);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Width = 500;
            // 
            // listView3
            // 
            listView3.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6 });
            listView3.Location = new Point(375, 6);
            listView3.Name = "listView3";
            listView3.Size = new Size(341, 244);
            listView3.TabIndex = 2;
            listView3.UseCompatibleStateImageBehavior = false;
            listView3.View = View.Details;
            // 
            // columnHeader6
            // 
            columnHeader6.Width = 500;
            // 
            // listView4
            // 
            listView4.Columns.AddRange(new ColumnHeader[] { columnHeader7, columnHeader8 });
            listView4.Location = new Point(375, 267);
            listView4.Name = "listView4";
            listView4.Size = new Size(341, 244);
            listView4.TabIndex = 3;
            listView4.UseCompatibleStateImageBehavior = false;
            listView4.View = View.Details;
            // 
            // columnHeader8
            // 
            columnHeader8.Width = 500;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 869);
            Controls.Add(pbProcess);
            Controls.Add(btnParse);
            Controls.Add(btnExport);
            Controls.Add(tabControl1);
            Controls.Add(btnClearFileList);
            Controls.Add(label1);
            Controls.Add(btnSelectFile);
            Controls.Add(lvFiles);
            Name = "FrmMain";
            Text = "Form ";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
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
        private ListView listView1;
        private Button btnExport;
        private Button btnParse;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ProgressBar pbProcess;
        private ListView listView4;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ListView listView3;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ListView listView2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
    }
}
