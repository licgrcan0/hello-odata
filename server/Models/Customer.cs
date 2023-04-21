namespace server.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public CustomerType CustomerType { get; set; }
    public decimal CreditLimit { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Order> Orders { get; set; }
}