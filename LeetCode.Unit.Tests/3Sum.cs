using FluentAssertions;

namespace LeetCode.Unit.Tests;

public class ThreeSum
{
    [Theory]
    [InlineData(new[] { 0, 1, 1 }, 0)]
    [InlineData(new[] { -1, 0, 1, 2, -1, -4 }, 2)]
    [InlineData(new[] { -1, 0, 1, 2, -1, -4, -2, -3, 3, 0, 4 }, 9)]
    public void SumNumbersInArray(int[] input, int count)
    {
        var result = new List<IList<int>>();
        for (var current = 0; current <= input.Length - 1; current++)
        {
            for (var next = 0; next <= input.Length - 1; next++)
            {
                if(current == next) continue;
                var thirdIndex = SearchIndex(input, current, next);
                if (thirdIndex < 0) continue;
                var newItem = new List<int> { input[current], input[next], input[thirdIndex] }
                    .OrderBy(ii => ii)
                    .ToList();
                
                if (result.All(item => !Enumerable.SequenceEqual(item, newItem)))
                {
                    result.Add(newItem);
                }
            }
        }

        result.Count().Should().Be(count);
    }


    private int SearchIndex(int[] input, int current, int next)
    {
        //if (next > input.Length - 1) return -1;
        //if (current == next) return -1;

        var value = input[current] + input[next];
        for (var i = 0; i <= input.Length - 1; i++)
        {
            if (i == current || i == next) continue;
            if (value + input[i] == 0)
            {
                return i;
            }
        }

        return -1;
    }
}
