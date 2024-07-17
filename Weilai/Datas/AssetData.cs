namespace Weilai.Datas;
public sealed record AssetData
{
    public string Name { get; init; }
    public EAssetType Type { get; init; }

    public Dictionary<string, long> CountInfo { get; } = [];

    public AssetData(string name, EAssetType type)
    {
        Name = name;
        Type = type;
    }
}
