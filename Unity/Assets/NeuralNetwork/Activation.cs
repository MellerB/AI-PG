using UnityEngine;
using System;

namespace NeuralNetwork
{
    public static class ActivationFunc
    {
        public static double Tanh(double x)
        {
            return 1-(2.0)/(Math.Exp(2*x)+1);
        }

        public static double Linear(double x)
        {
            return x;
        }

        public static double BinaryStep(double x)
        {
            if(x > 0)
            {
                return 1;
            }
            if(x == 0)
            {
                return 0;
            }
            if(x < 0)
            {
                return -1;
            }
            return 0;
        }
    }

}
