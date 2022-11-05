using Business.Abstract;
using Business.Constants;
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

		public IResult Add(Brand brand)
		{
			_brandDal.Add(brand);
			return new SuccessResult(Messages.BrandAdded);
		}

		public IResult Delete(Brand brand)
		{
			if (brand == null)
				return new ErrorResult(Messages.BrandNotFound);
			else
			{
				_brandDal.Delete(brand);
				return new SuccessResult(Messages.BrandDeleted);
			}

		}
		public IResult Update(Brand brand)
		{
			if (brand == null)
				return new ErrorResult(Messages.BrandNotFound);
			else
			{
				_brandDal.Update(brand);
				return new SuccessResult(Messages.BrandUpdated);
			}
		}
		public IDataResult<List<Brand>> GetAll()
		{

			return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
		}

	

		public IDataResult<Brand> GetByBrandId(Expression<Func<Brand, bool>> filter)
		{
			throw new NotImplementedException();
		}
	}
}
