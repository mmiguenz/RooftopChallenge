using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RooftopChallenge.Core.Domain;

namespace RooftopChallenge.Core.Test.utils
{
    public class DummyCheckBlockService : ICheckBlockService
    {
        private readonly string _orderedBlocks;
        public int _calls = 0;

        public DummyCheckBlockService(List<String> orderedBlocks)
        {
            _orderedBlocks = String.Join("", orderedBlocks);
        }

        public bool AreConsequent(ImmutableList<string> list)
        {
            _calls++;
            var input = string.Join("", list);

            return _orderedBlocks.Contains(input);
        }


    }
}
