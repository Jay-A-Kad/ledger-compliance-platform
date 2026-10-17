using System.Reflection;
using FluentValidation;
using LedgerPlatform.Application;
using LedgerPlatform.Infrastructure;
using Microsoft.EntityFrameworkCore;
using LedgerPlatform.Application.Behaviors;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");




builder.Services.AddOpenApi();


//dbcontext
builder.Services.AddDbContext<LedgerPlatformDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<LedgerPlatformDbContext>());

//mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OptimisticRetryBehavior<,>));

//fluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(IApplicationDbContext).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline. t shows t
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();

