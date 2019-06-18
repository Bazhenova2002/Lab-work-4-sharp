using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_rabota_4
{
    class Square
    {

        private int side;
        public int Side { get => side; set => side = value == 0 ? 1 : value; }

        public Square() { }
        public Square(int side)
        {
            Side = side;
        }



        public double Diagonal() => Math.Sqrt(2 * Side * Side);


        public int Perimeter() => 4 * Side;


        public int Area() => Side * Side;

        public int Area(int side) => side * side;


        public override string ToString()
        {
            return "\nSide: " + Side + "\nArea: " + Area() + "\nPerimeter: " + Perimeter() + "\nDiagonal: " + Diagonal();
        }
    }
}
