using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicExample.Classes
{
    /*
     This class is :
        The class representing a piecevise math function.
        its have own end points(every point would be diffrent function).
        1 function have many endpoints(piecewise func.)
     */
    public class Function 
    {

        public SortedDictionary<double,double> functionEndPoints; // end points of piecewies function [ 1.double : function point x parameter, 2.double : function y parameter.]

        public Degree name; //which degree is represent.

        public Function(SortedDictionary<double, double> functionEndPoints, Degree name)
        {
            this.functionEndPoints = functionEndPoints;
            this.name = name;
        }

        public double getMinXValue()
        {
            return functionEndPoints.Keys.Min();
        } // get the lowest value of the func
        public double getMaxXValue()
        {
            return functionEndPoints.Keys.Max();
        } // get the highest value of the func

        public double getMembershipDegreeByFunction(double input)
        {
            KeyValuePair<double, double> smallThenInput, bigThanInput;
            smallThenInput = bigThanInput = new KeyValuePair<double, double>(1,1);

            foreach (KeyValuePair<double,double> item in functionEndPoints)
            {
                if (item.Key == input)
                {
                    return item.Value;
                }
                else if (item.Key < input)
                {
                    smallThenInput = item;
                    continue;
                }
                else if (item.Key > input)
                {
                    bigThanInput = item;

                    //there is calculate the degree of membership at this level
                    if (smallThenInput.Value == bigThanInput.Value)
                    {
                        return smallThenInput.Value; // or bigThanInput.Value
                    }
                    else if (smallThenInput.Value < bigThanInput.Value)
                    {
                        return (input - smallThenInput.Key) / (bigThanInput.Key - smallThenInput.Key);
                    }
                    else if (bigThanInput.Value < smallThenInput.Value)
                    {
                        return (bigThanInput.Key - input) / (bigThanInput.Key - smallThenInput.Key);
                    }

                }
            }
            return 0;
        } // calculating membership degree to the func.

        public override string ToString()
        {
            switch (name)
            {
                case Degree.VERY_LOW: return "VERY_LOW";
                case Degree.LOW: return "LOW";
                case Degree.MEDIUM: return "MEDIUM";
                case Degree.HIGH: return "HIGH";
                case Degree.VERY_HIGH: return "VERY_HIGH";
                case Degree.NONE: return "NONE";
            }
            return "<NON>";
        }
    }
}
