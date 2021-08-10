using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzService.HttpService;
using QuartzService.Listeners;
using QuartzService.Listeners.JobListeners;
using QuartzService.Quartz;
using QuartzService.Quartz.Triggers;
using QuartzService.SchedulerFactory;
using QuartzService.SignalR;
using System;

namespace QuartzService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddQuartz(q =>
            {
                q.UsePersistentStore(s =>
                {
                    s.UseProperties = false;
                    s.RetryInterval = TimeSpan.FromSeconds(15);
                    s.UseSqlServer(sql => sql.ConnectionString = Configuration["ConnectionStrings:QuartzServer"]);
                    s.UseJsonSerializer();
                });
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.AddSchedulerListener<SchedulerListener>();
                q.SchedulerName = "QuartzSchedulerService";
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = false);

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.Configure<IISOptions>(p =>
            {
                p.AutomaticAuthentication = true;
                p.ForwardClientCertificate = true;
            });
            services.Configure<HttpServiceSettings>(Configuration.GetSection("HttpServiceSettings"));
            services.AddAuthorization();
            services.AddScoped<IQuartzManager, QuartzManager>();
            services.AddScoped<IAppJobListener, DefaultJobListener>();
            services.AddScoped<IAppJobListener, TaskManagerJobListener>();
            services.AddScoped<IJobListenerResolver, JobListenerResolver>();
            services.AddScoped<IAppSchedulerFactory, AppSchedulerFactory>();
            services.AddTransient<IHttpService, QuartzService.HttpService.HttpService>();
            services.AddScoped<ITriggerStrategy, TriggerStrategy>();
            services.AddScoped<ITriggerCreator, TriggerCreator>();
            services.AddScoped<IJobTrigger, TriggerSingle>();
            services.AddScoped<IJobTrigger, TriggerEveryHour>();
            services.AddScoped<IJobTrigger, TriggerEveryDay>();
            services.AddScoped<IJobTrigger, TriggerEveryWeek>();
            services.AddScoped<IJobTrigger, TriggerEveryMonth>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuartzService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<QuartzServiceHub>("/hub");
            });
        }
    }
}