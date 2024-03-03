using Application;
using AttendanceRegister.Extensions;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebUi();

var app = builder.Build();

if (app.Environment.IsDevelopment() == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.SeedData();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapCustomControllers();
app.Run();