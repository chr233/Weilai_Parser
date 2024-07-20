using DynamicData;
using System.Collections.ObjectModel;
using ToolGood.Words.FirstPinyin;
using Weilai.Datas;

namespace Weilai.Core;

/// <summary>
/// 工具类
/// </summary>
public static class ContentParser
{
    private static char[] Separator { get; } = ['\r', '\n'];

    private static ReadOnlyDictionary<string, EAssetType> AssetKeyword { get; } = new Dictionary<string, EAssetType>(){
        { "CG"  , EAssetType.CG },
        { "特写" , EAssetType.CG },
        { "道具" , EAssetType.Item },
        { "场景" , EAssetType.Scene },
        { "背景" , EAssetType.Scene },
        { "立绘" , EAssetType.Spire },
        { "音效" , EAssetType.Music },
        { "配音" , EAssetType.Voice },
        { "选择" , EAssetType.Other },
        { "判定" , EAssetType.Other },
        { "跳转" , EAssetType.Other },
        { "选项" , EAssetType.Other },
        { "回忆" , EAssetType.Other },
        { "模式" , EAssetType.Other },
        { "剧情" , EAssetType.Other },
        { "画面" , EAssetType.Other },
        { "字体" , EAssetType.Other },
        { "成就" , EAssetType.Other },
        { "结局" , EAssetType.Other },
        { "进入" , EAssetType.Other },
        { "视角" , EAssetType.Other },
        { "返回" , EAssetType.Other },
        { "设定" , EAssetType.Other },
        { "获取" , EAssetType.Other },
        { "特效" , EAssetType.Other },
        { "失败" , EAssetType.Other },
        { "触发" , EAssetType.Other },
        { "主线" , EAssetType.Other },
        { "支线" , EAssetType.Other },
        { "镜头" , EAssetType.Other },
        { "时间" , EAssetType.Other },
        { "点击" , EAssetType.Other },
        { "提示" , EAssetType.Other },
        { "问"  , EAssetType.Other },
        { "D"  , EAssetType.Other },
        { "PV"  , EAssetType.Other },
        { "END" , EAssetType.Other },
        { "QTE" , EAssetType.Other },
        { "FLAG" , EAssetType.Other },
        { "倒计时" , EAssetType.Other },
    }.AsReadOnly();

    /// <summary>
    /// 获取资源类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static EAssetType GetAssetType(string name)
    {
        name = name.ToUpperInvariant();
        foreach (var (key, type) in AssetKeyword)
        {
            if (name.Contains(key))
            {
                return type;
            }
        }

        if (name.Length > 6)
        {
            return EAssetType.Unknown;
        }

        return EAssetType.None;
    }

