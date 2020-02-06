using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace NeuralNetwork
{

    [Serializable]
    public class Dendrite
    {
        public Pulse InputPulse { get; set; }

        public double SynapticWeight { get; set; }

        public bool Learnable { get; set; }

        public Dendrite()
        {
            SynapticWeight = 0;
        }

        public void Randomize(double lr)
        {
            float t = (float)lr;
            SynapticWeight += (double)UnityEngine.Random.Range(-t, t);
        }
    

        public override string ToString()
        {
            return SynapticWeight.ToString();
        }
       

    }
}






