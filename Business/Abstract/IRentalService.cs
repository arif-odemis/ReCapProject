﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
	public interface IRentalService
	{
		IResult Add(Rental rental);
		IResult Update(Rental rental);
		IResult Delete(Rental rental);
		IDataResult<List<Rental>> GetAll();
		IDataResult<Rental> GetById(int rentalId);
		IDataResult<List<Rental>> GetRentalsByCarId(int carId);
		IResult Deliver(Rental rental);
		IDataResult<List<RentalDetailDto>> GetRentalDetails();

	}
}
