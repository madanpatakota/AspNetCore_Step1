var builder = WebApplication.CreateBuilder();

// Register the built-in OpenAPI document generator.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Expose the OpenAPI document at /openapi/v1.json.

//Expose endponts at /openapi/v1.json

// User interface for openapi document at /swagger/index.html



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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

app.MapPost("/Createproduct", (Product product) => {
    // Implementation for creating a new product
    List<Product> products = new List<Product>();
    products.Add(product);
    return products;
});

//Update Product
app.MapPut("/Updateproduct/{id}", (int id, Product updatedProduct) =>
{
    // Implementation for updating a product
    return new Product(id, updatedProduct.Name, updatedProduct.Price);
});


app.MapPatch("/Patchproduct/{id}", (int id, Product updatedProduct) =>
{
    // Implementation for patching a product
    return new Product(id, updatedProduct.Name, updatedProduct.Price);
});

app.MapDelete("/Deleteproduct/{id}", (int id) =>
{
    // Implementation for deleting a product
    return $"Product with ID {id} has been deleted.";
});

////prepare the swagger
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.Run();
public record Product(int Id, string Name, decimal Price);
