using HelloMySql.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// see https://www.c-sharpcorner.com/article/how-to-connect-mysql-with-asp-net-core/
builder.Services.Add(new ServiceDescriptor(typeof(MusicStoreContext), new MusicStoreContext(builder.Configuration.GetConnectionString("DefaultConnection"))));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
