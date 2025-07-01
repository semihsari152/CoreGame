using InfrastructureLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Persistence layer
builder.Services.AddInfrastructure(builder.Configuration);

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