using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Data;
using PeterWongClientRestApi.Services;

// https://learn.microsoft.com/en-gb/aspnet/core/web-api/?view=aspnetcore-7.0#apicontroller-attribute
[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClientContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ClientContext")));
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ClientContext>();
    
    context.Database.EnsureCreated();
    ClientDbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
