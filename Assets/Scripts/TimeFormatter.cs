using System.Text;

public static class TimeFormatter
{
    // 全局复用缓冲区（避免每帧分配）
    private static readonly char[] _buffer = new char[12]; // 足够存储 "12:34:56"
    private static readonly StringBuilder _stringBuilder = new StringBuilder(12);

    public static string Format(float totalSeconds)
    {
        // 重置StringBuilder
        _stringBuilder.Length = 0;

        // 计算时分秒
        int seconds = (int)totalSeconds;
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        seconds %= 60;

        // 写入小时（若存在）
        if (hours > 0)
        {
            int len = NumberUtils.WriteNumber(hours, _buffer, 0);
            _stringBuilder.Append(_buffer, 0, len);
            _stringBuilder.Append(':');
        }

        // 写入分钟
        int minStart = hours > 0 ? hours.ToString().Length + 1 : 0;
        int minLen = NumberUtils.WriteNumber(minutes, _buffer, minStart);
        _stringBuilder.Append(_buffer, minStart, minLen);
        _stringBuilder.Append(':');

        // 写入秒
        int secStart = minStart + minLen + 1;
        int secLen = NumberUtils.WriteNumber(seconds, _buffer, secStart);
        _stringBuilder.Append(_buffer, secStart, secLen);

        return _stringBuilder.ToString();
    }
}

public static class NumberUtils
{
    // 预分配数字字符缓存（0-9）
    private static readonly char[] _digitChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    // 将整数转为字符并写入缓冲区，返回写入长度
    public static int WriteNumber(int value, char[] buffer, int startIndex, int minDigits = 2)
    {
        int index = startIndex;
        int digits = 0;

        // 处理0值边界情况
        if (value == 0)
        {
            for (int i = 0; i < minDigits; i++)
                buffer[index++] = '0';
            return minDigits;
        }

        // 反向写入数字（从个位开始）
        int temp = value;
        while (temp > 0)
        {
            buffer[index++] = _digitChars[temp % 10]; // 取余得当前位
            temp /= 10;
            digits++;
        }

        // 补零（不足minDigits时）
        int zerosToAdd = minDigits - digits;
        for (int i = 0; i < zerosToAdd; i++)
            buffer[index++] = '0';

        // 反转数字部分（因写入顺序为个位→高位）
        int end = index - 1;
        int start = startIndex + zerosToAdd; // 补零后实际数字起始位
        while (start < end)
        {
            char tmp = buffer[start];
            buffer[start] = buffer[end];
            buffer[end] = tmp;
            start++;
            end--;
        }
        return digits + zerosToAdd;
    }
}