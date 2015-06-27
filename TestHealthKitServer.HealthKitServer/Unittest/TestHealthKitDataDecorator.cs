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

		[SetUp()]
		public void Init()
		{
			m_healthKitAccess = new TestableHealthKitDataAccess ();
			m_decoratableObject = new HealthKitData();
			m_decoratableDistanceReading = new DistanceReading (); 
			m_decoratableObject.DistanceReadings = m_decoratableDistanceReading;
			m_decorator = new HealthKitDataDecorator (m_healthKitAccess, m_decoratableObject);
		}

		[Test()]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_SetUpPermissionIsCalledOnce()
		{
			var healthKitAccessMoc = new Mock<IHealthKitAccess> (); 
			healthKitAccessMoc.Setup (x => x.SetUpPermissions ()).Verifiable ();
			m_decorator = new HealthKitDataDecorator (healthKitAccessMoc.Object, m_decoratableObject);

			await m_decorator.DecorateHealthKitData ();

			healthKitAccessMoc.Verify(x => x.SetUpPermissions(), Times.AtLeastOnce()); 
		}
			
		[Test()]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_ReturnsTrue()
		{
			Assert.That(async () => await m_decorator.DecorateHealthKitData (), Is.True);
		}

		[Test()]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_HealthKitObjectIsDecorated()
		{
			var decoration = await m_decorator.DecorateHealthKitData ();

			Assert.AreEqual(m_decoratableObject.BloodType, "A+");
			Assert.AreEqual(m_decoratableObject.DateOfBirth, "22.04.1990");
			Assert.AreEqual(m_decoratableObject.Height, 1.74);
			Assert.AreEqual(m_decoratableObject.Sex, "Male");
			Assert.AreEqual (m_decoratableObject.LastRegisteredHeartRate, 85);
		}

		[Test()]
		public async Task DecorateHealthKitData_GivenValidIHealthKitAccess_DistanceReadingsIsDecorated()
		{
			var decoration = await m_decorator.DecorateHealthKitData ();

			var actualDistanceReadings = m_decoratableObject.DistanceReadings;

			Assert.AreEqual(actualDistanceReadings.TotalDistance, "10000");
			Assert.AreEqual(actualDistanceReadings.TotalFlightsClimed, "3000");
			Assert.AreEqual(actualDistanceReadings.RecordingStarted, "01.01.2011");
			Assert.AreEqual(actualDistanceReadings.RecordingStoped, "01.01.2012");
			Assert.AreEqual(actualDistanceReadings.TotalSteps, "50000");
		}

	}
}

