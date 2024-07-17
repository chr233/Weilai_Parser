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

        context.FileList.Add(new FileInfoData("人物大全.docx", "C:\\Users\\chr11\\Downloads\\人物大全.docx"));
        context.FileList.Add(new FileInfoData("demo.docx", "C:\\Users\\chr11\\Downloads\\demo.docx"));
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
                wordCount = countInfo.Values.Sum(static x => x.RawWordCount);
                wordCount = countInfo.Values.Sum(static x => x.LineCount);
                wordCount = countInfo.Values.Sum(static x => x.RawLineCount);
            }
            else if (character.CountInfo.TryGetValue(context.SelectedFile, out var countInfo))
            {
                wordCount = countInfo.WordCount;
                wordCount = countInfo.RawWordCount;
                wordCount = countInfo.LineCount;
                wordCount = countInfo.RawLineCount;
            }

            if (wordCount + lineCount > 0)
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
        }
        lvCharacter.EndUpdate();
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
            Filter = "受支持的文件 (*.docx;*.txt;*.rtf)|*.docs;*.rtf;*.txt|所有文件 (*.*)|*.*",
            Multiselect = true,
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

        //扫描剧本, 添加角色和资源, 资源计数
        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                continue;
            }

            string? name = null;
            foreach (var part in parts)
            {
                var rawName = part.ToUpperInvariant();
                if (rawName.StartsWith('"'))
                {
                    break;
                }

                var pureName = regexRawString.Replace(rawName, "");
                if (!string.IsNullOrEmpty(pureName) && pureName.Length <= 5)
                {
                    name = pureName;
                    break;
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                continue;
            }

            var assetType = Utils.GetAssetType(name);

            if (assetType != EAssetType.None)
            {
                if (!assetDict.TryGetValue(name, out var assetInfo))
                {
                    assetInfo = new AssetData(name, assetType);
                    assetDict.Add(name, assetInfo);
                }

                if (!assetInfo.CountInfo.TryGetValue(fileName, out var countInfo))
                {
                    countInfo = 0;
                }

                assetInfo.CountInfo[fileName] = countInfo + 1;
            }
            else
            {
                if (name.StartsWith('"'))
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
            }
        }

        //记录台词
        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                continue;
            }

            List<string> names = [];
            string? say = null;

            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];

                if (part.StartsWith('"'))
                {
                    say = string.Join(' ', parts[i..]);
                    break;
                }
                else
                {
                    var pureName = regexRawString.Replace(part, "");
                    if (!string.IsNullOrEmpty(pureName) && pureName.Length <= 5)
                    {
                        names.Add(pureName);
                    }
                }
            }

            if (string.IsNullOrEmpty(say))
            {
                continue;
            }



            //if (name.StartsWith('#'))
            //{
            //    name = name[1..];
            //}

            //if (!Utils.AssetKeyword.Contains(name[..2]))
            //{
            //    var pinyin = WordsHelper.GetFirstPinyin(name);

            //    if (pinyin == "CNE")
            //    {
            //        pinyin = "CNR";
            //    }

            //    if (!characterNameDict.TryGetValue(name, out var character) && !characterPyDict.TryGetValue(pinyin, out character))
            //    {
            //        character = new CharacterData(name, pinyin);
            //        characterNameDict.TryAdd(name, character);
            //        characterPyDict.TryAdd(pinyin, character);
            //    }

            //    if (body.StartsWith('"') && body.EndsWith('"'))
            //    {
            //        var say = body[1..^1];

            //        //character.WordCount += say.Length;
            //        //character.LineCount++;

            //        //Debug.WriteLine(line);
            //    }
            //    else
            //    {
            //        //Debug.WriteLine(line);

            //    }
            //}
            //else
            //{
            //    var type = name switch {
            //        "场景" => EAssetType.Scene,
            //        "音效" => EAssetType.Music,
            //        "道具" => EAssetType.Item,
            //        "立绘" => EAssetType.Spire,
            //        "CG" => EAssetType.CG,
            //        _ => EAssetType.None,
            //    };

            //    //assetList.Add(new AssetData(type, body, 1));
            //}
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
