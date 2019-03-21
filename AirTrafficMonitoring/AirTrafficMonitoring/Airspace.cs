using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace AirTrafficMonitoring
{
    public class Airspace
    {



       

        public double Length { get; private set; }
        public double Depth { get; private set; }
        public double MinHeight { get; private set; }
        public double MaxHeight { get; private set; }



        public Airspace(double x, double y, double min_z, double max_z)
        {
            /*
            MinHeight = min_z;
            MaxHeight = max_z;
            Depth = x;
            Length = y;
            */
        }

        public bool InArea(Flight flight)
        {
           


            return true;
        }


    }


}