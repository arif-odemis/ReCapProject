﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
	public interface IBrandService
	{
		IResult Add(Brand brand);
		IResult Update(Brand brand);
		IResult Delete(Brand brand);
		IDataResult<List<Brand>> GetAll();
		IDataResult<Brand> GetById(int brandId);
		IResult TransactionalOperation(Brand brand);
	}
}
