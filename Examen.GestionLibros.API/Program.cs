using Examen.GestionLibros.AccesoDatos;
using Examen.GestionLibros.Negocio.Implementaciones;
using Examen.GestionLibros.Negocio.Interfaces;
using Examen.GestionLibros.Negocio.Mappers;
using Examen.GestionLibros.Repositorios.Implementaciones;
using Examen.GestionLibros.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

string cn=builder.Configuration.GetConnectionString("cn");
builder.Services.AddDbContext<BdlibrosContext>(opt =>
{
    opt.UseSqlServer(cn);   
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Examen Gestion Libros API",
        Version = "v1",
        Description = "API para gestión de libros y tipos de libro"
    });
});

builder.Services.AddOpenApi();


builder.Services.Scan(scan =>
    scan.FromAssemblies(
        typeof(ITipoLibroRepositorio).Assembly,
        typeof(ITipoLibroNegocio).Assembly)
    .AddClasses(false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithScopedLifetime()
);

//builder.Services.AddScoped<ITipoLibroRepositorio, TipoLibroRepositorio>();
//builder.Services.AddScoped<IAutorRepositorio, AutorRepositorio>();
//builder.Services.AddScoped<ILibroRepositorio, LibroRepositorio>();



//builder.Services.AddScoped<ITipoLibroNegocio, TipoLibroNegocio>();
//builder.Services.AddScoped<IAutorNegocio, AutorNegocio>();
//builder.Services.AddScoped<ILibroNegocio, LibroNegocio>();


builder.Services.AddAutoMapper(conf =>
{
    conf.AddMaps(typeof(TipoLibroProfile).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();           
    app.UseSwaggerUI(c =>       
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examen Gestion Libros API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
