using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CarManager carManager = new CarManager(new EfCarDal());
			carManager.Add(new Car { CarId = 5, BrandId = 5, Description = "X", ColorId = 1, DailyPrice = 0, ModelYear = 2008 }); //Araba eklenmicek istenilen şartlara uymuyor.
		}

	}
}
