using recetBac.Data;
using RecetasAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuraci�n de servicios HttpClient
builder.Services.AddHttpClient<NutritionixService>();
builder.Services.AddHttpClient<MealService>();

// Habilitar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configuraci�n de la aplicaci�n
var app = builder.Build();

// Usar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

// Redirigir la p�gina ra�z a Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseCors("AllowAnyOrigin"); // Activar CORS
app.UseAuthorization();
app.MapControllers();

app.Run();
