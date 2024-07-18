using DynamicData;
using System.Diagnostics;
using ToolGood.Words.FirstPinyin;
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

        tabControl1.TabIndex = 0;
    }

    private void BindControls()
    {
        // 绑定数据
        context.FileList.Connect().Subscribe(OnFileListChange);
        context.CharacterList.Connect().Subscribe(OnCharacterListChange);
        context.AssetList.Connect().Subscribe(OnAssetListChange);

        pbProcess.DataBindings.Add(nameof(pbProcess.Value), context, nameof(context.ProgressValue));
        pbProcess.DataBindings.Add(nameof(pbProcess.Maximum), context, nameof(context.ProgressMax));

        btnSelectFile.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnClearFileList.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnExport.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
        btnParse.DataBindings.Add(nameof(btnClearFileList.Enabled), context, nameof(context.AllowOperate));
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
                var item = new ListViewItem {
                    Text = character.FullName,
                    SubItems = {
                        character.PinYinName,
                        dialog.LineId.ToString(),
                        dialog.Content,
                        dialog.RawContent,
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
        };

        if (dialog.ShowDialog() == DialogResult.OK && dialog.FileNames.Length > 0)
        {
            context.FileList.Edit(list => {
                var paths = list.Select(static x => x.Path).ToHashSet();
                var newList = new List<FileInfoData>();

                foreach (var filePath in dialog.FileNames)
                {
                    if (!paths.Contains(filePath))
                    {
                        var fileName = Path.GetFileName(filePath);
                        list.Add(new FileInfoData(fileName, filePath));
                    }
                }
            });
        }
    }

    private async void BtnExport_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog {
            ShowNewFolderButton = true,
            AutoUpgradeEnabled = true,
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var path = dialog.SelectedPath;

            await Utils.ExportSummary(path, context.SelectedFile, context.CharacterList.Items, context.AssetList.Items).ConfigureAwait(false);
            await Utils.ExportDetail(path, context.SelectedFile, context.CharacterList.Items, context.AssetList.Items).ConfigureAwait(false);
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
                        if (!paths.Contains(filePath))
                        {
                            var fileName = Path.GetFileName(filePath);
                            list.Add(new FileInfoData(fileName, filePath));
                        }
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
                        ".TXT" => Utils.ReadText(filePath),
                        ".RTF" => Utils.ReadText(filePath),
                        ".DOCX" => Utils.ReadDocx(filePath),
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

                    JubenParser(fileInfo.Name, content);
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

    //================================================================

    /// <summary>
    /// 解析剧本
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    private void JubenParser(string fileName, string fileContent)
    {
        if (string.IsNullOrEmpty(fileContent))
        {
            return;
        }

        var lines = fileContent.Split(Utils.Separator, StringSplitOptions.RemoveEmptyEntries);

        var characterNameDict = context.CharacterList.Items.ToDictionary(static x => x.FullName, static x => x);
        var characterPyDict = context.CharacterList.Items.ToDictionary(static x => x.PinYinName, static x => x);

        var assetDict = context.AssetList.Items.ToDictionary(static x => x.Name, static x => x);

        var regexRawString = RegexUtils.MatchRawString();

        //第一遍扫描剧本, 添加角色和资源
        foreach (var line in lines)
        {
            var trimedStart = line.Trim();

            if (string.IsNullOrEmpty(trimedStart) || (!trimedStart.Contains('#') && !trimedStart.Contains('@')))
            {
                continue;
            }

            trimedStart = trimedStart[1..].Trim();

            var temp = trimedStart.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var keyword = temp.FirstOrDefault("");

            if (string.IsNullOrEmpty(keyword))
            {
                continue;
            }

            var assetType = Utils.GetAssetType(keyword);

            if (assetType != EAssetType.None)
            {
                //判断为资源
                if (!assetDict.TryGetValue(trimedStart, out var assetInfo))
                {
                    assetInfo = new AssetData(trimedStart, assetType);
                    assetDict.Add(trimedStart, assetInfo);
                }

                if (!assetInfo.CountInfo.TryGetValue(fileName, out var countInfo))
                {
                    countInfo = 0;
                }

                assetInfo.CountInfo[fileName] = countInfo + 1;
            }
            else
            {
                //判断为角色
                var parts = line[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                {
                    continue;
                }

                var name = parts[0];
                if (name.Length == 0 || name.Length > 6)
                {
                    continue;
                }

                var pinyin = WordsHelper.GetFirstPinyin(name);

                if (pinyin == "CNE")
                {
                    pinyin = "CNR";
                }

                if (!characterNameDict.TryGetValue(name, out var character) &&
                    !characterPyDict.TryGetValue(pinyin, out character))
                {
                    character = new CharacterData(name, pinyin);

                    characterNameDict.TryAdd(name, character);
                    characterPyDict.TryAdd(pinyin, character);
                }

                character.CountInfo.TryAdd(fileName, new CountInfoData());

                for (var i = 1; i < parts.Length; i++)
                {
                    var part = parts[i];
                    if (part.StartsWith('"'))
                    {
                        break;
                    }

                    character.Emojis.Add(part);
                }
            }
        }

        //解析台词
        for (var lineId = 0; lineId < lines.Length; lineId++)
        {
            var line = lines[lineId];
            var parts = line.Trim().Split('"', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                continue;
            }

            var strNames = parts[0].ToUpperInvariant();
            var names = strNames.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<CharacterData> charactersList = [];
            foreach (var name in names)
            {
                if (characterNameDict.TryGetValue(name, out var character) ||
                    characterPyDict.TryGetValue(name, out character))
                {
                    charactersList.Add(character);
                }
            }

            if (charactersList.Count == 0)
            {
                continue;
            }

            var say = parts[1];
            if (say.EndsWith('"'))
            {
                say = say[..^1];
            }

            var rawSay = regexRawString.Replace(say, "");

            if (say.Length + rawSay.Length > 0)
            {
                foreach (var character in charactersList)
                {
                    if (character.CountInfo.TryGetValue(fileName, out var countInfo))
                    {
                        if (say.Length > 0)
                        {
                            countInfo.WordCount += say.Length;
                            countInfo.LineCount++;
                        }

                        if (rawSay.Length > 0)
                        {
                            countInfo.RawWordCount += rawSay.Length;
                            countInfo.RawLineCount++;
                        }

                        var dialog = new CharacterDialogData(lineId, say, rawSay);
                        countInfo.Dialogs.Add(dialog);
                    }
                }
            }
        }

        context.CharacterList.Edit(list => {
            list.Clear();
            list.AddRange(characterNameDict.Values);
        });

        context.AssetList.Edit(x => {
            x.Clear();
            x.AddRange(assetDict.Values);
        });
    }
}
