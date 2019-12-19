using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
namespace NeuralNetwork
{
class Pulse
{
    public double Value { get; set; }
}

class Dendrite
{
    public Pulse InputPulse { get; set; }

    public double SynapticWeight { get; set; }

    public bool Learnable { get; set; }
}

class Neuron
{
    public List<Dendrite> Dendrites { get;set; }

    public Pulse OutputPulse { get; set; }

    private double Weight;

    public Neuron()
    {
        Dendrites = new List<Dendrite>();
        OutputPulse = new Pulse();
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
        foreach(var d in Dendrites)
        {
            computeValue += d.InputPulse.Value * d.SynapticWeight;
        }

        return computeValue;
    }

    private double Activation(double input) //change?
    {
        double treshold = 1.0f;
        return input >= treshold ? 0 : treshold;
    }
}

class NeuralLayer
{
    public List<Neuron> Neurons { get; set; }

    public string Name { get; set; }

    public double Weight { get; set; }

    public NeuralLayer(int count, double initialWeight, string name = "")
    {
        Neurons = new List<Neuron>();
        for(int i = 0; i< count; i++)
        {
            Neurons.Add(new Neuron());
        }

        Weight = initialWeight;

        Name = name;
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
        foreach(var neuron in Neurons)
        {
            neuron.Fire();
        }
    }

    public void Log()
    {
        Debug.Log(Name+": "+Weight);
    }

}

class NetworkModel
{
    public List<NeuralLayer> Layers { get; set; }

    public NetworkModel()
    {
        Layers = new List<NeuralLayer>();
    }

    public void AddLayer(NeuralLayer layer)
    {
        int dendriteCount = 1;

        if (Layers.Count>0)
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
            if(i >= Layers.Count -1)
            {
                break;
            }
        var nextLayer = Layers[i+1];
        CreateNetwork(layer, nextLayer);
        i++;
        }
    }

    public List<double> Decide()
    {

    }

    public void Print()
    {

        Debug.Log("Name | Neurons | Weight");

	foreach (var layer in Layers)
        {
            Debug.Log(layer.Name+" | "+layer.Neurons.Count+" | "+layer.Weight);
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
                first=false;
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
        if(accuracy > 1)
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

