var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Week3.Web.Endpoints.AuthEndpoints.MapAuthEndpoints(app);

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
