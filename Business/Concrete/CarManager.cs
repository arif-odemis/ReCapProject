using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class CarManager : ICarService
	{
		ICarDal _carDal;

		public CarManager(ICarDal carDal)
		{
			_carDal = carDal;
		}
		[SecuredOperation("car.add,admin")]
		[ValidationAspect(typeof(CarValidator))]
		[CacheRemoveAspect("ICarService.Get")]
		public IResult Add(Car car)
		{
			if (car.Description.Length < 2 || car.DailyPrice < 0)
				return new ErrorResult(Messages.CarNameInvalid);
			else
			{
				_carDal.Add(car);
				return new SuccessResult(Messages.CarAdded);
			}

		}

		[SecuredOperation("admin")]
		[CacheRemoveAspect("ICarService.Get")]
		public IResult Delete(Car car)
		{

			_carDal.Delete(car);
			return new SuccessResult(Messages.CarDeleted);


		}
		[SecuredOperation("admin")]
		[ValidationAspect(typeof(CarValidator))]
		[CacheRemoveAspect("ICarService.Get")]
		public IResult Update(Car car)
		{
			_carDal.Update(car);
			return new SuccessResult(Messages.CarUpdated);

		}
		[CacheAspect]
		[PerformanceAspect(5)]
		public IDataResult<List<Car>> GetAll()
		{
			
			return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
		}

		public IDataResult<Car> GetById(int id)
		{
			return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id), Messages.TheCarListed);
		}

		public IDataResult<List<CarDetailDto>> GetCarDetails()
		{
			return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(),Messages.CarsListed);
		}

		public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
		{
			return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListed);
		}

		public IDataResult<List<Car>> GetCarsByColorId(int colorId)
		{
			return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId),Messages.CarsListed);
		}

		public IDataResult<CarDetailDto> GetCarDetailById(int carId)
		{
			return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetailById(carId), Messages.TheCarListed);
		}

		public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
		{
			return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandId(brandId), Messages.CarsListed);
		}

		public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
		{
			return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColorId(colorId), Messages.CarsListed);
		}
	}
}
