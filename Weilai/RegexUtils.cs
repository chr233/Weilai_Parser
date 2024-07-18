using System.Text.RegularExpressions;

namespace Weilai;

public static partial class RegexUtils
{
    [GeneratedRegex(@"[@#·…!！¿?？,.，。;:；：、\s]|[(（]\S+[）)]")]
    public static partial Regex MatchRawString();
}
