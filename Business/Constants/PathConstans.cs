using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
	public  class PathConstans
	{
		public static string GetCarImagesRouter()
		{
			string ImagesPath = "wwwroot\\Uploads\\Images\\";

			return ImagesPath;
		}

		public static string GetDefaultImagesRouter()
		{
			return GetCarImagesRouter() + @"\DefaultImage.jpg";
		}
	
}
}
