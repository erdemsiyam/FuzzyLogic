using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicExample.Classes
{
    /*
        This Class is :
            graphics required by the example system.
         */
    public class Graphic
    {
        // ----- OBJECT Side
        public string name; // graph name.
        public List<Function> ownedFunction = new List<Function>(); // graph functions. (1 graph have many functions. 1 functions have many end points(piecewise))
        public Graphic(string name,List<Function> ownedFunction)
        {
            this.name = name;
            this.ownedFunction = ownedFunction;
        }

        // ----- STATIC Side
        public static List<Graphic> graphics // all graphs storing here.
        {
            /*
                EXAMPLE :
                    There is 2 graphs specified by me for the Water Heating System example.
                 */
            get
            {
                List<Graphic> tempGraphics = new List<Graphic>();
                tempGraphics.Add(
                            new Graphic("TemperatureLevel", new List<Function>() { // Graphic constructor wants from us = a name and a Function List
                                        new Function(new SortedDictionary<double, double>() { { 0, 1 }, { 10, 1 }, { 20, 0 } }, Degree.VERY_LOW), // Function constructor wants from us  = sorted end X points with Y values, and Degree
                                        new Function(new SortedDictionary<double, double>() { { 15, 0 }, { 27.5, 1 }, { 40, 0 } }, Degree.LOW),
                                        new Function(new SortedDictionary<double, double>() { { 35, 0 }, { 47.5, 1 }, { 60, 0 } }, Degree.MEDIUM),
                                        new Function(new SortedDictionary<double, double>() { { 55, 0 }, { 67.5, 1 }, { 80, 0 } }, Degree.HIGH),
                                        new Function(new SortedDictionary<double, double>() { { 75, 0 }, { 87.5, 1 }, { 100, 1 } }, Degree.VERY_HIGH)
                            })
                    );
                tempGraphics.Add(
                            new Graphic("WaterLevel", new List<Function>() {
                                        new Function(new SortedDictionary<double, double>() { { 0, 1 }, { 0.5, 1 }, { 1, 0 } },  Degree.VERY_LOW),
                                        new Function(new SortedDictionary<double, double>() { { 0.5, 0 }, { 1.25, 1 }, { 2, 0 } }, Degree.LOW),
                                        new Function(new SortedDictionary<double, double>() { { 1.5, 0 }, { 2.5, 1 }, { 3.5, 0 } }, Degree.MEDIUM),
                                        new Function(new SortedDictionary<double, double>() { { 3, 0 }, { 3.75, 1 }, { 4.5, 0 } }, Degree.HIGH),
                                        new Function(new SortedDictionary<double, double>() { { 4, 0 }, { 4.5, 1 }, { 5, 1 } }, Degree.VERY_HIGH)
                            })
                    );
                return tempGraphics;
            }
        }

    }
}
