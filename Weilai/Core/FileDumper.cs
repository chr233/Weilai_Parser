using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DynamicData;
using Weilai.Datas;

namespace Weilai.Core;

/// <summary>
/// 工具类
/// </summary>
public static class FileDumper
{
    private static string FormatFileName(string fileName)
    {
        var now = DateTime.Now;
        return string.Format("{0}_{1:yyyy-MM-dd_HH-mm-ss}.xlsx", fileName, now);
    }

    private static string? SaveStreamToFile(MemoryStream stream, string fileName, string folderPath)
    {
        var formatName = FormatFileName(fileName);
        var filePath = Path.Combine(folderPath, formatName);

        if (File.Exists(filePath) &&
            MessageBox.Show("文件 " + filePath + " 已存在, 是否覆盖?", "询问", MessageBoxButtons.YesNo) != DialogResult.Yes)
        {
            return null;
        }

        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        stream.Position = 0;
        stream.WriteTo(fileStream);

        return filePath;
    }


    private static SheetData InitColumns(this Worksheet worksheet, string[] titles)
    {
        var sheetData = worksheet.GetOrCreateSheet();

        //var columns = new Columns();
        //for (uint i = 1; i <= titles.Length; i++)
        //{
        //    var column = new Column { Min = i, Max = i, Width = 99, CustomWidth = true, BestFit = true };
        //    columns.Append(column);
        //}
        //sheetData.Append(columns);

        sheetData.InsertTitle(titles);
        return sheetData;
    }

    private static void InsertTitle(this SheetData sheetData, string[] titles)
    {
        var row = new Row();
        foreach (var title in titles)
        {
            row.AppendStringCell(title);
        }
        sheetData.Append(row);
    }

    private static void AppendStringCell(this Row row, string value)
    {
        var cell = new Cell {
            CellValue = new CellValue(value),
            DataType = CellValues.String,
        };
        row.Append(cell);
    }

    private static void AppendIntCell(this Row row, int value)
    {
        var cell = new Cell {
            CellValue = new CellValue(value),
            DataType = CellValues.Number,
        };
        row.Append(cell);
    }

    private static void AppendDecimalCell(this Row row, decimal value)
    {
        var cell = new Cell {
            CellValue = new CellValue(value),
            DataType = CellValues.Number,
        };
        row.Append(cell);
    }

    private static void AppendCell(this Row row, CellValue cellValue, CellValues dataType)
    {
        var cell = new Cell {
            CellValue = cellValue,
            DataType = dataType,
        };
        row.Append(cell);
    }

    private static SheetData GetOrCreateSheet(this Worksheet worksheet)
    {
        var sheetData = worksheet.GetFirstChild<SheetData>();
        if (sheetData == null)
        {
            sheetData = new SheetData();
            worksheet.Append(sheetData);
        }

        return sheetData;
    }

    private readonly static string[] AssetTitle = ["Id", "类型", "关键字", "引用次数"];

    public static string? ExportAssets(string folderPath, ISourceList<FileInfoData> fileSource, ISourceList<AssetData> assetSource, ISourceList<string> exportFileSource)
    {
        using var memoryStream = new MemoryStream();
        using var document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = document.AddWorkbookPart();
        var workbook = new Workbook();
        workbookPart.Workbook = workbook;

        var sheets = workbookPart.Workbook.AppendChild(new Sheets());

        var assetList = assetSource.Items.OrderBy(static x => x.Type);

        uint sheetId = 1;
        // 概要Sheet
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet(new SheetData());
            worksheetPart.Worksheet = worksheet;

            var sheet = new Sheet() {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = sheetId++,
                Name = "概要",
            };
            sheets.Append(sheet);

            var sheetData = worksheet.InitColumns(AssetTitle);

            int index = 1;
            foreach (var asset in assetList)
            {
                var count = asset.CountInfo.Values.Sum();
                if (count > 0)
                {
                    var row = new Row();
                    row.AppendIntCell(index++);
                    row.AppendStringCell(asset.Type.ToString());
                    row.AppendStringCell(asset.Name);
                    row.AppendDecimalCell(count);
                    sheetData.Append(row);
                }
            }
        }

        // 文件Sheet
        uint fileNo = 1;
        foreach (var fileInfo in fileSource.Items)
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet(new SheetData());
            worksheetPart.Worksheet = worksheet;

            var sheet = new Sheet() {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = sheetId++,
                Name = string.Format("{0}. {1}", fileNo++, fileInfo.Name),
            };
            sheets.Append(sheet);

            var sheetData = worksheet.InitColumns(AssetTitle);

