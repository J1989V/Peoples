using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Peoples.Classes;

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
			SignInStatus signInStatus = SignInStatus.Failure;
			if ( TempData[ "signInStatus" ] != null )
				signInStatus = ( SignInStatus )TempData[ "signInStatus" ];
			
			return View( signInStatus );
		}

		[HttpPost]
		public ActionResult UploadFile( HttpPostedFileBase file )
		{
			try
			{
				if ( file.ContentLength > 0 )
				{
					ApiCaller apiCaller = new ApiCaller( );
					var dataSourceData = apiCaller.ProcessFile( file );

					TempData[ "dataSourceData" ] = dataSourceData;

					return RedirectToAction( "Index", "Popi" );
				}
			}
			catch
			{
				ViewBag.Message = "File upload failed!!";
			}

			return View( );
		}
	}
}