using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace Weilai.Core;

/// <summary>
/// 工具类
/// </summary>
public static class FileReader
{
    /// <summary>
    /// 读取纯文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static async Task<string> ReadText(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var content = await reader.ReadToEndAsync().ConfigureAwait(false);
        return content;
    }

    /// <summary>
    /// 读取Docx
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static Task<string> ReadDocx(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        var sb = new StringBuilder();

        using var doc = WordprocessingDocument.Open(stream, false);
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

        return Task.FromResult(sb.ToString());
    }
}
