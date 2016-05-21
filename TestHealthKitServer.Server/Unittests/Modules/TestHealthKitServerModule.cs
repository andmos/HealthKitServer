using System;
using NUnit.Framework;
using LightInject.Nancy;
using Nancy.Testing;
using Nancy;
using Nancy.Routing;
using Nancy.ModelBinding;
using HealthKitServer.Server;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq; 
using HealthKitServer;

namespace TestHealthKitServer.Server
{
	[TestFixture]
	public class TestHealthKitServerModule
	{
		[Test]
		[Category("unit")]
		public void Ping_GivenCorrectRoute_ReturnsPong()
		{
			var bootstrapper = new TestableLightInjectBootstrapper();
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));

			var result = browser.Get("/api/v1/ping", with =>
				{
					with.HttpRequest(); 
				});

			Assert.AreEqual (HttpStatusCode.OK, result.StatusCode);
			Assert.AreEqual ("pong", result.Body.AsString());
		}

		[Test]
		[Category("unit")]
		public void GetHealthKitData_GivenValidPersonId_ReturnsCorrectRecord()
		{
			IHealthKitDataStorage cache = new HealthKitDataCache ();
			TestDataProvider.ProvideTestData (cache);
			var bootstrapper = new TestableLightInjectBootstrapper (cache);
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));
			string expectedBloodType = "A+"; 

			var result = browser.Get ("/api/v1/getHealthKitData", with => {
				with.HttpRequest ();
				with.Query ("id", "12");

			});

			var responseModels = JsonConvert.DeserializeObject<IEnumerable<HealthKitData>> (result.Body.AsString());

			Assert.IsTrue (result.StatusCode == HttpStatusCode.OK);
			Assert.AreEqual (expectedBloodType, responseModels.FirstOrDefault().BloodType);
		}

		[Test]
		[Category("unit")]
		public void GetHealthKitDataRecord_GivenValidPersonIdAndRecordId_ReturnsCorrectRecord()
		{
			IHealthKitDataStorage cache = new HealthKitDataCache ();
			TestDataProvider.ProvideTestData (cache);
			var bootstrapper = new TestableLightInjectBootstrapper (cache);
			var browser = new Browser(bootstrapper, defaults: to => to.Accept("application/json"));
			string expectedBloodType = "A+-"; 

			var result = browser.Get ("/api/v1/getHealthKitDataRecord", with => {
				with.HttpRequest ();
				with.Query("id", "11");
				with.Query("recordId", "2");

			});

			var responseModels = JsonConvert.DeserializeObject<HealthKitData> (result.Body.AsString());

			Assert.IsTrue (result.StatusCode == HttpStatusCode.OK);
			Assert.AreEqual (expectedBloodType, responseModels.BloodType);
						
		}
	
	}
}

