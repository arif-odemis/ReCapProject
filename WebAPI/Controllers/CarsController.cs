﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase
	{
		ICarService _carService;

		public CarsController(ICarService carService)
		{
			_carService = carService;
		}

		[HttpPost("add")]
		public IActionResult Add(Car car)
		{
			var result = _carService.Add(car);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("delete")]
		public IActionResult Delete(Car car)
		{
			var result = _carService.Delete(car);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getall")]
		public IActionResult GetAll()
		{
			var result = _carService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		[HttpGet("getbyid")]
		public IActionResult GetById(int id)
		{
			var result = _carService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcardetails")]
		public IActionResult GetCarDetails()
		{
			var result = _carService.GetCarDetails();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcardetailbyid")]
		public IActionResult GetCarDetailById(int id)
		{
			var result = _carService.GetCarDetailById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcardetailsbybrandid")]
		public IActionResult GetCarDetailsByBrandId(int brandId)
		{
			var result = _carService.GetCarDetailsByBrandId(brandId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcardetailsbycolorid")]
		public IActionResult GetCarDetailsByColorId(int colorId)
		{
			var result = _carService.GetCarDetailsByColorId(colorId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcarsbybrandid")]
		public IActionResult GetCarsByBrandId(int brandid)
		{
			var result = _carService.GetCarsByBrandId(brandid);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("getcarsbycolorid")]
		public IActionResult GetCarsByColorId(int colorid)
		{
			var result = _carService.GetCarsByColorId(colorid);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("update")]
		public IActionResult Update(Car car)
		{
			var result = _carService.Update(car);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
