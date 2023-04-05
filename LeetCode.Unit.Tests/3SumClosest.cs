using FluentAssertions;

namespace LeetCode.Unit.Tests;

public class ThreeSumClosest
{
    [Theory]
    [InlineData(new[] { 0, 0, 0 }, 1, 0)]
    [InlineData(new[] { -1, 2, 1, -4 }, 1, 2)]
    [InlineData(new[] { 1, 1, 1, 1 }, 0, 3)]
    [InlineData(new[] { 4, 0, 5, -5, 3, 3, 0, -4, -5 }, -2, -2)]
    [InlineData(new[] { 4, 0, 5, -5, 3, 3, 0, -4, -100 , 50 , 200 , 296 , 80 , 70 , 55 , 34, 22, -1 }, 243, 245)]
    public void ThreeSumClosestCalc(int[] input, int target, int output)
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
                    dic.Add(diff, sum);
                }
                if(sum == target) break;
                
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

        var closetValue = dic.Keys.Min();
        var closetSum = dic[closetValue];
        closetSum.Should().Be(output);
    }
}