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
    public class Pulse
    {
        public double Value { get; set; }
    }

    [Serializable]
    public class Dendrite
    {
        public Pulse InputPulse { get; set; }

        public double SynapticWeight { get; set; }

        public bool Learnable { get; set; }

        public Dendrite()
        {
            SynapticWeight = 0.5f;
        }

        public void Randomize(double lr)
        {
            float t = (float)lr;
            SynapticWeight += (double)UnityEngine.Random.Range(-t, t);
        }
    }

    [Serializable]
    public class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        private double Weight;

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

        public void UpdateWeights(double new_weights)
        {
            foreach (var terminal in Dendrites)
            {
                terminal.SynapticWeight = new_weights; //check it??
            }
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

    }
    [Serializable]
    public class NeuralLayer
    {
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

            Weight = initialWeight;

            Name = name;
        }


        public void Randomize(double lr)
        {
            foreach (var neuron in Neurons)
            {
                neuron.Randomize(lr);
            }
        }

        public void Optimize(double learningRate, double delta)
        {
            Weight += learningRate * delta;
            foreach (var neuron in Neurons)
            {
                neuron.UpdateWeights(Weight);
            }
        }

        public void Forward()
        {
            foreach (var neuron in Neurons)
            {
                neuron.Fire();
            }
        }

        public void Log()
        {
            Debug.Log(Name + ": " + Weight);
        }

    }
    
    [Serializable]
    public class NetworkModel
    {
        public List<NeuralLayer> Layers { get; set; }

        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
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


        public void Rebuild()
        {
            Layers.Clear();
            Build();
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

            Debug.Log("Name | Neurons | Weight");

            foreach (var layer in Layers)
            {
                Debug.Log(layer.Name + " | " + layer.Neurons.Count + " | " + layer.Weight);
            }
        }

        private void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var to in connectingTo.Neurons)
            {
                foreach (var from in connectingFrom.Neurons)
                {
                    to.Dendrites.Add(new Dendrite() { InputPulse = from.OutputPulse, SynapticWeight = connectingTo.Weight });
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

        private void OptimizeWeights(double accuracy)
        {
            double lr = 0.1f;
            if (accuracy == 1)
            {
                return;
            }
            if (accuracy > 1)
            {
                lr = -lr;
            }

            foreach (var layer in Layers)
            {
                layer.Optimize(lr, 1);
            }
        }

    }
}