    /// <summary>
    /// 解析剧本
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    public static void JubenParser(string fileName, string fileContent, ISourceList<CharacterData> characterSource, ISourceList<AssetData> assetSource)
    {
        if (string.IsNullOrEmpty(fileContent))
        {
            return;
        }

        var lines = fileContent.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

        var characterNameDict = characterSource.Items
            .ToDictionary(static x => x.FullName, static x => x);
        var characterPyDict = characterSource.Items
            .ToDictionary(static x => x.PinYinName, static x => x);

        var assetDict = assetSource.Items
            .ToDictionary(static x => x.Name, static x => x);

        var regexRawString = RegexUtils.MatchRawString();

        //第一遍扫描剧本, 添加角色和资源
        foreach (var line in lines)
        {
            var trimedStart = line.Trim();

            if (string.IsNullOrEmpty(trimedStart) || (!trimedStart.Contains('#') && !trimedStart.Contains('@')))
            {
                continue;
            }

            trimedStart = trimedStart[1..].Trim();

            var temp = trimedStart.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var keyword = temp.FirstOrDefault("");

            if (string.IsNullOrEmpty(keyword))
            {
                continue;
            }

            var assetType = GetAssetType(keyword);

            if (assetType != EAssetType.None)
            {
                //判断为资源
                if (!assetDict.TryGetValue(trimedStart, out var assetInfo))
                {
                    assetInfo = new AssetData(trimedStart, assetType);
                    assetDict.Add(trimedStart, assetInfo);
                }

                if (!assetInfo.CountInfo.TryGetValue(fileName, out var countInfo))
                {
                    countInfo = 0;
                }

                assetInfo.CountInfo[fileName] = countInfo + 1;
            }
            else
            {
                //判断为角色
                var parts = line[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                {
                    continue;
                }

                var name = parts[0];
                if (name.Length == 0 || name.Length > 6)
                {
                    continue;
                }

                var pinyin = WordsHelper.GetFirstPinyin(name);

                if (pinyin == "CNE")
                {
                    pinyin = "CNR";
                }

                if (!characterNameDict.TryGetValue(name, out var character) &&
                    !characterPyDict.TryGetValue(pinyin, out character))
                {
                    character = new CharacterData(name, pinyin);

                    characterNameDict.TryAdd(name, character);
                    characterPyDict.TryAdd(pinyin, character);
                }

                character.CountInfo.TryAdd(fileName, new CountInfoData());

                for (var i = 1; i < parts.Length; i++)
                {
                    var part = parts[i];
                    if (part.StartsWith('"'))
                    {
                        break;
                    }

                    var assetName = string.Format("{0} {1}", character.FullName, part);

                    if (!assetDict.TryGetValue(assetName, out var assetInfo))
                    {
                        assetInfo = new AssetData(assetName, EAssetType.Emoji);
                        assetDict.Add(assetName, assetInfo);
                    }

                    if (!assetInfo.CountInfo.TryGetValue(fileName, out var countInfo))
                    {
                        countInfo = 0;
                    }

                    assetInfo.CountInfo[fileName] = countInfo + 1;
                }
            }
        }

        var emojiCache = characterNameDict.Values
            .ToDictionary(static x => x.FullName, static _ => "");

        //解析台词
        for (var lineId = 0; lineId < lines.Length; lineId++)
        {
            var line = lines[lineId].Trim();

            //记录表情缓存
            if (line.StartsWith('#') || line.StartsWith('@'))
            {
                var keyword = line[1..].Trim();

                var parts = keyword.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                {
                    continue;
                }

                for (int i = 0; i < parts.Length; i++)
                {
                    var part = parts[i];
                    if (part.Length == 0 || part.Length > 6)
                    {
                        continue;
                    }

                    if (emojiCache.ContainsKey(part))
                    {
                        if (i + 1 < parts.Length)
                        {
                            var emoji = string.Join(' ', parts[(i + 1)..]);
                            emojiCache[part] = emoji;
                        }

                        break;
                    }
                }
            }
            else
            {
                var parts = line.Trim().Split('"', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2)
                {
                    continue;
                }

                var strNames = parts[0].ToUpperInvariant();
                var names = strNames.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                List<CharacterData> charactersList = [];
                foreach (var name in names)
                {
                    if (characterNameDict.TryGetValue(name, out var character) ||
                        characterPyDict.TryGetValue(name, out character))
                    {
                        charactersList.Add(character);
                    }
                }

                if (charactersList.Count == 0)
                {
                    continue;
                }

                var say = parts[1];
                if (say.EndsWith('"'))
                {
                    say = say[..^1];
                }

                var rawSay = regexRawString.Replace(say, "");

                if (say.Length + rawSay.Length > 0)
                {
                    foreach (var character in charactersList)
                    {
                        if (character.CountInfo.TryGetValue(fileName, out var countInfo))
                        {
                            if (rawSay.Length > 0)
                            {
                                countInfo.RawWordCount += rawSay.Length;
                                countInfo.RawLineCount++;

                                if (say.Length > 0)
                                {
                                    countInfo.WordCount += say.Length;
                                    countInfo.LineCount++;
                                }
                            }

                            if (!emojiCache.TryGetValue(character.FullName, out var emoji))
                            {
                                emoji = "";
                            }
                            var dialog = new CharacterDialogData(lineId, emoji, say, rawSay);
                            countInfo.Dialogs.Add(dialog);
                        }
                    }
                }
            }
        }

        characterSource.Edit(list => {
            list.Clear();
            list.AddRange(characterNameDict.Values);
        });

        assetSource.Edit(x => {
            x.Clear();
            x.AddRange(assetDict.Values);
        });
    }
}
