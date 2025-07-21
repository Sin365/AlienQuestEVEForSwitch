using System.Collections.Generic;
using System;
using System.Text;

public static class TimeFormatter
{
	private static readonly char[] _buffer = new char[12]; // 全局复用缓冲区
	private static readonly StringBuilder _stringBuilder = new StringBuilder(12);

	public static string Format(float totalSeconds)
	{
		_stringBuilder.Clear(); // 更高效的清空方式

		int seconds = (int)totalSeconds;
		int hours = seconds / 3600;
		int minutes = (seconds % 3600) / 60;
		seconds %= 60;

		// 小时处理（不补零）
		if (hours > 0)
		{
			NumberUtils.WriteNumber(hours, _buffer, 0, minDigits: 1); // 小时不强制补零[7](@ref)
			_stringBuilder.Append(_buffer, 0, NumberUtils.GetStringLength(hours, 1));
			_stringBuilder.Append(':');
		}

		// 分钟处理（强制补零）
		NumberUtils.WriteNumber(minutes, _buffer, 0, minDigits: 2); // 分钟固定两位[10,11](@ref)
		_stringBuilder.Append(_buffer, 0, 2); // 直接取前两位
		_stringBuilder.Append(':');

		// 秒处理（强制补零）
		NumberUtils.WriteNumber(seconds, _buffer, 0, minDigits: 2); // 秒固定两位[9](@ref)
		_stringBuilder.Append(_buffer, 0, 2);

		return _stringBuilder.ToString();
	}
}

public static class NumberUtils
{
	private static readonly char[] _digitChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	// 新增：计算数字格式化后的长度
	public static int GetStringLength(int value, int minDigits)
	{
		if (value == 0) return minDigits > 0 ? minDigits : 1;
		int digits = 0;
		int temp = value;
		while (temp > 0)
		{
			digits++;
			temp /= 10;
		}
		return Math.Max(digits, minDigits);
	}

	// 优化后的数字写入方法
	public static void WriteNumber(int value, char[] buffer, int startIndex, int minDigits = 2)
	{
		int index = startIndex;
		int digits = 0;
		int temp = value;

		// 处理补零[10](@ref)
		int zerosToAdd = Math.Max(0, minDigits - GetDigitCount(value));
		for (int i = 0; i < zerosToAdd; i++)
		{
			buffer[index++] = '0';
		}

		// 反向写入实际数字
		Stack<char> stack = new Stack<char>(minDigits);
		do
		{
			stack.Push(_digitChars[temp % 10]);
			temp /= 10;
		} while (temp > 0);

		while (stack.Count > 0)
		{
			buffer[index++] = stack.Pop();
		}
	}

	// 计算数字位数
	private static int GetDigitCount(int n)
	{
		if (n == 0) return 1;
		return (int)Math.Log10(n) + 1;
	}
}