using FluentAssertions;

namespace LeetCode.Unit.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("III", 3)]
    [InlineData("IX", 9)]
    [InlineData("CM", 900)]
    [InlineData("LVIII", 58)]
    [InlineData("MCMXCIV", 1994)]
    public void TestRomanNumerals(string s, int number)
    {
        var ch = new string[s.Length];
        for (var i = 0; i < s.Length; i++)
        {
            ch[i] = s[i].ToString();
        }

        var count = ch.Count();

        var answer = 0;
        for (var i = 0; i <= count - 1; i++)
        {
            if (i + 1 <= count - 1 && RomanNumerals[ch[i]] < RomanNumerals[ch[i + 1]])
            {
                var value = Math.Abs(RomanNumerals[ch[i++]] - RomanNumerals[ch[i]]);
                answer += value;
            }
            else
            {
                answer += RomanNumerals[ch[i]];
            }
        }

        answer.Should().Be(number);
    }


    private Dictionary<string, int> RomanNumerals = new()
    {
        { "I", 1 },
        { "V", 5 },
        { "X", 10 },
        { "L", 50 },
        { "C", 100 },
        { "D", 500 },
        { "M", 1000 }
    };
}