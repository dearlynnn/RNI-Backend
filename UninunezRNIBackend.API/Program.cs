using Microsoft.EntityFrameworkCore;
using UninunezRNIBackend.Data;
using UninunezRNIBackend.Repositories.AgreementsRequest;
using UninunezRNIBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<UninunezRNIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories
builder.Services.AddScoped<IAgreementsRequestRepository, SQLAgreementRequestRepository>();

// Add services
builder.Services.AddScoped<IAgreementRequestService, AgreementRequestService>();
builder.Services.AddScoped<IStatusHistoryService, StatusHistoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
