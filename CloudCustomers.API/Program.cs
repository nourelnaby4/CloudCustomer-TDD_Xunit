using CloudCustomers.API.Config;
using CloudCustomers.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
AddDependenciesService(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

void AddDependenciesService(IServiceCollection services)
{
    services.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApiOptions"));
    services.AddScoped<IUserServices, UserServices>();
    services.AddHttpClient<IUserServices, UserServices>();
}