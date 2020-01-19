using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
using RabbitMQ.Client;

namespace CqrsTodo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(Startup).GetType().Assembly);
            services.AddTransient<IRequestHandler<AddTodoCommand,Guid>,AddTodoCommandHandler>();
            services.AddTransient<IRequestHandler<CompleteTodoCommand,bool>, CompleteTodoCommandHadler>();
            services.AddDbContext<TodoDbContext>(opt=>opt.UseInMemoryDatabase("todo"));

            var rabbitMqConnectionFactory = new ConnectionFactory();
            Configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConnectionFactory);
            services.AddSingleton<IConnectionFactory, RabbitMQ.Client.ConnectionFactory>(x => rabbitMqConnectionFactory);
            services.AddTransient<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
            services.AddTransient<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            
            services.AddTransient<IEventBus, EventBusRabbitMQ>();
            
        }

        // This method gets called by the   runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
