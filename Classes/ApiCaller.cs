using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace Peoples.Classes
{
	public class ApiCaller
	{
		private string Baseurl;

		public ApiCaller( )
		{
			Baseurl = ConfigurationManager.AppSettings.Get( "dbApiUrl" );
		}

		// public async Task<SignInStatus> LoginCall( ApiLogin apiLogin )
		public async Task<SignInStatus> LoginCall( ApiLogin apiLogin )
		{
			SignInStatus signInStatus = new SignInStatus( );

			using ( var client = new HttpClient( ) )
			{
				//Passing service base url  
				client.BaseAddress = new Uri( Baseurl );

				client.DefaultRequestHeaders.Clear( );
				//Define request data format  
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

				//Sending request to find web api REST service resource GetAllEmployees using HttpClient  
				string json = JsonConvert.SerializeObject( apiLogin );
				var requestData = new StringContent( json, Encoding.UTF8, "application/json" );

				var responseTask = await client.PostAsync( "api/account/login", requestData );

				//Checking the response is successful or not which is sent using HttpClient  
				if ( responseTask.IsSuccessStatusCode )
				{
					//Storing the response details received from web api   
					var readTask = responseTask.Content.ReadAsStringAsync( ).Result;

					//Deserializing the response received from web api and storing into the Employee list  
					signInStatus = JsonConvert.DeserializeObject<SignInStatus>( readTask );

					return signInStatus;
				}
			}

			return signInStatus;
		}
	}

	public class ApiLogin
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}

	public class ApiLoginResult
	{
		public string Result { get; set; }

		public string Token { get; set; }
	}
}