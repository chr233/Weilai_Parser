namespace Weilai.Datas;
public sealed record CharacterData
{
    public string FullName { get; set; }
    public string PinYinName { get; set; }
    public long WordCount { get; set; }
    public long LineCount { get; set; }

    public HashSet<string> Emojis { get; set; } = [];

    public CharacterData(string fullName, string pinYinName)
    {
        FullName = fullName;
        PinYinName = pinYinName;
    }
}
