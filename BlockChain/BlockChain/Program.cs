using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BlockChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Data> transactions = new List<Data>();
            BlockChain blockChain = new BlockChain(2);
            blockChain.AddGenesis();
            Console.WriteLine(blockChain.chain[0].ToString());
            Data data1 = new Data("Max", "Rosta", 100);
            Data data2 = new Data("Max", "Rosta", 1);
            Data data3 = new Data("Rosta", "Max", 10);
            transactions.Add(data1);
            transactions.Add(data2);
            transactions.Add(data3);

            foreach (var item in transactions)
            {
                string data = JsonConvert.SerializeObject(item);

                Console.WriteLine("Adding block:" + data);

                Block block = new Block(DateTime.Now, item, blockChain.GetLastestBlock().hash);
                blockChain.AddBlock(block);
            }
            Console.WriteLine("Checking if blockchain is valid: " + blockChain.IsChainValid());
            Console.WriteLine("Blockchain: " + blockChain);

            blockChain.MinePendingTrans("Max");
            blockChain.MinePendingTrans("Max");
            Console.WriteLine("Balance (Max):" + blockChain.GetBalanceOfAddress("Max"));

            foreach (var item in blockChain.chain)
            {
                Console.WriteLine(item.hash.ToString());
            }
            for (int i = 0; i < 10; i++)
            {
                Block blocky = new Block(DateTime.Now, new Data(i.ToString(), "", i));
                blocky.MineBlock(5);
                Console.WriteLine(blocky.hash);
            }
        }
    }
}
