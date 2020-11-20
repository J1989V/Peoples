using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Peoples.Controllers
{
	public class UploadController : Controller
	{
		// GET: Upload  
		public ActionResult Index( )
		{
			return UploadFile( );
		}

		[HttpGet]
		public ActionResult UploadFile( )
		{
			return View( );
		}

		[HttpPost]
		public ActionResult UploadFile( HttpPostedFileBase file )
		{
			try
			{
				List<string> retStrings = new List<string>( );

				if ( file.ContentLength > 0 )
				{
					string _FileName = Path.GetFileName( file.FileName );
					string fileExtention = Path.GetExtension( _FileName );

					if ( fileExtention.Equals( ".txt" ) )
					{
						using ( StreamReader sr = new StreamReader( file.InputStream ) )
						{
							string line;
							while ( ( line = sr.ReadLine( ) ) != null )
							{
								retStrings.Add( line );
							}

							sr.Close( );
						}
					}

					//file.SaveAs( _path );
				}

				ViewBag.Message = "File Uploaded Successfully!!";
				return View( );
			}
			catch
			{
				ViewBag.Message = "File upload failed!!";
				return View( );
			}
		}
	}
}