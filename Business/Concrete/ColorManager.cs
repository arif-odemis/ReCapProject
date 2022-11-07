using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
	public class ColorManager : IColorService
	{
		IColorDal _colorDal;

		public ColorManager(IColorDal colorDal)
		{
			_colorDal = colorDal;
		}

		public IResult Add(Color color)
		{
			ValidationTool.Validate(new CarValidator(), color);
			_colorDal.Add(color);
			return new SuccessResult(Messages.ColorAdded);

		}

		public IResult Delete(Color color)
		{
			if (color == null)
				return new ErrorResult(Messages.ColorNotFound);
			else
			{
				_colorDal.Delete(color);
				return new SuccessResult(Messages.ColorDeleted);
			}


		}

		public IResult Update(Color color)
		{
			if (color == null)
				return new ErrorResult(Messages.ColorNotFound);
			else
			{
				_colorDal.Update(color);
				return new SuccessResult(Messages.CarUpdated);
			}
		}

		public IDataResult<List<Color>> GetAll()
		{
			return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
		}

		public IDataResult<List<Color>> GetByColorId(int id)
		{
			return new SuccessDataResult<List<Color>>(_colorDal.GetAll().Where(c => c.ColorId == id).ToList());
		}
	}
}
