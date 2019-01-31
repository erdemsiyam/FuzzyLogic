using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicExample.Classes
{
    /*
        This Class is :
            Only one outputgraphic can be, and it is result graphic to help find the example result.
         */
    public class OutputGraphic : Graphic
    {
        // ---- OBJECT Side : its inherit from Graphic Class. so only there is constructer we need.
        public OutputGraphic(string name, List<Function> ownedFunction) : base(name, ownedFunction) { }


        // ---- STATIC Side :
        public static OutputGraphic outputGraphic // The One OutputGraphic Oject.
        {
            /*
                We created the output graphic for our example.
                 */
            get {
                return new OutputGraphic("ResistanceOutput", // Graphic constructor wants from us = a name and a Function List
                            new List<Function>()
                            {
                                new Function(new SortedDictionary<double, double>() { { 0, 1 }, { 0.5, 1 }, { 1, 0 } }, Degree.VERY_LOW),// Function constructor wants from us  = sorted end X points with Y values, and Degree
                                new Function(new SortedDictionary<double, double>() { { 0.5, 0 }, { 1.25, 1 }, { 2, 0 } }, Degree.LOW),
                                new Function(new SortedDictionary<double, double>() { { 1.5, 0 }, { 2.5, 1 }, { 3.5, 0 } }, Degree.MEDIUM),
                                new Function(new SortedDictionary<double, double>() { { 3, 0 }, { 3.75, 1 }, { 4.5, 0 } }, Degree.HIGH),
                                new Function(new SortedDictionary<double, double>() { { 4, 0 }, { 4.5, 1 }, { 5, 1 } }, Degree.VERY_HIGH)
                            });
            }
        } // Static Object

        public static Dictionary<KeyValuePair<Degree, Degree>, Degree> table // this is state scheme, the result is shaped accordingly.
        {
            /*
                We created the output scheme for our example.
                 */
            get
            {
                Dictionary<KeyValuePair<Degree, Degree>, Degree> tableTemp = new Dictionary<KeyValuePair<Degree, Degree>, Degree>(); // dictionary value = output degree

                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_LOW, Degree.VERY_LOW), Degree.MEDIUM);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_LOW, Degree.LOW), Degree.HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_LOW, Degree.MEDIUM), Degree.VERY_HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_LOW, Degree.HIGH), Degree.VERY_HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_LOW, Degree.VERY_HIGH), Degree.VERY_HIGH);

                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.LOW, Degree.VERY_LOW), Degree.LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.LOW, Degree.LOW), Degree.MEDIUM);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.LOW, Degree.MEDIUM), Degree.HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.LOW, Degree.HIGH), Degree.HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.LOW, Degree.VERY_HIGH), Degree.HIGH);

                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.MEDIUM, Degree.VERY_LOW), Degree.VERY_LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.MEDIUM, Degree.LOW), Degree.VERY_LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.MEDIUM, Degree.MEDIUM), Degree.MEDIUM);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.MEDIUM, Degree.HIGH), Degree.HIGH);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.MEDIUM, Degree.VERY_HIGH), Degree.HIGH);

                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.HIGH, Degree.VERY_LOW), Degree.NONE); // there is no output resistance. output = 0
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.HIGH, Degree.LOW), Degree.VERY_LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.HIGH, Degree.MEDIUM), Degree.VERY_LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.HIGH, Degree.HIGH), Degree.LOW);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.HIGH, Degree.VERY_HIGH), Degree.MEDIUM);

                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_HIGH, Degree.VERY_LOW), Degree.NONE);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_HIGH, Degree.LOW), Degree.NONE);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_HIGH, Degree.MEDIUM), Degree.NONE);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_HIGH, Degree.HIGH), Degree.NONE);
                tableTemp.Add(new KeyValuePair<Degree, Degree>(Degree.VERY_HIGH, Degree.VERY_HIGH), Degree.NONE);

                return tableTemp;
            }
        } // Output Table

        public static Degree getResultByInput(Function f1, Function f2) // inputs : levels to compare at scheme. return is the result.
        {
            return table[new KeyValuePair<Degree, Degree>(f1.name, f2.name)];
        }


    }
}
