using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.ObjectModel;
using System.Text;
using Weilai.Datas;

namespace Weilai;

/// <summary>
/// 工具类
/// </summary>
public static class Utils
{
    public static char[] Separator { get; } = ['\r', '\n'];

    public static ReadOnlyDictionary<string, EAssetType> AssetKeyword { get; } = new Dictionary<string, EAssetType>(){
        { "CG" , EAssetType.CG },
        { "特写" , EAssetType.CG },
        { "道具" , EAssetType.Item },
        { "场景" , EAssetType.Scene },
        { "背景" , EAssetType.Scene },
        { "立绘" , EAssetType.Spire },
        { "音效" , EAssetType.Music },
        { "配音" , EAssetType.Voice },
        { "选择" , EAssetType.Other },
        { "判定" , EAssetType.Other },
        { "跳转" , EAssetType.Other },
        { "选项" , EAssetType.Other },
        { "回忆" , EAssetType.Other },
        { "模式" , EAssetType.Other },
        { "剧情" , EAssetType.Other },
        { "画面" , EAssetType.Other },
        { "字体" , EAssetType.Other },
        { "成就" , EAssetType.Other },
        { "结局" , EAssetType.Other },
        { "进入" , EAssetType.Other },
        { "返回" , EAssetType.Other },
        { "设定" , EAssetType.Other },
        { "获取" , EAssetType.Other },
        { "特效" , EAssetType.Other },
        { "失败" , EAssetType.Other },
        { "触发" , EAssetType.Other },
        { "END" , EAssetType.Other },
        { "QTE" , EAssetType.Other },
        { "FLAG" , EAssetType.Other },
        { "倒计时" , EAssetType.Other },
    }.AsReadOnly();

    /// <summary>
    /// 读取纯文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static Task<string> ReadText(string filePath)
    {
        return File.ReadAllTextAsync(filePath);
    }

    /// <summary>
    /// 读取Docx
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static async Task<string> ReadDocx(string filePath)
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

    public static EAssetType GetAssetType(string name)
    {
        foreach (var (key, type) in AssetKeyword)
        {
            if (name.Contains(key))
            {
                return type;
            }
        }

        return EAssetType.None;
    }
}
