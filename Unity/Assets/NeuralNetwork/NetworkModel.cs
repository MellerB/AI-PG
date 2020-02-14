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
    public class NetworkModel
    {
        public List<NeuralLayer> Layers { get; set; }

        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
        }

        public NetworkModel(string weights)
        {
            Layers = new List<NeuralLayer>();
            LoadFromString(weights);
        }


        public NetworkModel DeepCopy()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Context = new StreamingContext(StreamingContextStates.Clone);
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (NetworkModel)formatter.Deserialize(ms);
            }
        }

        public void AddLayer(NeuralLayer layer)
        {
            int dendriteCount = 1;
            if (Layers.Count > 0)
            {
                dendriteCount = Layers[Layers.Count - 1].Neurons.Count;
            }

            foreach (var element in layer.Neurons)
            {
                for (int i = 0; i < dendriteCount; i++)
                {
                    element.Dendrites.Add(new Dendrite());
                }
            }
        }

        public void Build()
        {
            int i = 0;
            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1)
                {
                    break;
                }
                var nextLayer = Layers[i + 1];
                CreateNetwork(layer, nextLayer);
                i++;
            }
        }


        public void Randomize(double lr)
        {
            foreach (var layer in Layers)
            {
                layer.Randomize(lr);
            }
        }

        public List<double> Decide(List<double> X)
        {
            var inputLayer = Layers[0];
            List<double> outputs = new List<double>();

            for (int i = 0; i < X.Count; i++)
            {
                inputLayer.Neurons[i].OutputPulse.Value = X[i];
            }
            ComputeOutput();
            foreach (var neuron in Layers.Last().Neurons)
            {
                outputs.Add(neuron.OutputPulse.Value);
            }
            return outputs;
        }

        public void Print()
        {

            Debug.Log("Name | Neurons");

            foreach (var layer in Layers)
            {
                Debug.Log(layer.Name + " | " + layer.Neurons.Count);
            }
        }

        private void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var to in connectingTo.Neurons)
            {
                foreach (var from in connectingFrom.Neurons)
                {
                    to.Dendrites.Add(new Dendrite() { InputPulse = from.OutputPulse });
                }
            }
        }

        private void ComputeOutput()
        {
            bool first = true;
            foreach (var layer in Layers)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    layer.Forward();
                }
            }
        }

        public override string ToString()
        {
            string tmp = "";
            for (int i = 1; i < Layers.Count; i++)
            {
                tmp += Layers[i].ToString();
                if (i != Layers.Count - 1)
                    tmp += "\n";
            }
            tmp+="\n";
            return tmp;
        }
       

        public void LoadFromString(string input)
        {
            List<string> layersText = input.Split(new string[] { "{","}" },StringSplitOptions.RemoveEmptyEntries).ToList();

            layersText  = layersText.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            if(layersText.Count!=Layers.Count-1)
            {
                throw new System.FormatException("inconsistent layers and readen layers number"+layersText.Count+" vs "+Layers.Count);
            }

            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].LoadFromString(layersText[i-1]);
            }
        }

    }
}






