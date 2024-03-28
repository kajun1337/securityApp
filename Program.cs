using securityApp.Helper;
using securityApp.Interfaces.AbuseIpDbInterfaces;
using securityApp.Interfaces.IHybridAnalysesRepository;
using securityApp.Interfaces.VirusTotalInterfaces;
using securityApp.Repositories.AbuseIpDbRepository;
using securityApp.Repositories.HybridAnalysesRepository;
using securityApp.Repositories.VirusTotalRepository;

var SpecificOrigins = "AllowSpecificOrigin";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IVirusTotalLinkRepository, VirusTotalLinkRepository>();
builder.Services.AddScoped<IVirusTotalFileRepository, VirusTotalFileRepository>();
builder.Services.AddScoped<IVirusTotalIpAddressRepository, VirusTotalIpAddressRepository>();
builder.Services.AddScoped<IAbuseIpDbIpRepository, AbuseIpDbIpRepository>();
builder.Services.AddScoped<IHybridLinkRepository, HybridLinkRepository>();

builder.Services.AddSingleton<HybridAnalysisSettings>(); 
builder.Services.AddSingleton<VirusTotalSettings>();
builder.Services.AddSingleton<Encoder>();
builder.Services.AddSingleton<FileHandler>();
builder.Services.AddSingleton<AbuseIpDbSettings>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7046","http://localhost:5090").AllowAnyMethod().AllowAnyHeader();

        });
});

var app = builder.Build();


//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(SpecificOrigins);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
