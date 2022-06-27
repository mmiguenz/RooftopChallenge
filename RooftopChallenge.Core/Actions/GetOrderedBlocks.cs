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

        public async Task<List<string>> Invoke(List<string> blocks)
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

            return ol;
        }
    }

   
}