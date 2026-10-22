using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// 极简 cfg / ini 读写助手。
/// 只依赖 SharpConfig，整个文件拷进项目即可用。
///
/// 支持：无段头（厂商 .cfg 扁平格式）、有段头（标准 .ini）、两种混合。
/// </summary>
public static class SimpleCfg
{
    /// <summary>无段头内容归一化后使用的段名。</summary>
    private const string DefaultSection = "DEFAULT";

    // ============================ 读 ============================

    /// <summary>读取全部键值（扁平视图；文件有多个段时合并到一起）。</summary>
    public static Dictionary<string, string> Read(string path)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var sec in ReadSections(path))
        {
            foreach (var kv in sec.Value)
            {
                if (!result.ContainsKey(kv.Key)) result[kv.Key] = kv.Value;
            }
        }
        return result;
    }

    /// <summary>按段读取。段名 → 该段键值；无段头文件的兜底段叫 "DEFAULT"。</summary>
    public static Dictionary<string, Dictionary<string, string>> ReadSections(string path)
    {
        Encoding encoding;
        string text = ReadText(path, out encoding);
        // 必须 Normalize：SharpConfig 没有 [section] 段头就一个键都读不到
        return ParseSections(Normalize(text));
    }

    // ============================ 写 ============================

    /// <summary>
    /// 保存（平铺格式：一行一个 KEY=VALUE，无段头）。
    /// 厂商下发的 .cfg 用这个格式回写。
    /// </summary>
    public static void Save(string path, Dictionary<string, string> values, Encoding encoding = null)
    {
        var sb = new StringBuilder();
        foreach (var kv in values)
        {
            if (string.IsNullOrEmpty(kv.Key)) continue;
            sb.Append(kv.Key).Append('=').Append(Clean(kv.Value)).Append("\r\n");
        }
        WriteText(path, sb.ToString(), encoding);
    }

    /// <summary>保存（分段格式：带 [段头]）。标准 .ini 用这个。</summary>
    public static void SaveSections(string path,
        Dictionary<string, Dictionary<string, string>> sections, Encoding encoding = null)
    {
        var sb = new StringBuilder();
        foreach (var sec in sections)
        {
            if (string.IsNullOrEmpty(sec.Key)) continue;
            sb.Append('[').Append(sec.Key).Append(']').Append("\r\n");
            foreach (var kv in sec.Value)
            {
                if (string.IsNullOrEmpty(kv.Key)) continue;
                sb.Append(kv.Key).Append('=').Append(Clean(kv.Value)).Append("\r\n");
            }
            sb.Append("\r\n");
        }
        WriteText(path, sb.ToString(), encoding);
    }

    /// <summary>
    /// 改一个键并保存 —— 最常用的操作。
    /// 自动保持原有结构：源文件有段头就分段写回，没有就平铺写回。
    /// </summary>
    public static void SetValue(string path, string key, string value, Encoding encoding = null)
    {
        Encoding fileEncoding;
        string text = ReadText(path, out fileEncoding);
        bool hasSections = HasSectionHeaders(text);

        var sections = ParseSections(Normalize(text));

        // 找到该键当前所在的段；不存在就落到 DEFAULT 段
        string target = DefaultSection;
        foreach (var sec in sections)
        {
            if (sec.Value.ContainsKey(key)) { target = sec.Key; break; }
        }

        Dictionary<string, string> map;
        if (!sections.TryGetValue(target, out map))
        {
            map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            sections[target] = map;
        }
        map[key] = value;

        var sb = new StringBuilder();
        foreach (var sec in sections)
        {
            if (!hasSections && !string.Equals(sec.Key, target, StringComparison.OrdinalIgnoreCase)) continue;

            if (hasSections) sb.Append('[').Append(sec.Key).Append(']').Append("\r\n");
            foreach (var kv in sec.Value)
            {
                if (string.IsNullOrEmpty(kv.Key)) continue;
                sb.Append(kv.Key).Append('=').Append(Clean(kv.Value)).Append("\r\n");
            }
            if (hasSections) sb.Append("\r\n");
        }
        WriteText(path, sb.ToString(), encoding ?? fileEncoding);
    }

    // ======================== 内部实现 ========================

    private static Dictionary<string, Dictionary<string, string>> ParseSections(string normalizedText)
    {
        var sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        foreach (var section in Configuration.LoadFromString(normalizedText))
        {
            string name = string.IsNullOrEmpty(section.Name) ? DefaultSection : section.Name;
            Dictionary<string, string> map;
            if (!sections.TryGetValue(name, out map))
            {
                map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                sections[name] = map;
            }
            foreach (var setting in section)
            {
                if (string.IsNullOrEmpty(setting.Name)) continue;
                if (!map.ContainsKey(setting.Name)) map[setting.Name] = Clean(setting.StringValue);
            }
        }
        return sections;
    }

    /// <summary>
    /// 归一化：SharpConfig 要求 key=value 必须在 [section] 之下。
    /// 若"第一行有效内容"不是段头（说明顶部有裸键），就在最前面补一个 [DEFAULT]。
    /// </summary>
    private static string Normalize(string text)
    {
        bool firstIsSection = true;
        bool found = false;
        foreach (var raw in text.Split('\n'))
        {
            string line = raw.Trim('\r', ' ', '\t');
            if (line.Length == 0 || IsComment(line)) continue;
            firstIsSection = line.StartsWith("[") && line.EndsWith("]");
            found = true;
            break;
        }
        return (found && !firstIsSection) ? "[" + DefaultSection + "]\r\n" + text : text;
    }

    /// <summary>原文是否含有 [section] 段头。</summary>
    private static bool HasSectionHeaders(string text)
    {
        foreach (var raw in text.Split('\n'))
        {
            string line = raw.Trim('\r', ' ', '\t');
            if (line.Length == 0 || IsComment(line)) continue;
            if (line.StartsWith("[") && line.EndsWith("]")) return true;
        }
        return false;
    }

    private static bool IsComment(string line)
    {
        return line.StartsWith("#") || line.StartsWith(";") || line.StartsWith("//");
    }

    /// <summary>去掉值尾部的全角/半角分号、逗号。</summary>
    private static string Clean(string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return value.TrimEnd().TrimEnd('；', ';', '，', ',').TrimEnd();
    }

    // ---------- 编码与文件 IO ----------

    private static string ReadText(string path, out Encoding encoding)
    {
        if (!File.Exists(path)) throw new FileNotFoundException("配置文件不存在: " + path, path);
        encoding = DetectEncoding(path);
        return File.ReadAllText(path, encoding);
    }

    private static void WriteText(string path, string content, Encoding encoding)
    {
        string dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
        // UTF8Encoding(false) 不写 BOM，设备端解析器更友好
        File.WriteAllText(path, content, encoding ?? new UTF8Encoding(false));
    }

    /// <summary>编码嗅探：BOM → 严格 UTF-8 → GB18030（中文设备 cfg 常见）。</summary>
    private static Encoding DetectEncoding(string path)
    {
        byte[] head;
        using (var fs = File.OpenRead(path))
        {
            head = new byte[4];
            int n = fs.Read(head, 0, 4);
            if (n >= 3 && head[0] == 0xEF && head[1] == 0xBB && head[2] == 0xBF) return new UTF8Encoding(false);
            if (n >= 2 && head[0] == 0xFF && head[1] == 0xFE) return Encoding.Unicode;
            if (n >= 2 && head[0] == 0xFE && head[1] == 0xFF) return Encoding.BigEndianUnicode;
        }
        try
        {
            var strictUtf8 = new UTF8Encoding(false, true);
            strictUtf8.GetString(File.ReadAllBytes(path));
            return strictUtf8;
        }
        catch (DecoderFallbackException)
        {
            try { return Encoding.GetEncoding("GB18030"); }
            catch { return Encoding.Default; }
        }
    }
}
