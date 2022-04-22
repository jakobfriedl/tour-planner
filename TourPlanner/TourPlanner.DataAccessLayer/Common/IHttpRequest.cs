using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.Common; 

/// <summary>
/// Interface for Http Request, to get Tour Information from MapQuest API
/// </summary>
public interface IHttpRequest {
	public Task<Tour> GetTourInformation(Tour tour); 
	public Task<byte[]> GetTourImageBytes(Tour tour); 
}