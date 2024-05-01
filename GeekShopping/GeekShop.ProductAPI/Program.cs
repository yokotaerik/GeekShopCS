
using AutoMapper;
using GeekShop.ProductAPI.Config;
using GeekShop.ProductAPI.Model.Context;
using GeekShop.ProductAPI.Repostory;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];
            // Add services to the container.

            builder.Services.AddDbContext<MySQLContext>(options => options
                .UseMySql(
                    connection,
                    ServerVersion.AutoDetect(connection)));

            // Registre os mapeamentos e crie um IMapper
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

            // Adicione o IMapper como serviço singleton
            builder.Services.AddSingleton(mapper);

            // Adicione o AutoMapper com os assemblies necessários
            // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
