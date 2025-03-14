using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add Cosmos DB Client
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    var cosmosDbEndpoint = builder.Configuration["CosmosDb:Endpoint"];
    var cosmosDbKey = builder.Configuration["CosmosDb:Key"];
    return new CosmosClient(cosmosDbEndpoint, cosmosDbKey);
});

// Add Application Services
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
