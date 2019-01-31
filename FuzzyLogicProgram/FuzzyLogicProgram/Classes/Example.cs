using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicExample.Classes
{
    /*
        This Class is :
            Represent any example.
         */
    public class Example
    {
        // ------- OBJECT Side : this params and constructor for the example.

            private double[] exampleValues = new double[2];
            public Example(params double[] parameters)
            {
                if (parameters.Length != 2)
                {
                    throw new Exception("Input Values Must Be 2.");
                }
                else
                {
                    exampleValues = parameters;
                }
            }


        // ------ STATIC Side : all these functions serve solving the problem.

            // main function.
            public static double fuzzyLogic(Example example)
            {
                return defuzzification(inference(fuzzification(example.exampleValues)));
            }

            // step 1 : fuzzification (bulanıklaştırma)
            private static List<Tuple<Graphic, Dictionary<Function, double>>> fuzzification(double[] exampleValues)
            {
                List<Tuple<Graphic, Dictionary<Function, double>>> fuzzificationResults = new List<Tuple<Graphic, Dictionary<Function, double>>>();

                int valueCounter = 0;
                foreach (Graphic item in Graphic.graphics)
                {
                    Dictionary<Function, double> forNextGraphic = new Dictionary<Function, double>();
                    foreach (Function level in item.ownedFunction)
                    {
                        if (level.getMinXValue() <= exampleValues[valueCounter] && level.getMaxXValue() >= exampleValues[valueCounter])
                        {
                            double membershipDegree = level.getMembershipDegreeByFunction(exampleValues[valueCounter]);
                            forNextGraphic.Add(level, membershipDegree);
                        }
                    }
                    fuzzificationResults.Add(new Tuple<Graphic, Dictionary<Function, double>>(item,forNextGraphic));
                    valueCounter++;
                }
                return fuzzificationResults;
            }
            // step 2 : inference (bulanık çıkarım)
            private static Dictionary<Degree, double> inference(List<Tuple<Graphic, Dictionary<Function, double>>> fuzzificationResults)
            {
                Dictionary<Degree, double>  inferenceResults = new Dictionary<Degree, double>(); // NEW : Graph out Degree'si

                Tuple<Graphic, Dictionary<Function, double>> graphic1 = fuzzificationResults[0];
                Tuple<Graphic, Dictionary<Function, double>> graphic2 = fuzzificationResults[1];

                foreach (Function item in graphic1.Item2.Keys)
                {
                    foreach (Function item2 in graphic2.Item2.Keys)
                    {
                        Degree degree = OutputGraphic.getResultByInput(item, item2);
                        double newValue = Math.Min(graphic1.Item2[item], graphic2.Item2[item2]);
                        if (inferenceResults.ContainsKey(degree))
                        {
                            double oldValue = inferenceResults[degree];
                            if (newValue > oldValue)
                            {
                                inferenceResults[degree] = newValue;
                            }
                        }
                        else
                        {
                            inferenceResults.Add(degree, newValue);
                        }
                    }
                }
                return inferenceResults;
            }
            // step 3 : defuzzification (durulaştırma)
            private static double defuzzification(Dictionary<Degree, double> inferenceResults)
            {
                List<Tuple<double, List<double>>> defuzzificationResults = new List<Tuple<double, List<double>>>(); // rezis değeri , xDeğerleri

                foreach (KeyValuePair<Degree, double> item in inferenceResults)
                {
                    List<double> nextResult = new List<double>();
                    Degree next = item.Key;
                    double kesimNoktaDegeri = -1;
                    int kesimNoktaSayisi = -1;
                    KeyValuePair<double, double> oldItem2 = new KeyValuePair<double, double>();
                    foreach (KeyValuePair<double, double> item2 in OutputGraphic.outputGraphic.ownedFunction.SingleOrDefault(x=>x.name == next).functionEndPoints)
                    {
                        if (item2.Value != kesimNoktaDegeri)
                        {
                            kesimNoktaSayisi++;
                            kesimNoktaDegeri = item2.Value;
                            if (kesimNoktaSayisi > 0)
                            {
                                double xValue = 0;
                                if (oldItem2.Value > item2.Value) // soldan sağa iniyor
                                {
                                    xValue = item2.Key - (item.Value * (item2.Key - oldItem2.Key));
                                    nextResult.Add(xValue);
                                }
                                else if (oldItem2.Value < item2.Value) // soldan sağa çıkıyor
                                {
                                    xValue = (item.Value * (item2.Key - oldItem2.Key)) + oldItem2.Key;
                                    nextResult.Add(xValue);
                                }
                            }
                        }
                        oldItem2 = item2;
                    }
                    defuzzificationResults.Add(new Tuple<double, List<double>>(item.Value, nextResult));
                }

                //calculate the output k ohm
                double total = 0;
                double div = 0;
                foreach (Tuple<double, List<double>> item in defuzzificationResults)
                {
                    double temp = 0;
                    double forDivCounter = 0;
                    foreach (double item2 in item.Item2)
                    {
                        forDivCounter++;
                        temp += item2;
                    }
                    temp *= item.Item1;
                    total += temp;
                    div += (forDivCounter * item.Item1);
                }
                return total / div;
            }

    }
}
