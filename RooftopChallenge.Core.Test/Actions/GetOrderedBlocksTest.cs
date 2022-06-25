using System;
using System.Collections.Generic;
using Moq;
using RooftopChallenge.Core.Actions;
using RooftopChallenge.Core.Domain;
using Xunit;
using System.Linq;
using System.Collections.Immutable;

namespace RooftopChallenge.Core.Test.Actions
{
    public class GetOrderedBlocksTest
    {        
        private List<string> _actualOrderedBlocks;
        private List<string> _orderedBlocks;
        private List<string> _unOrderedList;
        private ICheckBlockService _checkBlockService;

     
        [Fact]
        public void Given_An_UnOrderedBlocks_When_GetOrderedBlocks_Should_Return_Blocks_Ordered()
        {
            GivenAnOrderedListOfBlocks();
            GivenAnUnorderedListOfBlocks();
            GivenACheckBlocksService();

            WhenGetOrderedBlocks();

            ShouldReturnBlocksOrdered();
        }

        private void GivenACheckBlocksService()
        {
            _checkBlockService = new DummyCheckBlockService(_orderedBlocks);
        }

        private void GivenAnUnorderedListOfBlocks()
        {
            _unOrderedList = new List<string>()
            {
                "A",
                "D",
                "C",
                "B"
            };
        }

        private void GivenAnOrderedListOfBlocks()
        {
            _orderedBlocks = new List<string>()
            {
                "A",
                "B",
                "C",
                "D"
            };
        }

        private void ShouldReturnBlocksOrdered()
        {
            Assert.Equal(_orderedBlocks, _actualOrderedBlocks);
        }

        private void WhenGetOrderedBlocks()
        {
            _actualOrderedBlocks = new GetOrderedBlocks(_checkBlockService).Invoke(_unOrderedList);
        }
    }


    public class DummyCheckBlockService: ICheckBlockService
    {
        private readonly string _orderedBlocks;

        public DummyCheckBlockService(List<String> orderedBlocks)
        {
            _orderedBlocks = String.Join("",orderedBlocks);
        }

        public bool AreConsequent(ImmutableList<string> list)
        {
            var input = string.Join("",list);

            return _orderedBlocks.Contains(input);
        }
    }

}
