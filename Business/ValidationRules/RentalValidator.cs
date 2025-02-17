﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules
{
	public class RentalValidator : AbstractValidator<Rental>
	{
		public RentalValidator()
		{
			RuleFor(r => r.RentalId).NotEmpty();
			RuleFor(r => r.CarId).NotEmpty();
			RuleFor(r => r.RentDate).NotEmpty();
			RuleFor(r => r.ReturnDate).NotEmpty();
			RuleFor(r => r.ReturnDate).LessThan(DateTime.Now);
			RuleFor(r => r.RentDate).GreaterThan(r => r.ReturnDate);
		}

	}
}
