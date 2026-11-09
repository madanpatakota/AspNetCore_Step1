using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder();

// Register the built-in OpenAPI document generator.
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose the OpenAPI document at /openapi/v1.json.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Expose the OpenAPI document at /openapi/v1.json.
    app.MapScalarApiReference(); // Expose the Scalar API reference at /scalar-api-reference.
}

app.MapGet("/", () => "API is running.");



app.MapGet("/products", () => new[] {
      new { Id = 1, Name = "Laptop", Price = 999.99m },
      new { Id = 2, Name = "Keyboard", Price = 49.99m },
      new { Id = 3, Name = "Mouse", Price = 24.99m } })
    .WithSummary("Gets all products")
    .WithDescription("Returns the available products.");

app.MapGet("/products/{id}", (int id) => new
{
    Id = id,
    Name = $"Product {id}",
    Price = 19.99m
})
    .WithSummary("Gets a product by ID")
    .WithDescription("Returns a product for the specified ID.");

app.MapPost("/Createproduct", (Product product) =>
{
    // Implementation for creating a new product
    List<Product> products = new List<Product>();
    products.Add(product);
    products.Add(product);

    return products;
})
.WithSummary("Creates a new product")
.WithDescription("Creates a new product with the provided details.");


//Update Product
app.MapPut("/Updateproduct/{id}", (int id, Product updatedProduct) =>
{
    // Implementation for updating a product
    return new Product(id, updatedProduct.Name, updatedProduct.Price);
})
.WithSummary("Updates a product by ID")
.WithDescription("Updates the details of a product for the specified ID.");


app.MapPatch("/Patchproduct/{id}", (int id, Product updatedProduct) =>
{
    // Implementation for patching a product
    return new Product(id, updatedProduct.Name, updatedProduct.Price);
})
    .WithSummary("Patches a product by ID")
    .WithDescription("Patches the details of a product for the specified ID.");

app.MapDelete("/Deleteproduct/{id}", (int id) =>
{
    // Implementation for deleting a product
    return $"Product with ID {id} has been deleted.";
})
    .WithSummary("Deletes a product by ID")
    .WithDescription("Deletes the product for the specified ID.");


//prepare the swagger

app.Run();

//Explain this while we are working in Middleware and Routing. This is the code that we will be using to create our API. We are using the minimal API approach in ASP.NET Core. We are defining our endpoints using the MapGet, MapPost, MapPut, MapPatch, and MapDelete methods. Each endpoint has a summary and description for documentation purposes. The Product record is defined at the end of the file to represent the product entity.

//app.UseHttpsRedirection();
//Use of the usehttprediretion is that it is a middleware that redirects HTTP requests to HTTPS. This is important for security, as it ensures that all communication between the client and server is encrypted. In this code, we have commented out the UseHttpsRedirection method, but in a production environment, it is recommended to enable it to enforce secure connections.


//public record Product(int Id, string Name, decimal Price); both are fine ..
public class Product
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

// what is record here ?
// In C#, a `record` is a reference type that provides built-in functionality for encapsulating data. It is designed to be immutable by default, meaning that once an instance of a record is created, its properties cannot be changed. Records are particularly useful for defining simple data structures that primarily hold data and do not require complex behavior.
