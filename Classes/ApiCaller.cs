using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Peoples.Dtos;

namespace Peoples.Classes
{
	public class ApiCaller
	{
		private string Baseurl;

		public ApiCaller( )
		{
			Baseurl = ConfigurationManager.AppSettings.Get( "dbApiUrl" );
		}

		public async Task<ApiLoginResult> LoginCall( ApiLogin apiLogin )
		{
			ApiLoginResult apiLoginResult = new ApiLoginResult( );

			using ( var client = new HttpClient( ) )
			{
				//Passing service base url  
				client.BaseAddress = new Uri( Baseurl );

				client.DefaultRequestHeaders.Clear( );
				//Define request data format  
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

				string json = JsonConvert.SerializeObject( apiLogin );
				var requestData = new StringContent( json, Encoding.UTF8, "application/json" );

				var responseTask = await client.PostAsync( "account/login", requestData );

				//Checking the response is successful or not which is sent using HttpClient  
				if ( responseTask.IsSuccessStatusCode )
				{
					//Storing the response details received from web api   
					var readTask = responseTask.Content.ReadAsStringAsync( ).Result;

					//Deserializing the response received from web api and storing into the Employee list  
					apiLoginResult = JsonConvert.DeserializeObject<ApiLoginResult>( readTask );

					return apiLoginResult;
				}
			}

			return apiLoginResult;
		}

		public DataSourceData ProcessFile( HttpPostedFileBase file )
		{
			DataSourceData dataSourceData = new DataSourceData( );

			byte[ ] fileData;
			using ( var reader = new BinaryReader( file.InputStream ) )
			{
				fileData = reader.ReadBytes( file.ContentLength );
			}

			HttpContent fileContent = new ByteArrayContent( fileData );

			using ( var client = new HttpClient( ) )
			{
				//Passing service base url  
				client.BaseAddress = new Uri( Baseurl );

				using ( var formData = new MultipartFormDataContent( ) )
				{
					formData.Add( fileContent, "file", file.FileName );

					var responseTask = client.PostAsync( "api/upload/uploadfile", formData ).Result;

					//Checking the response is successful or not which is sent using HttpClient  
					if ( responseTask.IsSuccessStatusCode )
					{
						//Storing the response details received from web api   
						var readTask = responseTask.Content.ReadAsStringAsync( ).Result;

						//Deserializing the response received from web api and storing into the Employee list  
						dataSourceData = JsonConvert.DeserializeObject<DataSourceData>( readTask );

						return dataSourceData;
					}
				}
			}

			return dataSourceData;
		}

		public async Task<CallResult> Save( List<DataStoreDto> dataStoreDtos )
		{
			CallResult callResult = new CallResult( );

			using ( var client = new HttpClient( ) )
			{
				//Passing service base url  
				client.BaseAddress = new Uri( Baseurl );

				client.DefaultRequestHeaders.Clear( );
				//Define request data format  
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

				string json = JsonConvert.SerializeObject( dataStoreDtos );
				var requestData = new StringContent( json, Encoding.UTF8, "application/json" );

				var responseTask = await client.PostAsync( "api/datastore/InsertPopiMetadata", requestData );

				//Checking the response is successful or not which is sent using HttpClient  
				if ( responseTask.IsSuccessStatusCode )
				{
					//Storing the response details received from web api   
					var readTask = responseTask.Content.ReadAsStringAsync( ).Result;

					//Deserializing the response received from web api and storing into the Employee list  
					callResult = JsonConvert.DeserializeObject<CallResult>( readTask );

					return callResult;
				}
			}

			return callResult;
		}

		public List<DataStoreDto> GetPopiMetada( )
		{
			List<DataStoreDto> dataStoreDtos = new List<DataStoreDto>( );
			using ( var client = new HttpClient( ) )
			{
				//Passing service base url  
				client.BaseAddress = new Uri( Baseurl );

				var responseTask = client.GetAsync( "api/datastore/GetPopiMetadata" ).Result;

				if ( responseTask.IsSuccessStatusCode )
				{
					//Storing the response details received from web api   
					var readTask = responseTask.Content.ReadAsStringAsync( ).Result;

					//Deserializing the response received from web api and storing into the Employee list  
					dataStoreDtos = JsonConvert.DeserializeObject<List<DataStoreDto>>( readTask );

					return dataStoreDtos;
				}
			}

			return dataStoreDtos;
		}
	}

	public class ApiLogin
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}

	public class ApiLoginResult
	{
		public IPrincipal User { get; set; }

		public SignInStatus SignInStatus { get; set; }
	}

	public class CallResult
	{
		public string Result { get; set; }
	}
}