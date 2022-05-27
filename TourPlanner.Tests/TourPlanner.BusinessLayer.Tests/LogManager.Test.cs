using System;
using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using NUnit.Framework;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlannerBL.Tests
{
    public class LogManager_Test {
	    private ILogManager _manager;
	    private readonly Log _log = new Log(0, DateTime.Now, DateTime.Now, 100, "Start", "Destination", "Comment", 5, 5); 
	    private readonly Log _log2 = new Log(9, 0, DateTime.Now, DateTime.Now, 300, "Start", "Destination", "Comment2", 1, 10); 
	    private readonly Log _fullLog = new Log(10, 0, DateTime.Now, DateTime.Now, 100, "Start", "Destination", "Comment", 5, 5); 
	    private readonly Log _updatedFullLog = new Log(10, 0, DateTime.Now, DateTime.Now, 100, "Start", "Destination", "Updated", 6, 6); 

	    [SetUp]
	    public void Setup() {
		    var mockDao = new Mock<ILogDAO>();
		    mockDao
			    .Setup(dao => dao.GetLogsByTourId(0))
			    .Returns(new ObservableCollection<Log> { _log, _log2 });
		    mockDao
			    .Setup(dao => dao.AddNewLog(_log))
			    .Returns(_fullLog);
		    mockDao
			    .Setup(dao => dao.UpdateLog(_fullLog))
			    .Returns(_updatedFullLog);
		    mockDao
			    .Setup(dao => dao.DeleteLog(10))
			    .Returns(true); 
			mockDao
				.Setup(dao => dao.DeleteLog(11))
				.Returns(false);

		    _manager = new LogManager(mockDao.Object); 
	    }

	    [Test]
	    public void TestCreateLog_ReturnsFullLogObject() {
		    var result = _manager.CreateLog(_log);
		    Assert.AreEqual(_fullLog, result);
	    }

	    [Test]
	    public void TestGetLogs_ReturnsLogList() {
		    var result = _manager.GetLogs(0);
		    Assert.AreEqual(2, result.Count());
	    }

	    [Test]
	    public void TestUpdateLog_ReturnsUpdatedLogObject() {
		    var result = _manager.UpdateLog(_fullLog); 
			Assert.AreEqual(_updatedFullLog, result);
	    }

	    [Test]
	    public void TestDeleteLog_ReturnsTrue() {
		    var result = _manager.DeleteLog(10); 
			Assert.True(result);
	    }

	    [Test]
	    public void TestDeleteLog_ReturnsFalse() {
		    var result = _manager.DeleteLog(11); 
			Assert.False(result);
	    }
    }
}
