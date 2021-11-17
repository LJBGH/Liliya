using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public class RandomExtensions
    {
        private Random random = new Random(Guid.NewGuid().GetHashCode());
        /// <summary>
        /// 生成随机数
        /// </summary>
        public static string GetRandom()
        {
            byte[] randomBytes = new byte[8];
            System.Security.Cryptography.RNGCryptoServiceProvider rngServiceProvider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rngServiceProvider.GetBytes(randomBytes);
            var nextNums = new RandomExtensions().GetNextRandom();
            var nums = (int)(Math.Abs(BitConverter.ToInt64(randomBytes, 0)) / Int64.MaxValue * (100000 + 1));
            if (nextNums % 2 == 0) return (nums + nextNums).ToString();
            return Math.Abs(nums - nextNums).ToString();
        }

        public int GetNextRandom()
        {
            string nowGuid = Guid.NewGuid().ToString();
            int seed = DateTime.Now.Millisecond + random.Next(1000, 99999);
            for (int i = 0; i < nowGuid.Length; i++)
            {
                switch (nowGuid[i])
                {
                    case 'a':
                        seed = seed + 1;
                        break;
                    case 'b':
                        seed = seed + 3;
                        break;
                    case 'c':
                        seed = seed + 5;
                        break;
                    case 'd':
                        seed = seed + 7;
                        break;
                    case 'e':
                        seed = seed + 11;
                        break;
                    case 'f':
                        seed = seed + 13;
                        break;
                    case 'g':
                        seed = seed + 17;
                        break;
                    case 'h':
                        seed = seed + 19;
                        break;
                    case 'i':
                        seed = seed - 31;
                        break;
                    case 'j':
                        seed = seed + 37;
                        break;
                    case 'k':
                        seed = seed + 53;
                        break;
                    case 'l':
                        seed = seed - 57;
                        break;
                    case 'm':
                        seed = seed + 61;
                        break;
                    case 'n':
                        seed = seed + 73;
                        break;
                    case 'o':
                        seed = seed + 2;
                        break;
                    case 'p':
                        seed = seed - 33;
                        break;
                    case 'q':
                        seed = seed - 91;
                        break;
                    case 'r':
                        seed = seed - 10;
                        break;
                    case 's':
                        seed = seed + 23;
                        break;
                    case 't':
                        seed = seed - 29;
                        break;
                    case 'u':
                        seed = seed + 15;
                        break;
                    case 'v':
                        seed = seed + 87;
                        break;
                    case 'w':
                        seed = seed - 24;
                        break;
                    case 'x':
                        seed = seed + 8;
                        break;
                    case 'y':
                        seed = seed - 71;
                        break;
                    case 'z':
                        seed = seed + 39;
                        break;
                    default:
                        seed = seed - 9;
                        break;
                }
            }
            return Math.Abs(seed);
        }
    }
}
