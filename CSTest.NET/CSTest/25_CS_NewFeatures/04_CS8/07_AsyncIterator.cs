using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _07_AsyncIterator
    {
#if CS7
        [Test]
        public async Task TestAsyncIterator_1()
        {
            await foreach (var number  in  RangeAsync(0, 10, 100) ) 
                Debug.WriteLine(number);
        }

        async IAsyncEnumerable<int> RangeAsync(int start, int count, int delay)
        {
            for (int i = start; i < start + count; i++)
            {
                await Task.Delay(delay);
                yield return i;
            }
        }
    }

#endif

}
