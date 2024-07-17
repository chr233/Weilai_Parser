namespace Weilai.Datas;
public sealed record CharacterData
{
    public string FullName { get; init; }
    public string PinYinName { get; init; }
    public Dictionary<string, CountInfoData> CountInfo { get; } = [];
    public HashSet<string> Emojis { get; } = [];

    public CharacterData(string fullName, string pinYinName)
    {
        FullName = fullName;
        PinYinName = pinYinName;
    }

    public sealed record CountInfoData
    {
        public long WordCount { get; set; }
        public long RawWordCount { get; set; }
        public long LineCount { get; set; }
        public long RawLineCount { get; set; }
    }
}

