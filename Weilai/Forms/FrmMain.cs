using DynamicData;
using System.Diagnostics;
using Weilai.Core;
using Weilai.Datas;
using Weilai.VIewModels;

namespace Weilai.Forms;

public partial class FrmMain : Form
{
    private readonly FrmMainViewModel context;

    public FrmMain()
    {
        InitializeComponent();

        context = new FrmMainViewModel();
        BindControls();

        Text = $"转换工具 By chr_ {BuildInfo.Copyright} Ver {BuildInfo.Version} {BuildInfo.Configuration}";

#if DEBUG
        context.FileList.Add(new FileInfoData("人物大全.docx", "C:\\Users\\chr11\\Downloads\\人物大全.docx"));
        context.FileList.Add(new FileInfoData("demo.docx", "C:\\Users\\chr11\\Downloads\\demo.docx"));
#endif

        tcMain.TabIndex = 0;
    }

    private void BindControls()
    {
        // 绑定数据
        context.FileList.Connect().Subscribe(OnFileListChange);
        context.CharacterList.Connect().Subscribe(OnCharacterListChange);
        context.AssetList.Connect().Subscribe(OnAssetListChange);
        context.ExportFileList.Connect().Subscribe(OnExportFileListChange);

        pbProcess.DataBindings.Add(nameof(pbProcess.Value), context, nameof(context.ProgressValue));
        pbProcess.DataBindings.Add(nameof(pbProcess.Maximum), context, nameof(context.ProgressMax));

        btnSelectFile.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnSelectFolder.DataBindings.Add(nameof(btnSelectFolder.Enabled), context, nameof(context.AllowOperate));
        btnClearFileList.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnExport.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnParse.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        cbFileFilter.DataBindings.Add(nameof(cbFileFilter.Enabled), context, nameof(context.AllowOperate));

        txtExportFolder.DataBindings.Add(nameof(txtExportFolder.Text), context, nameof(context.ExportFolder));
    }

    private void OnFileListChange(IChangeSet<FileInfoData> e)
    {
        var oldSelector = cbFileFilter.SelectedIndex;
        cbFileFilter.BeginUpdate();
        cbFileFilter.Items.Clear();

        cbFileFilter.Items.Add("-- 全部文件 --");

        lvFiles.BeginUpdate();
        lvFiles.Items.Clear();
        foreach (var fi in context.FileList.Items)
        {
            var item = new ListViewItem {
                Text = fi.Name,
                SubItems = { fi.Path }
            };

            lvFiles.Items.Add(item);
            cbFileFilter.Items.Add(fi.Name);
        }
        lvFiles.EndUpdate();
        cbFileFilter.EndUpdate();

        cbFileFilter.SelectedIndex = Math.Max(Math.Min(oldSelector, cbFileFilter.Items.Count - 1), 0);

        context.ProgressMax = context.FileList.Count;
    }

    private void OnExportFileListChange(IChangeSet<string> e)
    {
        lvExportFile.BeginUpdate();
        lvExportFile.Items.Clear();
        foreach (var str in context.ExportFileList.Items)
        {
            var item = new ListViewItem(str);
            lvExportFile.Items.Add(item);
        }
        lvExportFile.EndUpdate();
    }

    private void OnCharacterListChange(IChangeSet<CharacterData> e)
    {
        lvCharacter.BeginUpdate();
        lvCharacter.Items.Clear();

        lvDialog.BeginUpdate();
        lvDialog.Items.Clear();


        foreach (var character in context.CharacterList.Items)
        {
            long wordCount = 0;
            long rawWordCount = 0;
            long lineCount = 0;
            long rawLineCount = 0;

            if (string.IsNullOrEmpty(context.SelectedFile))
            {
                var countInfo = character.CountInfo;

                wordCount = countInfo.Values.Sum(static x => x.WordCount);
                rawWordCount = countInfo.Values.Sum(static x => x.RawWordCount);
                lineCount = countInfo.Values.Sum(static x => x.LineCount);
                rawLineCount = countInfo.Values.Sum(static x => x.RawLineCount);
            }
            else if (character.CountInfo.TryGetValue(context.SelectedFile, out var countInfo))
            {
                wordCount = countInfo.WordCount;
                rawWordCount = countInfo.RawWordCount;
                lineCount = countInfo.LineCount;
                rawLineCount = countInfo.RawLineCount;
            }

            if (!context.HiddenZero || wordCount + lineCount > 0)
            {
                var item = new ListViewItem {
                    Text = character.FullName,
                    SubItems = {
                        character.PinYinName,
                        wordCount.ToString(),
                        rawWordCount.ToString(),
                        lineCount.ToString(),
                        rawLineCount.ToString(),
                    },
                };

                lvCharacter.Items.Add(item);
            }

            List<CharacterDialogData> dialogs = [];
            if (string.IsNullOrEmpty(context.SelectedFile))
            {
                foreach (var countInfo in character.CountInfo.Values)
                {
                    dialogs.AddRange(countInfo.Dialogs);
                }
            }
            else
            {
                if (character.CountInfo.TryGetValue(context.SelectedFile, out var countInfo))
                {
                    dialogs.AddRange(countInfo.Dialogs);
                }
            }

            foreach (var dialog in dialogs)
            {
                var count = dialog.Content?.Length ?? 0;
                var rawCount = dialog.RawContent?.Length ?? 0;

                if (rawCount == 0)
                {
                    count = 0;
                }

                var item = new ListViewItem {
                    Text = character.FullName,
                    SubItems = {
                        character.PinYinName,
                        dialog.LineId.ToString(),
                        dialog.Emoji,
                        dialog.Content,
                        dialog.RawContent,
                        string.Format("{0} ({1})",count,rawCount),
                    },
                };

                lvDialog.Items.Add(item);
            }
        }

        lvCharacter.EndUpdate();
        lvDialog.EndUpdate();
    }

