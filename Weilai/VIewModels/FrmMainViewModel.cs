using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using Weilai.Datas;

namespace Weilai.VIewModels;
public sealed partial class FrmMainViewModel : ObservableObject
{
    public SourceList<FileInfoData> FileList { get; } = new SourceList<FileInfoData>();
    public SourceList<CharacterData> CharacterList { get; } = new SourceList<CharacterData>();
    public SourceList<AssetData> AssetList { get; } = new SourceList<AssetData>();

    [ObservableProperty]
    private int progressValue = 0;

    [ObservableProperty]
    private int progressMax = 0;

    [ObservableProperty]
    private bool allowOperate = true;
}
