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

        Array.Sort(input);
        for (var current = 0; current <= input.Length - 1; current++)
        {
            for (var next = 0; next <= input.Length - 1; next++)
            {
                if (current == next) continue;
                var thirdIndex = SearchIndex(input, current, next);
                if (thirdIndex < 0) continue;
                var newItem = new List<int> { input[current], input[next], input[thirdIndex] }
                    .OrderBy(ii => ii)
                    .ToList();

                // if (result.All(item => !Enumerable.SequenceEqual(item, newItem)))
                // {
                //     result.Add(newItem);
                // }

                var comparer = new ArrayListComparer();
                if (!result.Any(item =>
                        comparer.Equals(item,
                            newItem))) // item[0] == newItem[0] && item[1] == newItem[1] && item[2] == newItem[2]))
                {
                    result.Add(newItem);
                }
            }
        }

        result.Count().Should().Be(count);
    }


    private int SearchIndex(int[] input, int current, int next)
    {
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


    [Theory]
    [InlineData(new[] { 1, -1, 0 , -1 }, 1)]
    [InlineData(new[] { 0, 1, 1 }, 0)]
    [InlineData(new[] { -1, 0, 1, 2, -1, -4 }, 2)]
    [InlineData(new[] { -1, 0, 1, 2, -1, -4, -2, -3, 3, 0, 4 }, 9)]
    public void SumNumbersInArrayWithTwoPointers(int[] input, int count)
    {
        var result = new List<IList<int>>();
        Array.Sort(input);
        var n = input.Length;
        for (var i = 0; i < n; i++)
        {
            if (i > 0 && input[i - 1] == input[i]) continue;
            
            var left = i + 1;
            var right =  n - 1;

            while (left < right)
            {
                var sum = input[i] + input[left] + input[right];
                if (sum == 0)
                {
                    result.Add(new List<int>() { input[i], input[left], input[right] });

                    while (left < right && input[left] == input[left + 1]) left++;
                    while (left < right && input[right] == input[right - 1]) right--;

                    left++;
                    right--;
                }
                else if (sum < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }

        result.Count().Should().Be(count);
    }
}

public class ArrayListComparer : EqualityComparer<IList<int>>
{
    public override bool Equals(IList<int>? x, IList<int>? y)
    {
        if (x == null || y == null) return false;
        return x[0] == y[0] && x[1] == y[1] && x[2] == y[2];
    }

    public override int GetHashCode(IList<int> obj)
    {
        return obj.GetHashCode();
    }
}