using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Models
{
    public struct Dimensions
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public Dimensions(double width, double height, double length)
        {
            Width = width;
            Height = height;
            Length = length;
        }
    }
}
