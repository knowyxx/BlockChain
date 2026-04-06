using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class Block
    {
        int index;
        internal DateTime time;
        internal Data data;
        internal string hash = "";
        internal string previousHash = "";
        int numberUsedOnce = 0;

        public Block(int index, DateTime time, Data data, string previousHash)
        {
            this.index = index;
            this.time = time;
            this.data = data;
            this.previousHash = previousHash;
            this.hash = CalculateHash();
        }

        public Block(DateTime time, Data data, string previousHash)
        {
            this.time = time;
            this.data = data;
            this.previousHash = previousHash;
            this.hash = CalculateHash();
        }

        public Block(DateTime time, Data data)
        {
            this.time = time;
            this.data = data;
            this.hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using SHA256 sha256 = SHA256.Create();

            string rawData = index.ToString() + time.ToString() + data.Serialize() + previousHash + numberUsedOnce.ToString();
            byte[] inputBytes = Encoding.UTF8.GetBytes(rawData);
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToHexString(outputBytes);
        }

        public void MineBlock(int diff)
        {
            string zeros = new string('0', diff);
            while (hash.Substring(0, diff) != zeros)
            {
                numberUsedOnce++;
                hash = CalculateHash();
            }
        }
    }
}
