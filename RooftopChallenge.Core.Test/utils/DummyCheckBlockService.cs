using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using RooftopChallenge.Core.Domain;

namespace RooftopChallenge.Core.Test.utils
{
    public class DummyCheckBlockService : ICheckBlockService
    {
        private readonly string _orderedBlocks;
        public int Calls { get; private set; }

        public DummyCheckBlockService(List<String> orderedBlocks)
        {
            _orderedBlocks = String.Join("", orderedBlocks);
        }

        public Task<bool> AreConsequent(ImmutableList<string> list)
        {
            Calls++;
            var input = string.Join("", list);

            return Task.FromResult(_orderedBlocks.Contains(input));
        }


    }
}
