using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using server.Data;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
var orderEntityType = modelBuilder.EntityType<Order>();
var customerEntityType = modelBuilder.EntitySet<Customer>("Customers");

customerEntityType.EntityType.Collection.Function("GetVIPs")
    .ReturnsCollectionFromEntitySet<Customer>("Customers");
customerEntityType.EntityType.Function("IsRetail")
    .Returns<bool>();

var edmModel = modelBuilder.GetEdmModel();
builder.Services.AddControllers().AddOData(
    options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(null)
        // Add routes under "odata/v1" and "odata" paths with batching enabled
        .AddRouteComponents("odata/v1", edmModel, new DefaultODataBatchHandler())
        .AddRouteComponents("odata", edmModel, new DefaultODataBatchHandler()));

builder.Services.AddDbContext<BasicCrudDbContext>(options => 
    options.UseInMemoryDatabase("BasicCrudDb"));

var app = builder.Build();

// Enable batching of odata operations/jobs as a single request
// batch requests made by POSTing to /$batch endpoint
app.UseODataBatching();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

// Seed database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<BasicCrudDbContext>();
    BasicCrudDbHelper.SeedDb(db);
}

app.Run();
