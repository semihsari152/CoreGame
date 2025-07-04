using ApplicationLayer;     
using InfrastructureLayer;  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application and Infrastructure layers
builder.Services.AddApplication();                    // ? Application layer
builder.Services.AddInfrastructure(builder.Configuration); // ? Infrastructure layer

var app = builder.Build();

// Configure the pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Identity için
app.UseAuthorization();
app.MapControllers();

app.Run();