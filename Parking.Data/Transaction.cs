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

        public static List<Transaction> TransactionsForCurtainCar(int id,Parking parking) =>
             parking.Transactions.FindAll(x => x.Id == id);
    }
}
