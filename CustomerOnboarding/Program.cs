using LoggerLibrary;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Onboarding.Core.Config;
using OnBoarding.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilogLogger();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddControllers();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddService();
builder.Services.AddValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = new Newtonsoft.Json.JsonSerializerSettings();
// This tells your serializer that multiple references are okay.
settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

builder.Services.AddMvcCore()
    .AddApiExplorer().
    AddDataAnnotations()
    .AddCors()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter { AllowIntegerValues = true });
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Error = (sender, args) =>
        {
            throw args?.ErrorContext?.Error;
        };
    });

var app = builder.Build();

app.SerilogPipelineConfig<Program>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
