using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ✅ OpenAPI সার্ভিস যোগ করা
builder.Services.AddOpenApi();

var app = builder.Build();

// ✅ ডেভেলপমেন্টে OpenAPI এবং Swagger UI সচল করা
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    
    app.UseSwaggerUI(options => {
        options.RoutePrefix = "swagger"; 
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

// ইন-মেমোরি ডাটা লিস্ট
List<Category> categories = new List<Category>();

// 1️⃣ GET /api/categories => সব ক্যাটাগরি দেখা এবং সার্চ করা
app.MapGet("/api/categories", ([FromQuery] string searchValue = "") =>
{
    if (!string.IsNullOrEmpty(searchValue))
    {
        var searchedCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(searchedCategories);
    }

    return Results.Ok(categories);
});

// 2️⃣ POST /api/categories => নতুন ক্যাটাগরি তৈরি করা
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    if (string.IsNullOrEmpty(categoryData.Name))
    {
        return Results.BadRequest("Category name is required and cannot be empty");
    }
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

// 3️⃣ DELETE /api/categories/{categoryId} => ক্যাটাগরি মুছে ফেলা
app.MapDelete("/api/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if (foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});

// 4️⃣ PUT /api/categories/{categoryId} => ক্যাটাগরি আপডেট করা
app.MapPut("/api/categories/{categoryId}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if (foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    if(categoryData == null)
    {
        return Results.BadRequest("Category data is missing");
    }
    if (!string.IsNullOrEmpty(categoryData.Name))
    {
        if (categoryData.Name.Length >= 2)
        {
            foundCategory.Name = categoryData.Name;
        }
        else
        {
            foundCategory.Name = string.Empty; // অথবা আপনি চাইলে এররও রিটার্ন করতে পারেন
            return Results.BadRequest("Category name must be at least 2 characters long");
        }
    }

    if (!string.IsNullOrWhiteSpace(categoryData.Description))
    {
        foundCategory.Description = categoryData.Description;
    }
    return Results.NoContent();
});

app.UseHttpsRedirection();

app.Run();

// =========================================================
// 🟢 নিচে Category ক্লাসটি যোগ করা হলো (যাতে কোনো এরর না আসে)
// =========================================================
public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}