using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalYearProjectApp.AppServices
{
    class MathService
    {
        public double minLong { get; set; }
        public double maxLong { get; set; }
        public double minLat { get; set; }
        public double maxLat { get; set; }

        public MathService()
        {
        }

        public double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public double ConvertRadianToDegree(double radian)
        {
            double degrees = radian * (180.0 / Math.PI);
            return degrees;
        }
        public void setBoundingCircle(double currentLatitude, double currentLongitude)
        {
            double searchDistanceInKM = 1;
            //radius of earth
            double radiusOfTheEarthInKm = 6371;
            double currentLatitudeRad = currentLatitude;
            double currentLongitudeRad = currentLongitude;
            maxLat = currentLatitudeRad + ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            minLat = currentLatitudeRad - ConvertRadianToDegree(searchDistanceInKM / radiusOfTheEarthInKm);
            maxLong = currentLongitudeRad + ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(ConvertDegreesToRadians(currentLatitude));
            minLong = currentLongitudeRad - ConvertRadianToDegree(System.Math.Asin(searchDistanceInKM / radiusOfTheEarthInKm)) / System.Math.Cos(ConvertDegreesToRadians(currentLatitude));
        }
    }
}