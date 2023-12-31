using TestApp1;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("exchangeRates.json",
        optional: true,
        reloadOnChange: true);

//builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();

builder.Services.Configure<List<Converter>>(builder.Configuration.GetSection("Converters"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
