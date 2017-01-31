using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalYearProjectClassLibrary.Services
{
    class MathService
    {
        public MathService()
        {

        }

        public double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}