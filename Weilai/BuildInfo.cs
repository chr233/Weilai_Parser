using System.Reflection;
using System.Runtime.Versioning;

namespace Weilai;

/// <summary>
/// 工具类
/// </summary>
public static class BuildInfo
{
    /// <summary>
    /// 可执行文件
    /// </summary>
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();
    /// <summary>
    /// 版本
    /// </summary>
    public static string? Version => _assembly.GetName().Version?.ToString();
    /// <summary>
    /// 公司
    /// </summary>
    public static string? Company => _assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
    /// <summary>
    /// 版权
    /// </summary>
    public static string? Copyright => _assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
    /// <summary>
    /// 配置
    /// </summary>
    public static string? Configuration => _assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
    /// <summary>
    /// 框架
    /// </summary>
    public static string? FrameworkName => _assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkDisplayName;
}
