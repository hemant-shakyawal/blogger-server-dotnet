using blog_api.Data;
using blog_api.Repositories.Implementation;
using blog_api.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnetionSting"));
});
builder.Services.AddScoped<ICategoryRepository, CategoryRepossitory>();// call the categroryrepository

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();// call the BlogPostRepository

builder.Services.AddScoped<IImageRepository, ImageRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();

});

app.UseAuthorization();

app.UseStaticFiles(
    new StaticFileOptions
    {

        FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"images") ),
        RequestPath="/images"

    });

app.MapControllers();

app.Run();
