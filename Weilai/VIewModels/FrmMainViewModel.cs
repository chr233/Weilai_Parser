using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using Weilai.Datas;

namespace Weilai.VIewModels;
/// <summary>
/// Mvvm模型
/// </summary>
public sealed partial class FrmMainViewModel : ObservableObject
{
    /// <summary>
    /// 文件列表
    /// </summary>
    public SourceList<FileInfoData> FileList { get; } = new SourceList<FileInfoData>();
    /// <summary>
    /// 角色列表
    /// </summary>
    public SourceList<CharacterData> CharacterList { get; } = new SourceList<CharacterData>();
    /// <summary>
    /// 资源列表
    /// </summary>
    public SourceList<AssetData> AssetList { get; } = new SourceList<AssetData>();

    [ObservableProperty]
    private string? selectedFile;

    [ObservableProperty]
    private int progressValue = 0;

    [ObservableProperty]
    private int progressMax = 0;

    [ObservableProperty]
    private bool allowOperate = true;
}
