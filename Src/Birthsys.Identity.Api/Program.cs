using Birthsys.Identity.Api.Extensions;
using Birthsys.Identity.Api.Extentions;
using Birthsys.Identity.Application;
using Birthsys.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHealthCheckConfig();

builder.Services.AddJwtConfiguration();

builder.Services.AddSwaggerDocumentation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCustomExceptionHandler();

await app.RunAsync();


