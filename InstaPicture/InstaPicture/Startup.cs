using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaPicture.Helpers;
using InstaPicture.Interfaces;
using InstaPicture.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace InstaPicture
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IHostingEnvironment hostingEnvironment)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IMediaService, MediaService>();
			services.AddTransient<IUserService, UserService>();

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddJsonOptions(options =>
					{
						var resolver = options.SerializerSettings.ContractResolver;

						if (resolver != null)
							((DefaultContractResolver)resolver).NamingStrategy = null; // use real values to serialize
					});

			services.AddCors();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors(options => options.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
											.AllowAnyMethod()
											.AllowAnyHeader());

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseStaticFiles();
			app.UseMvc();

			Finder.InstaApi = Finder.GetInstaClientAsync().GetAwaiter().GetResult();
		}
	}
}
