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

        public DummyCheckBlockService(List<string> orderedBlocks)
        {
            _orderedBlocks = string.Join("", orderedBlocks);
        }

        public Task<bool> AreConsequent(string firstElem, string secondElem)
        {
            Calls++;
            var input = firstElem + secondElem;

            return Task.FromResult(_orderedBlocks.Contains(input));
        }


    }
}
