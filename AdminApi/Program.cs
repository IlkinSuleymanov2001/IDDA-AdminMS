using Application;
using Infastructure;
using AdminApi.Commons.Extensions;
using Core.Exceptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

var app = builder.Build();
// Configure the HTTP request pipeline.

app
.UseSwagger()
.UseSwaggerUI()
.UseHsts();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(builder =>
{
    builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
});

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//await app.ApplyMigrations();
app.Run();

