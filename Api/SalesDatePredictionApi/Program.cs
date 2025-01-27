using Application.Service;
using Domain.Interfaces;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); 
    });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<AppDbContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new AppDbContext(connectionString);
});
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<IEmployeesAppService, EmployeesAppService>();
builder.Services.AddScoped<IOrdersAppService, OrdersAppService>();
builder.Services.AddScoped<IProductsAppService, ProductsAppService>();
builder.Services.AddScoped<IShippersAppService, ShippersAppService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IShippersRepository, ShippersRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
});

app.UseRouting();

app.UseHttpsRedirection();
app.UseCors("AllowAllCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
