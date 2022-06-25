using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RooftopChallenge.Core.Test.utils
{
    public class Helper
    {
        public static IEnumerable<string> Shuffle(IEnumerable<string> orderedBlocks)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = orderedBlocks.Count();
            var shuffledList = orderedBlocks.ToList();
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                string value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }

            return shuffledList;
        }
    }
}
