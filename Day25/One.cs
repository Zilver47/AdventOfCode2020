using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    public class One : IAnswerGenerator
    {
        private readonly long _cardPublicKey;
        private readonly long _doorPublicKey;

        public One(IEnumerable<string> input)
        {
            var keys = new LineParser().Parse(input).ToList();
            _cardPublicKey = keys.First();
            _doorPublicKey = keys.Last();
        }

        public long Generate()
        {
            var doorNumbers = TryFind(_doorPublicKey);
            var cardNumbers = TryFind(_cardPublicKey);

            var cardEncryptionKey = CalculateKey(_doorPublicKey, cardNumbers);
            var doorEncryptionKey = CalculateKey(_cardPublicKey, doorNumbers);
            if (cardEncryptionKey == doorEncryptionKey)
            {
                return cardEncryptionKey;
            }

            return -1;
        }

        private long TryFind(long publicKey)
        {
            const long subjectNumber = 7;
            long result = 1;
            for (long loopSize = 1; loopSize < long.MaxValue; loopSize++)
            {
                result *= subjectNumber;
                result %= 20201227;

                if (result == publicKey)
                {
                    return loopSize;
                }
            }

            return result;
        }

        private static long CalculateKey(long subjectNumber, long loopSize)
        {
            long result = 1;
            for (long i = 0; i < loopSize; i++)
            {
                result *= subjectNumber;
                result %= 20201227;
            }

            return result;
        }
    }
}