    private void OnAssetListChange(IChangeSet<AssetData> e)
    {
        lvAsset.BeginUpdate();
        lvAsset.Items.Clear();
        foreach (var asset in context.AssetList.Items.OrderBy(static x => x.Type))
        {
            long count = 0;

            if (string.IsNullOrEmpty(context.SelectedFile))
            {
                var countInfo = asset.CountInfo;
                count = countInfo.Values.Sum(static x => x);
            }
            else if (asset.CountInfo.TryGetValue(context.SelectedFile, out var countInfo))
            {
                count = countInfo;
            }

            var item = new ListViewItem {
                Text = asset.Type.ToString(),
                SubItems = {
                    asset.Name,
                    count.ToString(),
                },
            };

            lvAsset.Items.Add(item);
        }
        lvAsset.EndUpdate();
    }

    //================================================================

    private void BtnSelectFile_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog {
            CheckPathExists = true,
            Filter = "受支持的文件 (*.docx;*.txt;*.rtf)|*.docs;*.rtf;*.txt|所有文件 (*.*)|*.*",
            Multiselect = true,
            AutoUpgradeEnabled = true,
            AddToRecent = false,
        };

        if (!string.IsNullOrEmpty(context.ImportInitFolder))
        {
            dialog.InitialDirectory = context.ImportInitFolder;
        }

