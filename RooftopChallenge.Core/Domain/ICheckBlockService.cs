using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RooftopChallenge.Core.Domain
{
    public interface ICheckBlockService
    {
        Task<bool> AreConsequent(ImmutableList<string> list);
    }
}