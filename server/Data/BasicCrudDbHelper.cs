using server.Models;

namespace server.Data;

internal static class BasicCrudDbHelper
{
    public static void SeedDb(BasicCrudDbContext db)
    {
        if (!db.Customers.Any())
        {
            db.Add(new Customer
            {
                Id = 1,
                Name = "Sue",
                CustomerType = CustomerType.Retail,
                Status = "PREMIUM",
                CreditLimit = 3700,
                CreatedAt = new DateTime(2022, 7, 4)
            });

            db.Add(new Customer
            {
                Id = 2,
                Name = "Joe",
                CustomerType = CustomerType.Wholesale,
                Status = "STANDARD",
                CreditLimit = 5100,
                CreatedAt = new DateTime(2022, 12, 12)
            });

            db.SaveChanges();
        }
    }
}