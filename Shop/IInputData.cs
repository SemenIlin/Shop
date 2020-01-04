namespace Shop
{
    interface IInputData
    {
        decimal Budget { get; }

        decimal SalaryForSeller { get;  }
        decimal SalaryForPorter { get;  }
        decimal SalaryForAccountant { get; }
        decimal SalaryForPurchasingAgent { get; }

        decimal PricePurchaseToy { get; }
        decimal PriceSalesToy { get; }
        int NumberOfToys { get;  }

        decimal ValueRentalSpace { get; }

        int NumberOfMonths { get; }
    }
}
