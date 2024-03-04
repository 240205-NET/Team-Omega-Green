using Microsoft.AspNetCore.DataProtection.Repositories;
using BRS.Data;
using WishList.Data;
var builder = WebApplication.CreateBuilder(args);


//connection string from appsettings.json
string connectionString = builder.Configuration.GetConnectionString("Book") ?? throw new ArgumentException(nameof(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRecommendationSqlRepository>(sp => new RecommendationSqlRepository(connectionString, sp.GetRequiredService<ILogger<RecommendationSqlRepository>>()));
builder.Services.AddSingleton<IUserRepository>(sp => new UserSqlRepository(connectionString,sp.GetRequiredService<ILogger<UserSqlRepository>>()));

builder.Services.AddSingleton<IHistoryRepository>(sp => new HistorySqlRepository(connectionString, sp.GetRequiredService<ILogger<HistorySqlRepository>>()));

builder.Services.AddSingleton<IReviewRepository>(sp => new SQLReviewRepository(connectionString, sp.GetRequiredService<ILogger<SQLReviewRepository>>()));

builder.Services.AddSingleton<ICategoryRepository>(sp => new CategorySqlRepository(connectionString, sp.GetRequiredService<ILogger<CategorySqlRepository>>()));

builder.Services.AddSingleton<IUserBookPreferenceRepository>(sp => new UserBookPreferenceSqlRepository(connectionString, sp.GetRequiredService<ILogger<UserBookPreferenceSqlRepository>>()));

builder.Services.AddSingleton<IWishRepository>(sp => new WishSQLRepository(connectionString, sp.GetRequiredService<ILogger<WishSQLRepository>>()));

builder.Services.AddSingleton<IBookRepository>(sp => new BookSqlRepository(connectionString, sp.GetRequiredService<ILogger<BookSqlRepository>>()));

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

