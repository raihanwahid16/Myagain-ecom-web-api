using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ✅ ১. Controllers এবং Custom Validation Response কনফিগারেশন
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                }).ToList();

            var errorString = string.Join("; ", errors.Select(e => $"{e.Field}: {string.Join(", ", e.Errors)}"));

            return new BadRequestObjectResult(new
            {
                Message = "Validation failed",
                Errors = errorString
            });
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