using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddRazorPages();
			services.AddControllersWithViews();

			services.AddLazyCache();

			services.AddSingleton<LocationProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapRazorPages();
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=AirQuality}/{action=Index}/{id?}");
			});

			var locationProvider = app.ApplicationServices.GetRequiredService<LocationProvider>();
			await ConfigureLocationProvider(locationProvider);
		}

		private static async Task ConfigureLocationProvider(LocationProvider locationProvider) {
			await locationProvider.DownloadLocations();
		}
	}
}
