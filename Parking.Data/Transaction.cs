using System;

namespace Parking.Data
{
    public class Transaction
    {
        public DateTime Time { get; }
        public Guid Id { get; }
        public double Fee { get; }

        public Transaction(Guid id,double fee)
        {
            Time = DateTime.Now;
            Id = id;
            Fee = fee;
        }

        public override string ToString()
        {
            return string.Format($"Car Id:{Id.ToString().Substring(Id.ToString().Length - 5)} | Paid {Fee} $ at {Time}");
        }
    }
}
