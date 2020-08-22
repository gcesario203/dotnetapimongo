using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace DotnetApi.Data.Collections
{
    public class Infected
    {
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public GeoJson2DGeographicCoordinates Localization { get; set; }

        public Infected(DateTime pBirthDate, string pGender, double latitude, double longitude)
        {
            this.BirthDate = pBirthDate;
            this.Gender = pGender;
            this.Localization = new GeoJson2DGeographicCoordinates(longitude,latitude);
        }
    }
}