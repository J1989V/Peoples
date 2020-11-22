using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Peoples.Classes;
using Peoples.Dtos;
using Peoples.Enums;
using Peoples.Models;

namespace Peoples.Controllers
{
	public class PopiController : Controller
	{
		// GET
		public ActionResult Index( )
		{
			var dataSourceData = ( DataSourceData )TempData[ "dataSourceData" ];

			return View( dataSourceData );
		}

		public ActionResult Save( DataSourceData dataSourceData )
		{
			List<DataStoreDto> dataStoreDtos = new List<DataStoreDto>( );

			foreach ( var fieldRow in dataSourceData.FieldRows )
			{
				foreach ( var field in fieldRow.Fields )
				{
					if ( field.Category != Categories.Unknown )
					{
						dataStoreDtos.Add( new DataStoreDto
						{
							DataStoreName = dataSourceData.DataStoreName,
							DataStoreType = dataSourceData.DataStoreType,
							DataStoreLocation = dataSourceData.DataStoreLocation,
							TableTabName = dataSourceData.DataStoreTabName,
							FieldName = field.Column,
							FieldRow = field.Row,
							Category = field.Category
						} );
					}
				}
			}

			ApiCaller apiCaller = new ApiCaller( );
			var result = apiCaller.Save( dataStoreDtos );

			return RedirectToAction( "Index", "Home" );
		}

		public ActionResult PopiMetadatas( )
		{
			List<DataStoreDto> dataStoreDto = new List<DataStoreDto>( );

			if ( Session[ "signInStatus" ] != null )
			{
				SignInStatus signInStatus = ( SignInStatus )Session[ "signInStatus" ];
				if ( signInStatus == SignInStatus.Success )
				{
					ApiCaller apiCaller = new ApiCaller( );
					dataStoreDto = apiCaller.GetPopiMetada( );
				}
			}

			return View( dataStoreDto );
		}
	}
}