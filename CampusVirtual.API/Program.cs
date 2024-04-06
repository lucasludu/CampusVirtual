using CampusVirtual.API.Util;
using CampusVirtual.API.Util.Interface;
using CampusVirtual.Data;
using CampusVirtual.Models.Mapper;
using CampusVirtual.Negocio.Repositorio;
using CampusVirtual.Negocio.Repositorio.Interface;
using CampusVirtual.Negocio.UOW;
using CampusVirtual.Negocio.UOW.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(Options => 
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("connectionCV"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
builder.Services.AddScoped<ICarreraNegocio, CarreraNegocio>();
builder.Services.AddScoped<IMateriaNegocio, MateriaNegocio>();
builder.Services.AddScoped<IRolNegocio    , RolNegocio>();
builder.Services.AddScoped<IUnitOfWork    , UnitOfWork>();
builder.Services.AddScoped<IExcelUtil     , ExcelUtil>();
builder.Services.AddScoped<IUsuarioUtil   , UsuarioUtil>();

builder.Services.AddAutoMapper(typeof(MapperCV));


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("ApiAuthCV", new OpenApiInfo
    {
        Title = "Api Auth",
        Version = "1",
        Description = "Backend Campus Virtual"
    });
    c.SwaggerDoc("ApiCarreraCV", new OpenApiInfo
    {
        Title = "Api Carrera",
        Version = "1",
        Description = "Backend Campus Virtual"
    });
    c.SwaggerDoc("ApiMateriaCV", new OpenApiInfo
    {
        Title = "Api Materias",
        Version = "1",
        Description = "Backend Campus Virtual"
    });
    c.SwaggerDoc("ApiUsuarioCV", new OpenApiInfo
    {
        Title = "Api Usuario",
        Version = "1",
        Description = "Backend Campus Virtual"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthorization(op => 
{
    op.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/ApiAuthCV/swagger.json"   , "Api Auth");
        c.SwaggerEndpoint("/swagger/ApiCarreraCV/swagger.json", "Api Carrera");
        c.SwaggerEndpoint("/swagger/ApiMateriaCV/swagger.json", "Api Materia");
        c.SwaggerEndpoint("/swagger/ApiUsuarioCV/swagger.json", "Api Usuario");
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
