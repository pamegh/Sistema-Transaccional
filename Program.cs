using Microsoft.EntityFrameworkCore;
using Proyecto.BD;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()    // Allow any origin
              .AllowAnyMethod()    // Allow any HTTP method
              .AllowAnyHeader();   // Allow any header
    });
});

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppBDContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowAll",

        policy =>

        {

            policy.AllowAnyOrigin()

                  .AllowAnyHeader()

                  .AllowAnyMethod();

        });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Habilita CORS antes de Authorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

