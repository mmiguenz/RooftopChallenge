using System;
using System.Collections.Generic;
using RooftopChallenge.Core.Domain;
using System.Linq;
using System.Collections.Immutable;

namespace RooftopChallenge.Core.Actions
{
    public class GetOrderedBlocks
    {
        private ICheckBlockService _checkBlockService;

        public GetOrderedBlocks(ICheckBlockService checkBlockService)
        {
            _checkBlockService = checkBlockService;
        }

        public List<String> Invoke(List<string> unOrderedList)
        {
            var orderedList = ImmutableList.Create(unOrderedList[0]) ;
            var leftElements = unOrderedList.Skip(1).ToImmutableList();        

            return OrderBlocks(orderedList, leftElements).ToList();
        }

        private ImmutableList<string> OrderBlocks(ImmutableList<string> orderedList, ImmutableList<string> leftElements)
        {
            if(!leftElements.Any())
            {
                return orderedList;
            }

            var nextElement = "";

            foreach (var elem in leftElements)
            {
                var listToCheck = orderedList.Add(elem);

                if (_checkBlockService.AreConsequent(listToCheck))
                {
                    nextElement = elem;
                    break;
                }
            }

            return OrderBlocks(orderedList.Add(nextElement), leftElements.Remove(nextElement));
        }

    }
}
