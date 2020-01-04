namespace Shop.DAL.Models
{
    public class RentalSpaces
    {
        private static int id = 1;
        public RentalSpaces(string title, decimal rental)
        {
            Rental = rental;
            Title = title;

            Id = id;
            ++id;
        }

        public int Id { get; private set; }

        public decimal Rental { get; private set; }
        public string Title { get; private set; }
    }
}
