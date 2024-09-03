using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.IRepository;
using Repository.Mapping;
using Repository.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5181")  // Replace with your actual frontend URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Pleasw Enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }

    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddTransient<IBuildingRepository, BuildingRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IRoomRepository, RoomRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IMajorRepository, MajorRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IDayRepository, DayRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ISLotRepository, SlotRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IAccountRepository, AccountRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IAdminRepository, AdminRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IRoleRepository, RoleRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ICurriculumRepository, CurriculumRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ICourseRepository, CourseRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ISemesterRepository, SemesterRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IGradeRepository, GradeRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ITermRepository, TermRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IStudentRepository, StudentRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<IStatusRepository, StatusRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));
builder.Services.AddTransient<ITypeRepository, TypeRepository>()
    .AddDbContext<StudentManagementContext>(opt => builder.Configuration.GetConnectionString("MyCnn"));



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
// Middleware kiểm tra token
//app.Use(async (context, next) =>
//{
//    // Bỏ qua các yêu cầu đến API login
//    if (context.Request.Path == "/api/Authen/Login" && context.Request.Method.ToLower() == "post")
//    {
//        await next();
//        return;
//    }

//    var authHeader = context.Request.Headers["Authorization"];
//    if (authHeader.Count == 0 || string.IsNullOrEmpty(authHeader[0]))
//    {
//        context.Response.StatusCode = 401; // Unauthorized
//        await context.Response.WriteAsync("Missing Authorization Header");
//        return;
//    }

//    var token = authHeader[0].Split(' ')[1]; // Lấy phần token từ header Authorization

//    var tokenHandler = new JwtSecurityTokenHandler();
//    var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"]);

//    try
//    {
//        tokenHandler.ValidateToken(token, new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//            ValidAudience = builder.Configuration["JWT:ValidAudience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key)
//        }, out SecurityToken validatedToken);

//        // Nếu token hợp lệ, chuyển tiếp cho các middleware tiếp theo
//        await next();
//    }
//    catch (Exception ex)
//    {
//        context.Response.StatusCode = 401; // Unauthorized
//        await context.Response.WriteAsync("Invalid Token: " + ex.Message);
//    }
//});

app.MapControllers();

app.Run();
