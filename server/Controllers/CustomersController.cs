using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using server.Data;
using server.Models;

namespace server.Controllers;

public class CustomersController: ODataController
{
    private readonly BasicCrudDbContext db;

    public CustomersController(BasicCrudDbContext db)
    {
        this.db = db;
    }

    [EnableQuery]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        return Ok(db.Customers);
    }
    
    [EnableQuery]
    public ActionResult<Customer> Get([FromRoute] int key)
    {
        var item = db.Customers.SingleOrDefault(d => d.Id.Equals(key));
    
        if (item == null)
        {
            return NotFound();
        }
    
        return Ok(item);
    }
    
    public ActionResult Post([FromBody] Customer item)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        db.Customers.Add(item);
        db.SaveChanges();
    
        return Created(item);
    }
    
    public ActionResult Put([FromRoute] int key, [FromBody] Customer item)
    {
        var customer = db.Customers.SingleOrDefault(d => d.Id == key);

        if (customer == null)
        {
            return NotFound();
        }

        customer.Name = item.Name;
        customer.CustomerType = item.CustomerType;
        customer.CreditLimit = item.CreditLimit;
        customer.CreatedAt = item.CreatedAt;

        db.SaveChanges();

        return Updated(customer);

        db.Customers.Update(item);
        db.SaveChanges();
    }
    
    public ActionResult Patch([FromRoute] int key, [FromBody] Delta<Customer> delta)
    {
        var customer = db.Customers.SingleOrDefault(d => d.Id == key);

        if (customer == null)
        {
            return NotFound();
        }

        delta.Patch(customer);

        db.SaveChanges();

        return Updated(customer);
    }
    
    public ActionResult Delete([FromRoute] int key)
    {
        var customer = db.Customers.SingleOrDefault(d => d.Id == key);

        if (customer != null)
        {
            db.Customers.Remove(customer);
        }

        db.SaveChanges();

        return NoContent();
    }
}