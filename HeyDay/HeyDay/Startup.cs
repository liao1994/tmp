using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HeyDay.Controllers;
using Microsoft.Net.Http.Headers;

namespace HeyDay
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		private string BearerToken =>
			"AAAAAAAAAAAAAAAAAAAAAKohYAEAAAAAaliWCjKsABFGrji%2FcbDe9z0CbL8%3Dvsnr45VF92rTTN3RMTrHwnAVbPUO5UqlVZW4zTdTlaXGT3XGwX";
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddHttpClient("Twitter", httpClient =>
			{
				httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
				httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BearerToken}");
			});
			services.AddTransient<TwitterService>();
			services.AddMemoryCache();
			services.AddRouting(options => options.LowercaseUrls = true);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
