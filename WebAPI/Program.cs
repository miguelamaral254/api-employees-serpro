using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.Services.EmployeeService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<EmployeeInterface, EmployeeService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("EmployeesApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("EmployeesApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
