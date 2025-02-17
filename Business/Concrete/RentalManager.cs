﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class RentalManager : IRentalService
	{
		IRentalDal _rentalDal;
		ICarService _carService;
		ICustomerService _customerService;
		public RentalManager(IRentalDal rentalDal, ICarService carService, ICustomerService customerService)
		{
			_rentalDal = rentalDal;
			_carService = carService;
			_customerService = customerService;
		}

		[ValidationAspect(typeof(RentalValidator))]
		public IResult Add(Rental rental)
		{
			rental.IsRentalCompleted = false;
			var ruleResult = BusinessRules.Run(IsTheCarWhichBeWantedToRentalAvailable(rental.CarId),
				IsCarIdValid(rental.CarId), IsCustomerIdValid(rental.CustomerId));

			if (ruleResult != null)
			{
				return new ErrorResult(ruleResult.Message);
			}
			rental.RentDate = DateTime.Now;
			_rentalDal.Add(rental);
			return new SuccessResult(Messages.RentalAdded);
		}

		public IResult Delete(Rental rental)
		{
			_rentalDal.Delete(rental);
			return new SuccessResult(Messages.RentalDeleted);
		}

		[ValidationAspect(typeof(RentalValidator))]
		public IResult Deliver(Rental rental)
		{
			var ruleResult = BusinessRules.Run(IsThereARentalWhichHasIdThatIsGiven(rental.RentalId),
				TheRentalHasDeliveredAlready(rental.RentalId), IsCarIdValid(rental.CarId),
				IsCustomerIdValid(rental.CustomerId));

			if (ruleResult != null)
			{
				return new ErrorResult(ruleResult.Message);
			}
			_rentalDal.Deliver(rental);
			return new SuccessResult(Messages.RentalWasDelivered);
		}

		public IDataResult<List<Rental>> GetAll()
		{
			return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
		}

		public IDataResult<Rental> GetById(int rentalId)
		{
			return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId),Messages.TheRentalListed);
		}

		public IDataResult<List<RentalDetailDto>> GetRentalDetails()
		{
			return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalsListed);
		}

		public IDataResult<List<Rental>> GetRentalsByCarId(int carId)
		{
			return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId), Messages.TheRentalListed);
		}

		[ValidationAspect(typeof(RentalValidator))]
		public IResult Update(Rental rental)
		{
			var ruleResult = BusinessRules.Run(IsCarIdValid(rental.CarId),
				IsCustomerIdValid(rental.CustomerId));

			if (ruleResult != null)
			{
				return new ErrorResult(ruleResult.Message);
			}
			_rentalDal.Update(rental);
			return new SuccessResult(Messages.RentalUpdated);
		}

		// Business Rules
		private IResult IsTheCarWhichBeWantedToRentalAvailable(int carId)
		{
			var rentalHistory = GetRentalsByCarId(carId);
			foreach (var item in rentalHistory.Data)
			{
				if (item.IsRentalCompleted == false)
				{
					return new ErrorResult(Messages.CarIsInAlreadyRental);
				}
			}
			return new SuccessResult();
		}

		private IResult IsCustomerIdValid(int customerId)
		{
			var result = _customerService.GetById(customerId);
			if (result.Data == null)
			{
				return new ErrorResult(Messages.TheCustomerDoesNotExist);
			}
			return new SuccessResult();
		}

		private IResult IsCarIdValid(int carId)
		{
			var result = _carService.GetById(carId);
			if (result.Data == null)
			{
				return new ErrorResult(Messages.TheCarDoesNotExist);
			}
			return new SuccessResult();
		}

		private IResult IsThereARentalWhichHasIdThatIsGiven(int rentalId)
		{
			var rentalHistory = GetById(rentalId);
			if (rentalHistory.Data == null)
			{
				return new ErrorResult(Messages.ThereIsNoRentalWhichHasTheGivenId);
			}
			return new SuccessResult();
		}

		private IResult TheRentalHasDeliveredAlready(int rentalId)
		{
			var rentalHistory = GetById(rentalId);
			if (rentalHistory.Data.IsRentalCompleted == true)
			{
				return new ErrorResult(Messages.RentalHasAlreadyDelivered);
			}
			return new SuccessResult();
		}



	}
}
