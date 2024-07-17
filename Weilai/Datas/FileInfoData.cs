namespace Weilai.Datas;
public sealed record FileInfoData
{
    public string Name { get; set; }
    public string Path { get; set; }

    public FileInfoData(string name, string path)
    {
        Name = name;
        Path = path;
    }
}
