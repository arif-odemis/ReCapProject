﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
	public class AspectInterceptorSelector : IInterceptorSelector
	{
		//[]'ları çalıştırmak içindir
		public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
		{
			var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
				(true).ToList();
			var methodAttributes = type.GetMethod(method.Name)
				.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
			classAttributes.AddRange(methodAttributes);

			//SİSTEMDEKİ HER ŞEYİ KONTROL EDER 
			//classAttributes.Add(new PerformanceAspect(10));

			return classAttributes.OrderBy(x => x.Priority).ToArray();
		}
	}
}
