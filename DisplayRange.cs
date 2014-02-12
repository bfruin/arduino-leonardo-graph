﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoGraph
{
    public class DisplayRange
    {
        public double Start { get; set; }
        public double End { get; set; }

        public DisplayRange(double start, double end)
        {
            Start = start;
            End = end;
        }
    }
}
