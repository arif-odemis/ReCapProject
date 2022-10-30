using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
	public class Result : IResult
	{
		public Result(bool success, string message) : this(success)
		{
			Message = message;
		}

		public Result(bool success)
		{
			Success = success;
		}
		public bool success { get; }

		public string message { get; }
		public bool Success { get; }
		public string Message { get; }
	}
}
