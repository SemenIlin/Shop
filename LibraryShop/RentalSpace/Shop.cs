namespace LibraryShop.RentalSpace
{
    public class Shop:IRentalSpace
    {
        public Shop(decimal rental)
        {
            Rental = rental;
        }

        public decimal Rental { get; set; }
    }
}
