using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class CarImageManager : ICarImageService
	{
		ICarImageDal _carImageDal;
		IFileHelper _fileHelper;

		public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
		{
			_carImageDal = carImageDal;
			_fileHelper = fileHelper;
		}

		public IResult AddImage(IFormFile formFile, CarImage carImage)
		{
			var fileUpload = _fileHelper.Add(formFile);
			if (fileUpload.Success)
			{
				carImage.ImagePath = fileUpload.Data;
				carImage.Date = DateTime.Now;
				_carImageDal.Add(carImage);
				return new SuccessResult(Messages.SuccessUploadOfCarImage);
			}
			return new ErrorResult("Error");
		}

		public IResult DeleteImage(CarImage carImage)
		{
			var deleteCarImage = _carImageDal.Get(c => c.Id == carImage.Id);

			_fileHelper.Delete(deleteCarImage.ImagePath);
			_carImageDal.Delete(deleteCarImage);
			return new SuccessResult(Messages.CarImageDeletedSuccessfully);
		}

		//public IResult UpdateImage(IFormFile formFile, CarImage carImage)
		//{

		//	//updateImage.ImagePath = _fileHelper.Update(formFile);
		//	//updateImage.Date = DateTime.Now;
		//	//_carImageDal.Update(updateImage);
		//	return new SuccessResult(Messages.CarImageUpdatedSuccesfully);
		//}

		public IDataResult<List<CarImage>> GetAll()
		{
			var result = _carImageDal.GetAll();
		
			return new SuccessDataResult<List<CarImage>>(result);
		}

		public IDataResult<List<CarImage>> GetByCarId(int carId)
		{
			var result = BusinessRules.Run(CheckIfCarHasImage(carId));

			if (result == null)
			{
				return new ErrorDataResult<List<CarImage>>(DefaultCarImage(carId).Data);
			}

			return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
		}

		public IDataResult<List<CarImage>> GetByImageId(int imageId)
		{
			return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.Id == imageId));
		}

		private IResult CheckIfImageLimitExceded(int carId)
		{
			var result = _carImageDal.GetAll(c => c.CarId == carId);
			if (result.Count <= 5) { return new SuccessResult(); }

			return new ErrorResult();

		}

		private IResult CheckIfCarHasImage(int carId)
		{
			var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
			if (result > 0)
			{
				return new SuccessResult();
			}
			return new ErrorResult();
		}

		private IDataResult<List<CarImage>> DefaultCarImage(int carId)
		{
			List<CarImage> defaultCarImages = new List<CarImage>();
			defaultCarImages.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
			return new SuccessDataResult<List<CarImage>>(defaultCarImages);
		}

		public IResult UpdateImage(IFormFile formFile, CarImage updateImage)
		{
			throw new NotImplementedException();
		}
	}
}
