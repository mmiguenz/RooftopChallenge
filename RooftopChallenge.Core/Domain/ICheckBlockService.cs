using System.Collections.Immutable;

namespace RooftopChallenge.Core.Domain
{
    public interface ICheckBlockService
    {
        bool AreConsequent(ImmutableList<string> list);
    }
}