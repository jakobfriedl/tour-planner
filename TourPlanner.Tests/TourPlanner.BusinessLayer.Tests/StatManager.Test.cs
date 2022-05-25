using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;

namespace TourPlannerBL.Tests {
	public class StatManager_Test {
		private IStatManager _manager;
		private const int Id = 1;
		private const int Count = 10;
		private const double AvgRating = 6.8f;
		private const double AvgDifficulty = 3.45f; 

		[SetUp]
		public void Setup() {
			var mockDao = new Mock<IStatDAO>(); 
			mockDao
				.Setup(dao => dao.GetLogCount(Id)).Returns(Count);
			mockDao
				.Setup(dao => dao.GetAvgRating(Id)).Returns(AvgRating);
			mockDao
				.Setup(dao => dao.GetAvgDifficulty(Id)).Returns(AvgDifficulty); 

			_manager = new StatManager(mockDao.Object);
		}

		[Test]
		public void TestGetLogCount_Returns10() {
			var count = _manager.GetLogCount(Id);
			Assert.AreEqual(Count, count);
		}

		[Test]
		public void TestGetAvgRating_Returns6_8() {
			var avgRating = _manager.GetAvgRating(Id);
			Assert.AreEqual(AvgRating, avgRating);
		}

		[Test]
		public void TestGetAvgDifficulty_Returns3_45() {
			var avgDifficulty = _manager.GetAvgDifficulty(Id);
			Assert.AreEqual(AvgDifficulty, avgDifficulty);
		}

		[Test]
		public void TestGetPopularity_ReturnsCountTimesRating() {
			var expected = Count * AvgRating; 
			var popularity = _manager.GetPopularity(Id);
			Assert.AreEqual(expected, popularity);
		}

		[Test]
		public void TestGetChildFriendliness_Returns10MinusDifficulty() {
			var expected = Count <= 0 ? 0 : 10 - AvgDifficulty;
			var childFriendliness = _manager.GetChildFriendliness(Id);
			Assert.AreEqual(expected, childFriendliness);
		}

		[Test]
		public void TestGetChildFriendliness_Returns0() {
			var mockDao = new Mock<IStatDAO>();
			mockDao
				.Setup(dao => dao.GetLogCount(Id)).Returns(0);
			mockDao
				.Setup(dao => dao.GetAvgDifficulty(Id)).Returns(AvgDifficulty);
			_manager = new StatManager(mockDao.Object);

			var expected = 0;
			var childFriendliness = _manager.GetChildFriendliness(Id);
			Assert.AreEqual(expected, childFriendliness);
		}
	}
}