        if (dialog.ShowDialog() == DialogResult.OK && dialog.FileNames.Length > 0)
        {
            context.FileList.Edit(list => {
                var paths = list.Select(static x => x.Path).ToHashSet();

                foreach (var filePath in dialog.FileNames)
                {
                    if (filePath.StartsWith("~$") || paths.Contains(filePath))
                    {
                        continue;
                    }

                    var ext = Path.GetExtension(filePath).ToUpperInvariant();

                    if (ext == ".TXT" || ext == ".RTF" || ext == ".DOCX")
                    {
                        var fileName = Path.GetFileName(filePath);
                        list.Add(new FileInfoData(fileName, filePath));
                    }
                }
            });
        }
    }

    private void BtnSelectFolder_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog {
            ShowNewFolderButton = true,
            AutoUpgradeEnabled = true,
            AddToRecent = false,
        };

        if (!string.IsNullOrEmpty(context.ImportInitFolder))
        {
            dialog.InitialDirectory = context.ImportInitFolder;
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var path = dialog.SelectedPath;

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                context.FileList.Edit(list => {
                    var paths = list.Select(static x => x.Path).ToHashSet();

                    foreach (var filePath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                    {
                        if (filePath.StartsWith("~$") || paths.Contains(filePath))
                        {
                            continue;
                        }

                        var fileName = Path.GetFileName(filePath);
                        list.Add(new FileInfoData(fileName, filePath));
                    }
                });
            }
        }
    }

    private void BtnExportFolder_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog {
            ShowNewFolderButton = true,
            AutoUpgradeEnabled = true,
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            context.ExportFolder = dialog.SelectedPath;
        }
    }

    private void BtnExport_Click(object sender, EventArgs e)
    {
        if (context.CharacterList.Count + context.AssetList.Count == 0)
        {
            MessageBox.Show(this, "请先解析文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!Directory.Exists(context.ExportFolder))
        {
            MessageBox.Show(this, "导出路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var filePath = context.ExportFolder;
        var fileList = context.FileList;
        var characterList = context.CharacterList;
        var assetList = context.AssetList;
        var exportFileList = context.ExportFileList;

        try
        {
            exportFileList.Clear();

            FileDumper.ExportAssets(filePath, fileList, assetList, exportFileList);
            FileDumper.ExportCharacterSummary(filePath, fileList, characterList, assetList, exportFileList);

            foreach (var fileInfo in fileList.Items)
            {
                FileDumper.ExportCharacter(filePath, fileInfo.Name, characterList, assetList, exportFileList);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private void BtnClearFileList_Click(object sender, EventArgs e)
    {
        context.FileList.Clear();
    }

    private void LvFiles_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = (e.Data?.GetDataPresent(DataFormats.FileDrop) == true) ? DragDropEffects.Copy : DragDropEffects.None;
    }

    private void LvFiles_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data != null)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data is string[] filePaths && filePaths.Length > 0)
            {
                context.FileList.Edit(list => {
                    var paths = list.Select(static x => x.Path).ToHashSet();

                    foreach (var filePath in filePaths)
                    {
                        if (filePath.StartsWith("~$") || paths.Contains(filePath))
                        {
                            continue;
                        }

                        var fileName = Path.GetFileName(filePath);
                        list.Add(new FileInfoData(fileName, filePath));
                    }
                });
            }
        }
    }

    private void CbHiddenZero_CheckedChanged(object sender, EventArgs e)
    {
        context.HiddenZero = cbHiddenZero.Checked;

        OnCharacterListChange(new ChangeSet<CharacterData>());
        OnAssetListChange(new ChangeSet<AssetData>());
    }

    private void CbFileFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbFileFilter.SelectedIndex > 0)
        {
            context.SelectedFile = cbFileFilter.SelectedItem as string;
        }
        else
        {
            context.SelectedFile = null;
        }

        OnCharacterListChange(new ChangeSet<CharacterData>());
        OnAssetListChange(new ChangeSet<AssetData>());
    }

    private async void BtnParse_Click(object sender, EventArgs e)
    {
        try
        {
            context.AllowOperate = false;
            context.ProgressValue = 0;
            context.CharacterList.Clear();
            context.AssetList.Clear();

            if (context.FileList.Count == 0)
            {
                MessageBox.Show("未选择任何文件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var fileInfo in context.FileList.Items)
            {
                try
                {
                    var filePath = fileInfo.Path;
                    var extension = Path.GetExtension(filePath).ToUpper();

                    var handler = extension switch {
                        ".TXT" => FileReader.ReadText(filePath),
                        ".RTF" => FileReader.ReadText(filePath),
                        ".DOCX" => FileReader.ReadDocx(filePath),
                        _ => null,
                    };

                    if (handler == null)
                    {
                        MessageBox.Show($"不支持的文件类型: {extension}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

#pragma warning disable CAC002 // ConfigureAwaitChecker
                    var content = await handler.ConfigureAwait(true);
#pragma warning restore CAC002 // ConfigureAwaitChecker

                    ContentParser.JubenParser(fileInfo.Name, content, context.CharacterList, context.AssetList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "处理文件时出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex);
                }
                finally
                {
                    context.ProgressValue++;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "处理文件时出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Debug.WriteLine(ex);
        }
        finally
        {
            context.AllowOperate = true;
        }
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        var config = AppConfig.Default;

        context.ImportInitFolder = config.ImportFolder;
        context.ExportFolder = config.ExportFolder;
        context.HiddenZero = config.HiddenZero;

        cbHiddenZero.Checked = context.HiddenZero;
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        var config = AppConfig.Default;

        config.ImportFolder = context.ImportInitFolder;
        config.ExportFolder = context.ExportFolder;
        config.HiddenZero = context.HiddenZero;

        config.Save();

        context.FileList.Dispose();
        context.AssetList.Dispose();
        context.CharacterList.Dispose();
        context.ExportFileList.Dispose();
    }

    private void FrmMain_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = (e.Data?.GetDataPresent(DataFormats.FileDrop) == true) ? DragDropEffects.Copy : DragDropEffects.None;
    }

    private void FrmMain_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data != null)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data is string[] filePaths && filePaths.Length > 0)
            {
                context.FileList.Edit(list => {
                    var paths = list.Select(static x => x.Path).ToHashSet();

                    foreach (var filePath in filePaths)
                    {
                        if (filePath.StartsWith("~$") || paths.Contains(filePath))
                        {
                            continue;
                        }

                        var fileName = Path.GetFileName(filePath);
                        list.Add(new FileInfoData(fileName, filePath));
                    }
                });
            }
        }
    }
}
