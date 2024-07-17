using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DynamicData;
using System.Diagnostics;
using System.Text;
using ToolGood.Words.FirstPinyin;
using Weilai.Datas;
using Weilai.VIewModels;

namespace Weilai;

public partial class FrmMain : Form
{
    private readonly FrmMainViewModel context;



    public FrmMain()
    {
        InitializeComponent();

        context = new FrmMainViewModel();
        BindControls();

        Text = $"转换工具 v{Utils.Version} By chr_";

        context.FileList.Add(new FileInfoData("人物大全.docx", "C:\\Users\\chr11\\Downloads\\人物大全.docx"));
        //context.FileList.Add(new FileInfoData("demo.docx", "C:\\Users\\chr11\\Downloads\\demo.docx"));
    }

    private void BindControls()
    {
        // 绑定数据
        context.FileList.Connect().Subscribe(OnFileListChange);
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
        Debug.WriteLine("OnFileListChange");

        lvFiles.BeginUpdate();
        lvFiles.Items.Clear();
        foreach (var fi in context.FileList.Items)
        {
            var item = new ListViewItem {
                Text = fi.Name,
                SubItems = { fi.Path }
            };
            lvFiles.Items.Add(item);
        }
        lvFiles.EndUpdate();

        context.ProgressMax = context.FileList.Count;
    }

    private void OnAssetListChange(IChangeSet<AssetData> e)
    {
        listView1.Items.Clear();
        listView2.Items.Clear();
        listView3.Items.Clear();
        listView4.Items.Clear();
        foreach (var asset in context.AssetList.Items)
        {
            var item = new ListViewItem {
                Text = asset.Type.ToString(),
                SubItems = { asset.Name, asset.Count.ToString() }
            };

            var lv = asset.Type switch {
                EAssetType.CG => listView1,
                EAssetType.Scene => listView2,
                EAssetType.Music => listView3,
                EAssetType.Item => listView4,
                _ => null,
            };

            lv?.Items.Add(item);
        }
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
            context.FileList.Edit(fileList => {
                var paths = fileList.Select(static x => x.Path).ToHashSet();
                var newList = new List<FileInfoData>();

                foreach (var filePath in dialog.FileNames)
                {
                    if (!paths.Contains(filePath))
                    {
                        var fileName = Path.GetFileName(filePath);
                        newList.Add(new FileInfoData(fileName, filePath));
                    }
                }

                fileList.Clear();
                fileList.AddRange(newList);
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
                context.FileList.Edit(fileList => {
                    var paths = fileList.Select(static x => x.Path).ToHashSet();
                    var newList = new List<FileInfoData>();

                    foreach (var filePath in filePaths)
                    {
                        if (!paths.Contains(filePath))
                        {
                            var fileName = Path.GetFileName(filePath);
                            newList.Add(new FileInfoData(fileName, filePath));
                        }
                    }

                    fileList.Clear();
                    fileList.AddRange(newList);
                });
            }
        }
    }

    private async void BtnParse_Click(object sender, EventArgs e)
    {
        try
        {
            context.AllowOperate = false;
            context.ProgressValue = 0;
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
                        ".TXT" => ReadText(filePath),
                        ".RTF" => ReadText(filePath),
                        ".DOCX" => ReadDocx(filePath),
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

                    JubenParser(content);
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

    private Task<string> ReadText(string filePath)
    {
        return File.ReadAllTextAsync(filePath);
    }

    private async Task<string> ReadDocx(string filePath)
    {
        var bytes = await File.ReadAllBytesAsync(filePath).ConfigureAwait(false);
        var ms = new MemoryStream(bytes);
        var sb = new StringBuilder();

        using var doc = WordprocessingDocument.Open(ms, false);
        var body = doc.MainDocumentPart?.Document?.Body;

        if (body != null)
        {
            foreach (var para in body.Elements<Paragraph>())
            {
                foreach (var run in para.Elements<Run>())
                {
                    foreach (var text in run.Elements<Text>())
                    {
                        sb.Append(text.Text);
                    }
                }
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }



    private void JubenParser(string fileContent)
    {
        if (string.IsNullOrEmpty(fileContent))
        {
            return;
        }

        var lines = fileContent.Split(Utils.Separator, StringSplitOptions.RemoveEmptyEntries);

        var characterNameDict = context.CharacterList.Items.ToDictionary(static x => x.FullName, static x => x);
        var characterPinyinDict = context.CharacterList.Items.ToDictionary(static x => x.PinYinName, static x => x);

        var assetList = new List<AssetData>();

        foreach (var line in lines)
        {
            var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                continue;
            }

            var name = parts[0].ToUpperInvariant();
            var body = parts[1];

            if (name.StartsWith('#'))
            {
                name = name[1..];

                var pinyin = WordsHelper.GetFirstPinyin(name);

                if (pinyin == "CNE")
                {
                    pinyin = "CNR";
                }

                if (!characterNameDict.TryGetValue(name, out var character) && !characterPinyinDict.TryGetValue(pinyin, out character))
                {
                    character = new CharacterData(name, pinyin);
                    characterNameDict.TryAdd(name, character);
                    characterPinyinDict.TryAdd(pinyin, character);
                }
            }
        }


        foreach (var line in lines)
        {
            var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                continue;
            }

            var name = parts[0].ToUpperInvariant();
            var body = parts[1];

            if (name.StartsWith('#'))
            {
                name = name[1..];
            }

            if (!Utils.NoneCharacters.Contains(name[..2]))
            {
                var pinyin = WordsHelper.GetFirstPinyin(name);

                if (pinyin == "CNE")
                {
                    pinyin = "CNR";
                }

                if (!characterNameDict.TryGetValue(name, out var character) && !characterPinyinDict.TryGetValue(pinyin, out character))
                {
                    character = new CharacterData(name, pinyin);
                    characterNameDict.TryAdd(name, character);
                    characterPinyinDict.TryAdd(pinyin, character);
                }

                if (body.StartsWith('"') && body.EndsWith('"'))
                {
                    var say = body[1..^1];

                    character.WordCount += say.Length;
                    character.LineCount++;

                    Debug.WriteLine(line);
                }
                else
                {
                    Debug.WriteLine(line);

                }
            }
            else
            {
                var type = name switch {
                    "场景" => EAssetType.Scene,
                    "音效" => EAssetType.Music,
                    "道具" => EAssetType.Item,
                    "立绘" => EAssetType.Spire,
                    "CG" => EAssetType.CG,
                    _ => EAssetType.None,
                };

                assetList.Add(new AssetData(type, body, 1));
            }
        }

        context.AssetList.Edit(x => {
            x.Clear();
            x.AddRange(assetList);

        });
    }
}
