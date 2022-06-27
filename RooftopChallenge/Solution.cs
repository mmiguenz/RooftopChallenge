using System;
using System.Linq;
using System.Threading.Tasks;

namespace RooftopChallenge
{
    public class Solution
    {
        private static string[] Check(string[] block, string token)
        {
            var getOrderedBlocks = GetOrderedBlocksProvider.GetOrderedBlocks;

            var result = Task.Run(async () => await getOrderedBlocks.Invoke(block.ToList()));

            return result.Result.ToArray();
        }
    }
}