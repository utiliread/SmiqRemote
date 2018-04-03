using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using SmiqServer.InputFormatters;

namespace SmiqServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new HexInputFormatter());
                options.InputFormatters.Add(new RawInputFormatter());
            })
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new StringEnumConverter(true)));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Title = "Vector Signal Generator",
                    Version = "v1"
                });
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(x => x.FullName.Replace("SmiqServer.Features.", string.Empty));
            });

            services.Configure<InstrumentOptions>(Configuration.GetSection("Instrument"));
            services.AddSingleton<InstrumentHostedService>();
            services.AddSingleton<IHostedService>(x => x.GetService<InstrumentHostedService>());
            services.AddSingleton<IInstrument>(x => x.GetService<InstrumentHostedService>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseMvc()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vector Signal Generator"));
        }
    }
}
