var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); // Swagger’ý etkinleþtirir

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger middleware
    app.UseSwaggerUI(); // Swagger UI middleware
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();