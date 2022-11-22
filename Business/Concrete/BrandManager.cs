using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
	public class BrandManager : IBrandService
	{
		IBrandDal _brandDal;

		public BrandManager(IBrandDal brandDal)
		{
			_brandDal = brandDal;
		}

		[SecuredOperation("brand.add,admin")]
		[ValidationAspect(typeof(BrandValidator))]
		[CacheRemoveAspect("IBrandService.Get")]
		public IResult Add(Brand brand)
		{
			var ruleResult = BusinessRules.Run(CheckIfBrandNameAlreadyExist(brand.BrandName));
			if (ruleResult != null)
			{
				return new ErrorResult(ruleResult.Message);
			}
			_brandDal.Add(brand);
			return new SuccessResult(Messages.BrandAdded);
		}

		[CacheRemoveAspect("IBrandService.Get")]
		public IResult Delete(Brand brand)
		{
			_brandDal.Delete(brand);
			return new SuccessResult(Messages.BrandDeleted);
		}

		[CacheAspect]
		public IDataResult<List<Brand>> GetAll()
		{
			return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandsListed);
		}

		[CacheAspect]
		public IDataResult<Brand> GetById(int brandId)
		{
			return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId), Messages.TheBrandListed);
		}

		[TransactionScopeAspect]
		public IResult TransactionalOperation(Brand brand)
		{
			_brandDal.Update(brand);
			_brandDal.Add(brand);
			return new SuccessResult(Messages.BrandUpdated);
		}

		[ValidationAspect(typeof(BrandValidator))]
		[CacheRemoveAspect("IBrandService.Get")]
		public IResult Update(Brand brand)
		{
			_brandDal.Update(brand);
			return new SuccessResult(Messages.BrandUpdated);
		}

		
		private IResult CheckIfBrandNameAlreadyExist(string brandName)
		{
			var result = _brandDal.GetAll(p => p.BrandName == brandName).Any();
			if (result)
			{
				return new ErrorResult(Messages.BrandNameAlreadyExist);
			}
			return new SuccessResult();
		}
	}
}
