using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
	public class CoreModule : ICoreModule
	{
		public void Load(IServiceCollection servisCollection)
		{
			servisCollection.AddMemoryCache();
			servisCollection.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
			servisCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
			servisCollection.AddSingleton<Stopwatch>();


		}
	}
}
