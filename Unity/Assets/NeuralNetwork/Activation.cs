using UnityEngine;
using System;

namespace NeuralNetwork
{
    public static class Activation
    {
        static double SigmoidMinus(double x)
        {
            return 2.0/(1.0 + Math.Exp(-x))-1;
        }


    }

}
