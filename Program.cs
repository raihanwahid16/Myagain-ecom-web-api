using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//এটি ডটনেটকে বলে যে প্রজেক্টে কন্ট্রোলার সার্ভিস ব্যবহার করা হচ্ছে।
builder.Services.AddControllers();

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



app.UseHttpsRedirection();

//এটি ডটনেটকে বলে যে সব কন্ট্রোলারের ভেতরে থাকা রুট অ্যাক্টিভেট করো।)
app.MapControllers();

app.Run();

