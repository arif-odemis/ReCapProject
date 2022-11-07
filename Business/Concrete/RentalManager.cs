using Business.Abstract;
using Business.ValidationRules;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class RentalManager : IRentalService
	{
		IRentalDal _rentalDal;

		public RentalManager(IRentalDal rentalDal)
		{
			_rentalDal = rentalDal;
		}

		public IResult Add(Rental rental)
		{
			ValidationTool.Validate(new RentalValidator(),rental);	

			_rentalDal.Add(rental);
			return new SuccessResult("Araç Kiralandı");

		}

		public IResult Delete(Rental rental)
		{
			_rentalDal.Delete(rental);
			return new SuccessResult();
		}

		public IDataResult<Rental> Get(int id)
		{
			return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.Id == id));
		}

		public IDataResult<List<Rental>> GetAll()
		{
			return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
		}

		public IResult Update(Rental rental)
		{
			_rentalDal.Update(rental);
			return new SuccessResult();
		}
	}
}
