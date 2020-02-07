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
    public class NeuralLayer{
        public List<Neuron> Neurons { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        Func<double, double> Activation;
        public NeuralLayer(int count, double initialWeight, Func<double, double> activation, string name = "")
        {
            Activation = activation;
            Neurons = new List<Neuron>();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add(new Neuron(Activation));
            }

            Name = name;
        }


        public void Randomize(double lr)
        {
            foreach (var neuron in Neurons)
            {
                neuron.Randomize(lr);
            }
        }

        public void Forward()
        {
            foreach (var neuron in Neurons)
            {
                neuron.Fire();
            }
            
        }

        public override string ToString()
        {
            string tmp = "{\n";
            for (int i = 0; i < Neurons.Count; i++)
            {
                tmp += Neurons[i].ToString();
                if (i != Neurons.Count - 1)
                    tmp += "\n";
            }
            tmp+="\n}";
            return tmp;
        }

    }
}






