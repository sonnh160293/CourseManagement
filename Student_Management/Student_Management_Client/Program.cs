using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Authentication/SignIn";            //redirect khi het phien dang nhap
        option.SlidingExpiration = true;                        // reset phien dang nhâp khi user gui request
        option.AccessDeniedPath = "/Authentication/SignIn";     //redirect khi truy cap tai nguyen k cho phep
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddHttpClient();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Authentication/SignIn"; // Path to your login page
    options.AccessDeniedPath = "/Authentication/SignIn"; // Path to your access denied page
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(ConstantRole.ROLE_EDU, policy => policy.RequireClaim(ClaimTypes.Role, ConstantRole.ROLE_EDU));
//});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();



app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseMiddleware<AuthorizationMiddleware>();
app.UseRouting();








app.UseAuthentication();  // Validate token
app.UseAuthorization();   // Validate token






app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    // Redirect the root URL to the default page
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/Authentication/SignIn");
    });
});

app.Run();