﻿using System;
using System.Collections.Generic;
using RooftopChallenge.Core.Actions;
using RooftopChallenge.Core.Domain;
using Xunit;
using System.Linq;
using AutoFixture;
using RooftopChallenge.Core.Test.utils;

namespace RooftopChallenge.Core.Test.Actions
{
    public class GetOrderedBlocksTest
    {        
        private List<string> _actualOrderedBlocks;
        private List<string> _orderedBlocks;
        private List<string> _unOrderedList;
        private DummyCheckBlockService _checkBlockService;
        private Fixture _fixture = new ();


        [Theory]       
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(20)]        
        public void Given_An_UnOrderedBlocks_When_GetOrderedBlocks_Should_Return_Blocks_Ordered(int blockListLength)
        {
            GivenAnOrderedListOfBlocks(blockListLength);
            GivenAnUnorderedListOfBlocks();
            GivenACheckBlocksService();

            WhenGetOrderedBlocks();

            ShouldReturnBlocksOrdered();
            ShouldCallCheckBlockService();
        }
     
        private void GivenACheckBlocksService()
        {
            _checkBlockService = new DummyCheckBlockService(_orderedBlocks);
        }

        private void GivenAnUnorderedListOfBlocks()
        {
            _unOrderedList = new List<String>() { _orderedBlocks.First() };
            _unOrderedList.AddRange(Helper.Shuffle(_orderedBlocks.Skip(1)));
        }        

        private void GivenAnOrderedListOfBlocks(int blockListLength)
        {
            _orderedBlocks = _fixture.CreateMany<string>(blockListLength).ToList();
        }

        private void ShouldReturnBlocksOrdered()
        {
            Assert.Equal(_orderedBlocks, _actualOrderedBlocks);
        }

        private void WhenGetOrderedBlocks()
        {
            _actualOrderedBlocks = new GetOrderedBlocks(_checkBlockService).Invoke(_unOrderedList);
        }

        private void ShouldCallCheckBlockService()
        {
            var bestCase = _orderedBlocks.Count - 1;                

            Assert.True(_checkBlockService._calls >= bestCase );
        }       

    }

   

}
