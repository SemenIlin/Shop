using System;

namespace Shop.DAL.Infrastructure
{
    public class MoneyException:Exception
    {
        public MoneyException(string message, string prop) : base(message)
        {
            Property = prop;
        }

        public string Property { get; protected set; }
    }
}
