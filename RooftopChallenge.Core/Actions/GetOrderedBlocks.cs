using System;
using System.Collections;
using System.Collections.Generic;
using RooftopChallenge.Core.Domain;
using System.Linq;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RooftopChallenge.Core.Actions
{
    public class GetOrderedBlocks
    {
        private ICheckBlockService _checkBlockService;

        public GetOrderedBlocks(ICheckBlockService checkBlockService)
        {
            _checkBlockService = checkBlockService;
        }

        public async Task<List<String>> Invoke(List<string> unOrderedList)
        {
            var orderedList = ImmutableList.Create(unOrderedList[0]);
            var leftElements = unOrderedList.Skip(1).ToImmutableList();

           // return (await OrderBlocksRecursive(orderedList, leftElements)).ToList();
           return (await OrderBlocksIterative(unOrderedList.ToImmutableList())).ToList();
        }

        private async Task<ImmutableList<string>> OrderBlocksRecursive(ImmutableList<string> orderedList,
            ImmutableList<string> leftElements)
        {
            if (!leftElements.Any())
            {
                return orderedList;
            }

            var nextElement = "";

            foreach (var elem in leftElements)
            {
                var listToCheck = orderedList.Add(elem);

                if (await _checkBlockService.AreConsequent(listToCheck))
                {
                    nextElement = elem;
                    break;
                }
            }

            return await OrderBlocksRecursive(orderedList.Add(nextElement), leftElements.Remove(nextElement));
        }

        private async Task<ImmutableList<string>> OrderBlocksIterative(ImmutableList<string> blocks)
        {
            var ol = new List<string> {blocks.First()};
            var alreadyProcessedIndexes = Enumerable.Range(0, blocks.Count).Select(_ => false).ToList();
            alreadyProcessedIndexes[0] = true;

            while (ol.Count < blocks.Count)
            {
                for (var i = 0; i < blocks.Count; i++)
                {
                    if (alreadyProcessedIndexes[i])
                        continue;

                    var lastSorted = ol.Last();
                    var currentElem = blocks[i];
                    var listToCheck = ImmutableList.Create<string>().Add(lastSorted).Add(currentElem);

                    if (await _checkBlockService.AreConsequent(listToCheck))
                    {
                        ol.Add(currentElem);
                        alreadyProcessedIndexes[i] = true;
                    }
                }
            }

            return ol.ToImmutableList();
        }

    }

   
}