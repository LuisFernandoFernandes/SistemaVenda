using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaVenda.DAL;
using Microsoft.EntityFrameworkCore;
using Aplicacao.Servico.Interfaces;
using Aplicacao.Servico;
using Dominio.Interfaces;
using Dominio.Servicos;
using Dominio.Repositorio;
using Repositorio.Entidades;

namespace SistemaVenda
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
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyStock")));
            //provisorio
            services.AddDbContext<Repositorio.Contexto.ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyStock")));

            services.AddHttpContextAccessor();
            services.AddSession();
            //Aplica��o
            services.AddScoped<IServicoAplicacaoCategoria, ServicoAplicacaoCategoria>();
            //Dom�nio
            services.AddScoped<IServicoCategoria, ServicoCategoria>();
            //Reposit�rio
            services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
