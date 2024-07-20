namespace Weilai.Datas;

/// <summary>
/// 资源类型
/// </summary>
public enum EAssetType : byte
{
    None = 0,
    CG,
    /// <summary>
    /// 场景
    /// </summary>
    Scene,
    /// <summary>
    /// 音效
    /// </summary>
    Music,
    /// <summary>
    /// 道具
    /// </summary>
    Item,
    /// <summary>
    /// 立绘
    /// </summary>
    Spire,
    /// <summary>
    /// 配音
    /// </summary>
    Voice,
    /// <summary>
    /// 表情
    /// </summary>
    Emoji,
    /// <summary>
    /// 其他
    /// </summary>
    Other,
    /// <summary>
    /// 未识别
    /// </summary>
    Unknown,
}