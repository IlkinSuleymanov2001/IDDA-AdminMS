using Application;
using Infastructure;
using AdminApi.Commons.Extensions;
using Core.Exceptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationsServices();
builder.Services.AddInfastructureServices(builder.Configuration);
builder.Services.AddSwagger();
//builder.Services.AddJwtBearer(builder.Configuration);


builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

// Customise default API behaviour
/*builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});*/



//builder.Services.AddDatabaseDeveloperPageExceptionFilter()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
  //  app.UseMigrationsEndPoint();
}
else
    app.UseHsts();


//app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(builder =>
{
    builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
});

//app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//await app.ApplyMigrations();

app.Run();

