using FluentAssertions;

namespace LeetCode.Unit.Tests;

public class ThreeSumClosest
{
    [Theory]
    [InlineData(new[] { 0,0,0}, 1 , 0)]
    [InlineData(new[] { -1,2,1,-4}, 1 , 2)]
    [InlineData(new[] { 1,1,1,1}, 0 , 3)]
    public void ThreeSumClosestCalc(int[] input, int target , int output)
    {
        var dic = new Dictionary<int, int>();
        Array.Sort(input);
        var n = input.Length;
        for (var i = 0; i < n; i++)
        {
            if (i > 0 && input[i - 1] == input[i]) continue;

            var left = i + 1;
            var right = n - 1;

            while (left < right)
            {
                var sum = input[i] + input[left] + input[right];
                var diff = target > sum ? target - sum : sum - target;
                if (!dic.ContainsKey(diff))
                {
                    dic.Add(diff , sum);
                }

                if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
   
            }
        }

        var properKey = dic.Keys.Min(i=>i);
        var properSum = dic[properKey];
        properSum.Should().Be(output);
    }
}