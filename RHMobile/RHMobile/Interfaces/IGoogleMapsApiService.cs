using System;
using System.Threading.Tasks;
using XForms.Models;

namespace XForms.Interfaces
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}
