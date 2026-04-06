using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class BlockChain
    {
        int index = 0;
        public List<Block> chain = new List<Block>();
        public int diff;
        public List<Data> pendingTrans = [];
        public double miningReward;

        public BlockChain(int difficulty)
        {
            diff = difficulty;
            chain = new List<Block>();
            chain.Add(AddGenesis());
            pendingTrans = new List<Data>();
            pendingTrans = [new Data("", "", 0)];
            miningReward = 100;

        }

        public Block AddGenesis()
        {
            return new Block(0, DateTime.Now, new Data("g", "g", 0.0), "");
        }

        public Block GetLastestBlock()
        {
            return chain[chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            index++;
            block.previousHash = GetLastestBlock().hash;
            block.MineBlock(diff);
            chain.Add(new Block(index, block.time, block.data, block.previousHash));
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < chain.Count; i++)
            {
                Block currentBlock = chain[i];
                Block previousBlock = chain[i - 1];

                if( currentBlock.hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.previousHash != previousBlock.CalculateHash())
                {
                    return false;
                }
            }
            return true;
        }

        public void MinePendingTrans(string address)
        {
            Block block = new Block(DateTime.Now, pendingTrans[0]);
            block.MineBlock(this.diff);

            Console.WriteLine("Block mined");
            chain.Add(block);

            this.pendingTrans = [new Data(address, "", miningReward)];

        }

        public void CreateTrans(Data trans)
        {
            this.pendingTrans.Add(trans);
        }

        public double GetBalanceOfAddress(string address)
        {
            double balance = 0;

            foreach (var block in this.chain)
            {
                if(block.data.sender == address) balance -= block.data.amount;

                if(block.data.reciever == address) balance += block.data.amount;

            }
            return balance;
        }
    }
}
