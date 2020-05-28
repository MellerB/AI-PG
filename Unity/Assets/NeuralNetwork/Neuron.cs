using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;

namespace NeuralNetwork
{
    [Serializable]
    public class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        public Func<double, double> Activation;

        public Neuron(Func<double, double> activation)
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
            Activation = activation;
        }

        public void Randomize(double lr)
        {
            foreach (var dendrite in Dendrites)
            {
                dendrite.Randomize(lr);
            }
        }

        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }

        private double Sum()
        {
            double computeValue = 0.0f;
            foreach (var d in Dendrites)
            {
                computeValue += d.InputPulse.Value * d.SynapticWeight;
            }

            return computeValue;
        }

        public override string ToString()
        {
            string tmp = "[";
            for (int i = 0; i < Dendrites.Count; i++)
            {
                tmp += Dendrites[i].ToString();
                if (i != Dendrites.Count - 1)
                    tmp += ", ";
            }
            tmp += "]";
            return tmp;
        }

        public void LoadFromString(string input){
        List<string> dendritesText = input.Split(new string[] { ", ","," }, StringSplitOptions.RemoveEmptyEntries).ToList();

        dendritesText  = dendritesText.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            if(dendritesText.Count!=Dendrites.Count)
            {
                throw new System.FormatException("inconsistent layers and readen dendrites number");
            }

            for (int i = 0; i < Dendrites.Count; i++)
            {
                Dendrites[i].SynapticWeight = Convert.ToDouble(dendritesText[i]);
            }

        }


    }
}






