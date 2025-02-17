﻿using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
	public class FileHelperManager : IFileHelper
	{

		static string path = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\";

		public IDataResult<String> Add(IFormFile file)
		{
			if (file.Length > 0)
			{
				
				var newGuidPath = Guid.NewGuid().ToString() + Path.GetExtension(path + file.FileName);
				using (FileStream fileStream = System.IO.File.Create(path + newGuidPath))
				{
					file.CopyTo(fileStream);
					fileStream.Flush();
				}
				String filePath = path + newGuidPath;
				return new SuccessDataResult<String>(filePath, "File Added");
			}
			return new ErrorDataResult<String>();

		}
		public IDataResult<String> Update(string oldfilepath, IFormFile newfile)
		{
			if (File.Exists(oldfilepath))
			{
				var newGuidPath = Guid.NewGuid().ToString() + Path.GetExtension(path + newfile.FileName);
				using (FileStream fileStream = System.IO.File.Create(path + newGuidPath))
				{
					newfile.CopyTo(fileStream);
					fileStream.Flush();
				}
				string newfilePath = path + newGuidPath;
				return new SuccessDataResult<String>(newfilePath, "File Added");
			}
			return new ErrorDataResult<String>("File Doesn't Exists");

		}
		public IResult Delete(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				return new SuccessResult();
			}
			return new ErrorResult();
		}
	}
}