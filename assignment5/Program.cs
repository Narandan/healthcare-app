using a5.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// persistence service
builder.Services.AddSingleton<PatientRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
