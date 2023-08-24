using IAMRefactor.Application.Interface;
using IAMRefactor.Application.Service.ALM;
using IAMRefactor.Application.Service.SignatureBussiness;
using IAMRefactor.Application.Service.SMAL;
using IAMRefactor.Infrastructure.implementation;
using IAMRefactor.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped<IAMRefactor.Application.Interface.IConfigurationProvider, IAMRefactor.Infrastructure.implementation.ConfigurationProvider>();
//builder.Services.AddScoped<ISignatureProviderFactory, SignatureProviderFactory>();
builder.Services.AddScoped<ISignatureService, SignatureService>();
builder.Services.AddScoped<IALMService, ALMService>();
//builder.Services.AddScoped<ISamlService, SamlService>();

builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IAM Core");
});

app.Run();
