using System;
using NUnit.Framework;
using HealthKitServer;
using Moq;
using System.Threading.Tasks;

namespace TestHealthKitServer.HealthKitServer
{
	
	[TestFixture ()]
	public class TestHealthKitDataDecorator
	{
		private HealthKitDataDecorator m_decorator;
		private HealthKitData m_decoratableObject;
		private IHealthKitAccess m_healthKitAccess; 
		private DistanceReading m_decoratableDistanceReading; 
		private HeartRateReading m_decoratableHeartRateReading; 

		[SetUp()]
		public void Init()
		{
			m_healthKitAccess = new TestableHealthKitDataAccess ();
			m_decoratableObject = new HealthKitData();
			m_decoratableDistanceReading = new DistanceReading (); 
			m_decoratableHeartRateReading = new HeartRateReading ();
			m_decoratableObject.DistanceReadings = m_decoratableDistanceReading;
			m_decoratableObject.HeartRateReadings = m_decoratableHeartRateReading;
			m_decorator = new HealthKitDataDecorator (m_healthKitAccess, m_decoratableObject);
		}

		[Test()]
		[Category("Unit")]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_SetUpPermissionIsCalledOnce()
		{
			var healthKitAccessMoc = new Mock<IHealthKitAccess> (); 
			healthKitAccessMoc.Setup (x => x.SetUpPermissions ()).Verifiable ();
			m_decorator = new HealthKitDataDecorator (healthKitAccessMoc.Object, m_decoratableObject);

			await m_decorator.DecorateHealthKitData ();

			healthKitAccessMoc.Verify(x => x.SetUpPermissions(), Times.AtLeastOnce()); 
		}
			
		[Test()]
		[Category("Unit")]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_ReturnsTrue()
		{
			Assert.That(async () => await m_decorator.DecorateHealthKitData (), Is.True);
		}

		[Test()]
		[Category("Unit")]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_HealthKitObjectIsDecorated()
		{
			var decoration = await m_decorator.DecorateHealthKitData ();

			Assert.AreEqual("A+", m_decoratableObject.BloodType);
			Assert.AreEqual("22.04.1990", m_decoratableObject.DateOfBirth);
			Assert.AreEqual(1.74, m_decoratableObject.Height);
			Assert.AreEqual("Male", m_decoratableObject.Sex);
			Assert.AreEqual (85, m_decoratableObject.HeartRateReadings.LastRegisteredHeartRate);
		}

		[Test()]
		[Category("Unit")]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_DistanceReadingsIsDecorated()
		{
			var decoration = await m_decorator.DecorateHealthKitData ();

			var actualDistanceReadings = m_decoratableObject.DistanceReadings;

			Assert.AreEqual(10000.5, actualDistanceReadings.TotalDistance);
			Assert.AreEqual(3000, actualDistanceReadings.TotalFlightsClimed);
			Assert.AreEqual("01.01.2011", actualDistanceReadings.RecordingStarted);
			Assert.AreEqual("01.01.2012", actualDistanceReadings.RecordingStoped);
			Assert.AreEqual(50000, actualDistanceReadings.TotalSteps);
		}

	}
}

