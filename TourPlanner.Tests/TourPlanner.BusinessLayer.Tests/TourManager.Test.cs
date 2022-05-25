using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.BusinessLayer.Exceptions;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlannerBL.Tests
{
	public class TestTourManager {

		private ITourManager _manager;
		private readonly Tour _tour = new Tour(0, "Tour", "Description", "Wien", "Graz", TransportType.Car);
		private readonly Tour _fullTour = new Tour(10, "Tour", "Description", "Wien", "Graz", TransportType.Car, 100, 100, "path", 5, 5);
		private readonly Tour _tourSameLoc = new Tour(0, "Tour", "Description", "Wien", "Wien", TransportType.Car);
		private readonly Tour _tourInvalidLoc = new Tour(0, "Tour", "Description", "Wien", "ThisIsNonExistant_kalsdjfkasjdklkasdf", TransportType.Car);

		[SetUp]
        public void Setup() {
	        var mockDao = new Mock<ITourDAO>();
	        mockDao
		        .Setup(dao => dao.GetTours())
		        .Returns(new ObservableCollection<Tour> { new("Tour1"), new("Tour2") });
	        mockDao
		        .Setup(dao => dao.AddNewTour(_tour))
		        .Returns(_fullTour); 

	        _manager = new TourManager(mockDao.Object);
		}

        [Test]
        public async Task TestGetInformation_ReturnsValidDistanceTime() {
	        var result = await _manager.GetInformation(_tour); 

			Assert.NotNull(_tour.Distance); 
			Assert.AreEqual(result.Distance, 193.1358); 
			Assert.NotNull(_tour.EstimatedTime); 
			Assert.AreEqual(result.EstimatedTime, 7192); 
        }

        [Test]
        public async Task TestGetInformation_ThrowsInvalidLocationException_SameLocation() {
	        try {
		        var result = await _manager.GetInformation(_tourSameLoc);
				Assert.Fail();
	        } catch (InvalidLocationException) {
				Assert.Pass();
	        }
        }

        [Test]
        public async Task TestGetInformation_ThrowsInvalidLocationException_InvalidLocation() {
	        try {
		        var result = await _manager.GetInformation(_tourInvalidLoc);
		        Assert.Fail();
	        } catch (InvalidLocationException) {
		        Assert.Pass();
	        }
		}
        
        [Test]
        public async Task TestSaveInformation_ReturnsValidTourObject() {
	        var result = await _manager.SaveInformation(_tour); 
			Assert.AreEqual(_fullTour, result);
        }

        [Test]
        public void TestGetTours_ReturnsListOfTours() {
	        var result = _manager.GetTours();
	        Assert.IsNotEmpty(result);
			Assert.AreEqual(2, result.Count());
        }

	}
}