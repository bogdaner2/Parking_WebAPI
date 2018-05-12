using System;
using System.Collections.Generic;

namespace Parking.Data
{
    public class Transaction
    {
        public DateTime Time { get; }
        public int Id { get; }
        public double Fee { get; }

        public Transaction(int id,double fee)
        {
            Time = DateTime.Now;
            Id = id;
            Fee = fee;
        }

        public override string ToString()
        {
            return string.Format($"Car Id:{Id.ToString().Substring(Id.ToString().Length - 5)} | Paid {Fee} $ at {Time}");
        }

        public static List<Transaction> TransactionsForCurtainCar(int id,Parking parking) =>
             parking.Transactions.FindAll(x => x.Id == id);
    }
}
