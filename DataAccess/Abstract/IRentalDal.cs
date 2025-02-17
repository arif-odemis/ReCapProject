﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
	public interface IRentalDal : IEntityRepository<Rental>
	{
		void Deliver(Rental rental);
		List<RentalDetailDto> GetRentalDetails();
	}
}
