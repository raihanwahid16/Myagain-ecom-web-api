using asp_net_ecommerce_web_api.Controllers;
using Microsoft.AspNetCore.Mvc;
using asp_net_ecommerce_web_api.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CategoryService>();

// Add services to the controller 
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.InvalidModelStateResponseFactory = context =>
  {
    var errors = context.ModelState
                    .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage) : new List<string>()).ToList();
    return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "Validation failed"));
  };
});



// ✅ ২. OpenAPI সার্ভিস যোগ করা
builder.Services.AddOpenApi();

var app = builder.Build();

// ✅ ৩. ডেভেলপমেন্ট এনভায়রনমেন্টে Swagger/OpenAPI সক্রিয় করা
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    
    app.UseSwaggerUI(options => {
        options.RoutePrefix = "swagger"; 
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

app.UseHttpsRedirection();

// ✅ ৪. কন্ট্রোলার রুট ম্যাপ করা
app.MapControllers();

app.Run();