using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RooftopChallenge.Core.Domain
{
    public interface ICheckBlockService
    {
        Task<bool> AreConsequent(string firstElem, string secondElem);
    }
}