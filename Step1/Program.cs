using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder();

// Register the built-in OpenAPI document generator.
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose the OpenAPI document at /openapi/v1.json.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
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
public record Product(int Id, string Name, decimal Price);
