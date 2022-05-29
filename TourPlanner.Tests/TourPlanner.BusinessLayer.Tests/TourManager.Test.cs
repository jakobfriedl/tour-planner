using System.Collections.Generic;
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
		private const double Distance_WienGraz = 193.1358; 
		private const double Time_WienGraz = 7192; 

		private readonly Tour _tour = new Tour(0, "Tour", "Description", "Wien", "Graz", TransportType.Car);
		private readonly Tour _fullTour = new Tour(10, "Tour", "Description", "Wien", "Graz", TransportType.Car, 100, 100, "path", 5, 5);
		private readonly Tour _updatedFullTour = new Tour(10, "Updated", "Description", "Wien", "Graz", TransportType.Car, 100, 100, "path", 5, 5);
		private readonly Tour _tourSameLoc = new Tour(0, "Tour", "Description", "Wien", "Wien", TransportType.Car);
		private readonly Tour _tourInvalidStart = new Tour(0, "Tour", "Description", "ThisIsNonExistant_kalsdjfkasjdklkasdf", "Wien", TransportType.Car);
		private readonly Tour _tourInvalidDestination = new Tour(0, "Tour", "Description", "Wien", "ThisIsNonExistant_kalsdjfkasjdklkasdf", TransportType.Car);
		private IEnumerable<Tour> _tours; 

		[SetUp]
        public void Setup() {
	        _tours = new List<Tour>{ _tour, _fullTour }; 

	        var mockDao = new Mock<ITourDAO>();
	        mockDao
		        .Setup(dao => dao.GetTours())
		        .Returns(new ObservableCollection<Tour> { new("Tour1"), new("Tour2") });
	        mockDao
		        .Setup(dao => dao.AddNewTour(_tour))
		        .Returns(_fullTour);
			mockDao
				.Setup(dao => dao.UpdateTour(_fullTour))
				.Returns(_updatedFullTour);
	        mockDao
		        .Setup(dao => dao.DeleteTour(10)).Returns(true);
	        mockDao
		        .Setup(dao => dao.DeleteTour(11)).Returns(false);
	        mockDao
		        .Setup(dao => dao.SearchTours("Wien")).Returns(_tours); 


	        _manager = new TourManager(mockDao.Object);
		}

        [Test]
        public async Task TestGetInformation_ReturnsValidDistanceTime() {
	        var result = await _manager.GetInformation(_tour); 

			Assert.NotNull(_tour.Distance); 
			Assert.AreEqual(Distance_WienGraz, result.Distance); 
			Assert.NotNull(_tour.EstimatedTime); 
			Assert.AreEqual(Time_WienGraz,result.EstimatedTime); 
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
        public async Task TestGetInformation_ThrowsInvalidLocationException_InvalidStart() {
	        try {
		        var result = await _manager.GetInformation(_tourInvalidStart);
		        Assert.Fail();
	        } catch (InvalidLocationException) {
		        Assert.Pass();
	        }
		}

        [Test]
        public async Task TestGetInformation_ThrowsInvalidLocationException_InvalidDestination()
        {
	        try {
		        var result = await _manager.GetInformation(_tourInvalidDestination);
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

        [Test]
        public void TestDeleteTour_ReturnsTrue() {
	        var result = _manager.DeleteTour(10); 
			Assert.True(result);
        }

        [Test]
        public void TestDeleteTour_ReturnsFalse() {
	        var result = _manager.DeleteTour(11);
			Assert.False(result);
        }

        [Test]
        public void TestSearchTours_ReturnsListOfTours() {
	        var result = _manager.SearchTours("Wien"); 
			Assert.AreEqual(_tours, result);
        }
	}
}