namespace Weilai.Datas;

public sealed record CharacterData
{
    public string FullName { get; init; }
    public string PinYinName { get; init; }
    public Dictionary<string, CountInfoData> CountInfo { get; } = [];

    public CharacterData(string fullName, string pinYinName)
    {
        FullName = fullName;
        PinYinName = pinYinName;
    }

    public override string ToString()
    {
        return CountInfo.Values.FirstOrDefault()?.ToString() ?? "null";
    }
}

public sealed record CountInfoData
{
    public long WordCount { get; set; }
    public long RawWordCount { get; set; }
    public long LineCount { get; set; }
    public long RawLineCount { get; set; }
    public List<CharacterDialogData> Dialogs { get; } = [];

    public override string ToString()
    {
        return $"{WordCount} - {RawWordCount} - {LineCount} - {RawLineCount} = {Dialogs.Count}";
    }
}

public sealed record CharacterDialogData
{
    public long LineId { get; set; }
    public string Emoji { get; set; }
    public string? Content { get; init; }
    public string? RawContent { get; set; }

    public CharacterDialogData(long lineId, string emoji, string? content, string? rawContent)
    {
        LineId = lineId;
        Emoji = emoji;
        Content = content;
        RawContent = rawContent;
    }
}