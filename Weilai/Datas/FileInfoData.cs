namespace Weilai.Datas;
public sealed record FileInfoData
{
    public string Name { get; private set; }
    public string Path { get; private set; }

    public FileInfoData(string name, string path)
    {
        Name = name;
        Path = path;
    }
}
