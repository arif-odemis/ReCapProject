using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public interface IFileHelper
    {
		IDataResult<String> Add (IFormFile file);
		IDataResult<String> Update (string oldfilepath, IFormFile newfile);
		IResult Delete(string filePath);
	}
}
