using System;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatorBoilerplate.Domain.Core.Pipeline.Validation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace MediatorBoilerplate
{
    public class Startup
    {
        private readonly Container _container = new();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();

                options.AddLogging();
                options.AddLocalization();
            });

            services.UseSimpleInjectorAspNetRequestScoping(_container);

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            object Get(Type requestedType)
            {
                return _container.GetInstance(requestedType);
            }

            _container.RegisterSingleton<IMediator>(() => new Mediator(Get));
            _container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(MessageValidationBehavior<,>)
            });
            _container.Register(typeof(IRequestHandler<,>), Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            _container.Verify();
        }
    }
}