            int index = 1;
            foreach (var asset in assetList)
            {
                if (!asset.CountInfo.TryGetValue(fileInfo.Name, out var count))
                {
                    count = 0;
                }

                if (count > 0)
                {
                    var row = new Row();
                    row.AppendIntCell(index++);
                    row.AppendStringCell(asset.Type.ToString());
                    row.AppendStringCell(asset.Name);
                    row.AppendDecimalCell(count);
                    sheetData.Append(row);
                }
            }
        }

        workbook.Save();
        document.Dispose();

        var filePath = SaveStreamToFile(memoryStream, "资源统计", folderPath);
        exportFileSource.Edit(list => list.Add(filePath ?? "导出失败"));
        return filePath;
    }

    private readonly static string[] CharacterSummaryTitle = ["Id", "角色名称", "代号", "对话字数", "对话字数(不含标点)", "对话句数", "对话句数(不含标点)"];

    public static string? ExportCharacterSummary(string folderPath, ISourceList<FileInfoData> fileSource, ISourceList<CharacterData> characterSource, ISourceList<AssetData> assetSource, ISourceList<string> exportFileSource)
    {
        using var memoryStream = new MemoryStream();
        using var document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = document.AddWorkbookPart();
        var workbook = new Workbook();
        workbookPart.Workbook = workbook;

        var sheets = workbookPart.Workbook.AppendChild(new Sheets());

        var characterList = characterSource.Items.OrderBy(static x => x.FullName);
        var emojiList = assetSource.Items.Where(static x => x.Type == EAssetType.Emoji);

        uint sheetId = 1;
        // 概要Sheet
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet(new SheetData());
            worksheetPart.Worksheet = worksheet;

            var sheet = new Sheet() {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = sheetId++,
                Name = "概要",
            };
            sheets.Append(sheet);

            var sheetData = worksheet.InitColumns(CharacterSummaryTitle);

            int index = 1;
            foreach (var character in characterList)
            {
                var countInfo = character.CountInfo;

                var wordCount = countInfo.Values.Sum(static x => x.WordCount);
                var rawWordCount = countInfo.Values.Sum(static x => x.RawWordCount);
                var lineCount = countInfo.Values.Sum(static x => x.LineCount);
                var rawLineCount = countInfo.Values.Sum(static x => x.RawLineCount);

                if (wordCount + rawWordCount > 0)
                {
                    var row = new Row();
                    row.AppendIntCell(index++);
                    row.AppendStringCell(character.FullName);
                    row.AppendStringCell(character.PinYinName);
                    row.AppendDecimalCell(wordCount);
                    row.AppendDecimalCell(rawWordCount);
                    row.AppendDecimalCell(lineCount);
                    row.AppendDecimalCell(rawLineCount);
                    sheetData.Append(row);
                }
            }
        }

        // 文件Sheet
        uint fileNo = 1;
        foreach (var fileInfo in fileSource.Items)
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet(new SheetData());
            worksheetPart.Worksheet = worksheet;

            var sheet = new Sheet() {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = sheetId++,
                Name = string.Format("{0}. {1}", fileNo++, fileInfo.Name),
            };
            sheets.Append(sheet);

            var sheetData = worksheet.InitColumns(CharacterSummaryTitle);

            int index = 1;
            foreach (var character in characterList)
            {
                if (!character.CountInfo.TryGetValue(fileInfo.Name, out var countInfo))
                {
                    continue;
                }

                var wordCount = countInfo.WordCount;
                var rawWordCount = countInfo.RawWordCount;
                var lineCount = countInfo.LineCount;
                var rawLineCount = countInfo.RawLineCount;

                if (wordCount + rawWordCount > 0)
                {
                    var row = new Row();
                    row.AppendIntCell(index++);
                    row.AppendStringCell(character.FullName);
                    row.AppendStringCell(character.PinYinName);
                    row.AppendDecimalCell(wordCount);
                    row.AppendDecimalCell(rawWordCount);
                    row.AppendDecimalCell(lineCount);
                    row.AppendDecimalCell(rawLineCount);
                    sheetData.Append(row);
                }
            }
        }

        workbook.Save();
        document.Dispose();

        var filePath = SaveStreamToFile(memoryStream, "角色统计", folderPath);
        exportFileSource.Edit(list => list.Add(filePath ?? "导出失败"));
        return filePath;
    }

    private readonly static string[] CharacterTitle = ["Id", "角色名称", "代号", "行号", "表情", "对话文本", "对话文本(不含标点)", "字数", "字数(不含标点)"];

    public static string? ExportCharacter(string folderPath, string fileName, ISourceList<CharacterData> characterSource, ISourceList<AssetData> assetSource, ISourceList<string> exportFileSource)
    {
        using var memoryStream = new MemoryStream();
        using var document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = document.AddWorkbookPart();
        var workbook = new Workbook();
        workbookPart.Workbook = workbook;

        var sheets = workbookPart.Workbook.AppendChild(new Sheets());

        var characterList = characterSource.Items.OrderBy(static x => x.FullName);
        var emojiList = assetSource.Items.Where(static x => x.Type == EAssetType.Emoji);

        // 文件Sheet
        uint sheetId = 1;
        foreach (var character in characterList)
        {
            if (character.CountInfo.TryGetValue(fileName, out var countInfo))
            {
                if (countInfo.Dialogs.Count == 0)
                {
                    continue;
                }

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var worksheet = new Worksheet(new SheetData());
                worksheetPart.Worksheet = worksheet;

                var sheet = new Sheet() {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = sheetId++,
                    Name = string.Format("{0} {1}", character.FullName, character.PinYinName),
                };
                sheets.Append(sheet);

                var sheetData = worksheet.InitColumns(CharacterTitle);

                int index = 1;
                foreach (var dialog in countInfo.Dialogs)
                {
                    var count = dialog.Content?.Length ?? 0;
                    var rawCount = dialog.RawContent?.Length ?? 0;

                    if (rawCount == 0)
                    {
                        count = 0;
                    }

                    var row = new Row();
                    row.AppendIntCell(index++);
                    row.AppendStringCell(character.FullName);
                    row.AppendStringCell(character.PinYinName);
                    row.AppendDecimalCell(dialog.LineId);
                    row.AppendStringCell(dialog.Emoji);
                    row.AppendStringCell(dialog.Content ?? "");
                    row.AppendStringCell(dialog.RawContent ?? "");
                    row.AppendDecimalCell(count);
                    row.AppendDecimalCell(rawCount);
                    sheetData.Append(row);
                }
            }
        }

        workbook.Save();
        document.Dispose();

        var name = Path.GetFileNameWithoutExtension(fileName);
        var filePath = SaveStreamToFile(memoryStream, string.Format("角色对话_{0}", name), folderPath);
        exportFileSource.Edit(list => list.Add(filePath ?? "导出失败"));
        return filePath;
    }
}
