using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.ObjectModel;
using System.Text;
using Weilai.Datas;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace Weilai;

/// <summary>
/// 工具类
/// </summary>
public static class Utils
{
    public static char[] Separator { get; } = ['\r', '\n'];

    public static ReadOnlyDictionary<string, EAssetType> AssetKeyword { get; } = new Dictionary<string, EAssetType>(){
        { "CG"  , EAssetType.CG },
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
        { "视角" , EAssetType.Other },
        { "返回" , EAssetType.Other },
        { "设定" , EAssetType.Other },
        { "获取" , EAssetType.Other },
        { "特效" , EAssetType.Other },
        { "失败" , EAssetType.Other },
        { "触发" , EAssetType.Other },
        { "主线" , EAssetType.Other },
        { "支线" , EAssetType.Other },
        { "镜头" , EAssetType.Other },
        { "时间" , EAssetType.Other },
        { "点击" , EAssetType.Other },
        { "提示" , EAssetType.Other },
        { "问"  , EAssetType.Other },
        { "D"  , EAssetType.Other },
        { "PV"  , EAssetType.Other },
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

    /// <summary>
    /// 获取资源类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static EAssetType GetAssetType(string name)
    {
        if (name.Length > 5)
        {
            return EAssetType.Unknown;
        }

        name = name.ToUpperInvariant();
        foreach (var (key, type) in AssetKeyword)
        {
            if (name.Contains(key))
            {
                return type;
            }
        }

        return EAssetType.None;
    }

    /// <summary>
    /// 导出数据
    /// </summary>
    /// <param name="filePath"></param>
    public static async Task ExportSummary(string folderPath, string? fileFilter, IEnumerable<CharacterData> characters, IEnumerable<AssetData> assets)
    {
        var fileName = "概要.xlsx";
        var filePath = Path.Combine(folderPath, fileName);

        if (File.Exists(filePath) &&
            MessageBox.Show("文件 " + filePath + " 已存在, 是否覆盖?", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            return;
        }

        //using var document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);
        //var workbookPart = document.AddWorkbookPart();
        //workbookPart.Workbook = new Workbook();

        //var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        //worksheetPart.Worksheet = new Worksheet(new SheetData());

        //foreach (var character in characters)
        //{

        //}
        //// 添加一个WorkbookPart（工作簿部分）


        //// 添加一个WorksheetPart（工作表部分）


        //// 添加Sheets集合到工作簿
        //Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());

        //// 创建一个Sheet（工作表），并将其添加到Sheets集合中
        //Sheet sheet = new Sheet() {
        //    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
        //    SheetId = 1,
        //    Name = "Sheet1"
        //};
        //sheets.Append(sheet);

        //// 获取SheetData，准备添加数据
        //SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

        //// 添加一行数据
        //Row row = new Row();
        //sheetData.Append(row);

        //// 在行中添加单元格并设置值
        //Cell cell = new Cell() { CellValue = new CellValue("Hello, OpenXml!"), DataType = CellValues.String };
        //row.Append(cell);

        //// 保存更改
        //workbookPart.Workbook.Save();
    }

    public static async Task ExportDetail(string folderPath, string? fileFilter, IEnumerable<CharacterData> characters, IEnumerable<AssetData> assets)
    {
        // 创建或覆盖Excel文件
        //using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
        //{
        //    // 添加一个WorkbookPart（工作簿部分）
        //    WorkbookPart workbookPart = document.AddWorkbookPart();
        //    workbookPart.Workbook = new Workbook();

        //    // 添加一个WorksheetPart（工作表部分）
        //    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        //    worksheetPart.Worksheet = new Worksheet(new SheetData());

        //    // 添加Sheets集合到工作簿
        //    Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());

        //    // 创建一个Sheet（工作表），并将其添加到Sheets集合中
        //    Sheet sheet = new Sheet() {
        //        Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
        //        SheetId = 1,
        //        Name = "Sheet1"
        //    };
        //    sheets.Append(sheet);

        //    // 获取SheetData，准备添加数据
        //    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

        //    // 添加一行数据
        //    Row row = new Row();
        //    sheetData.Append(row);

        //    // 在行中添加单元格并设置值
        //    Cell cell = new Cell() { CellValue = new CellValue("Hello, OpenXml!"), DataType = CellValues.String };
        //    row.Append(cell);

        //    // 保存更改
        //    workbookPart.Workbook.Save();
        //}
    }
}
