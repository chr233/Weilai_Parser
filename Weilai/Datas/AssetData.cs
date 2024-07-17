namespace Weilai.Datas;
public sealed record AssetData
{
    public EAssetType Type { get; set; }
    public string Name { get; set; }
    public long Count { get; set; }

    public AssetData(EAssetType type, string name, long count)
    {
        Type = type;
        Name = name;
        Count = count;
    }
}
