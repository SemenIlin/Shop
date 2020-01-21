namespace Shop.DAL.Models
{
    public class RentalSpaces
    {
        private static int id = 1;
        public RentalSpaces()
        {
            Id = id;
            ++id;
        }

        public int Id { get; set; }

        public decimal Rental { get; set; }
        public string Title { get; set; }
    }
}
