using System;
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
            var orderedList = ImmutableList.Create(unOrderedList[0]) ;
            var leftElements = unOrderedList.Skip(1).ToImmutableList();        

            return (await OrderBlocks(orderedList, leftElements)).ToList();
        }

        private async Task<ImmutableList<string>> OrderBlocks(ImmutableList<string> orderedList, ImmutableList<string> leftElements)
        {
            if(!leftElements.Any())
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

            return await OrderBlocks(orderedList.Add(nextElement), leftElements.Remove(nextElement));
        }

    }
}
