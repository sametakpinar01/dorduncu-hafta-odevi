using dorduncu_hafta_odevi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "dorduncu-hafta-odevi",
        Version = "v1"
    });
    // Metotlara göre sıralama
    options.OrderActionsBy(apiDesc =>
    {
        var method = apiDesc.HttpMethod?.ToLowerInvariant();

        return method switch
        {
            "post" => "1",   // Create
            "get" => "2",    // Read
            "put" => "3",    // Update
            "delete" => "4", // Delete
            _ => "99"
        };
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
