using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.OData.ModelBuilder;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntitySet<Customer>("Customers");
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

var app = builder.Build();

// Enable batching of odata operations/jobs as a single request
// batch requests made by POSTing to /$batch endpoint
app.UseODataBatching();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
