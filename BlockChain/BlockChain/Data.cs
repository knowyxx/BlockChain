using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class Data
    {
        public string reciever = "";
        public string sender = "";
        public double amount;

        public Data(string reciever, string sender, double amount)
        {
            this.reciever = reciever;
            this.sender = sender;
            this.amount = amount;
        }

        public string Serialize()
        {
            string temp = this.reciever + "," + this.sender + "," + this.amount.ToString();
            return temp;
        }
    }
}
