using IAMRefactor.Infrastructure;
using IAMRefactor.Application;
using IAMRefactor.Middleware;
using IAMRefactor.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();


builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IAM Core");
});

app.Run();
