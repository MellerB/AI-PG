using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using NeuralNetwork;

public class ModelManager
{
    public List<NetworkModel> Models = new List<NetworkModel>();
    public int NumModels { get; }
    public double LearningRate { get; }
    
    public JsonManager jsonManager;

    public ModelManager(List<NetworkModel> models, double learningRate = 0.1f)
    {
        LearningRate = learningRate;
        Models = models;
        NumModels = models.Count;
        jsonManager = new JsonManager();
    }


    public void SaveTop(int n)
    {
        if (n > Models.Count)
        {
            Debug.Log("Provided number is lower than count of models!");
        }
        else
        {
            Models.RemoveRange(0,n);
            jsonManager.saveModelsList(Models);
        }
    }

    public void Expand()
    {
        int n = Models.Count;

        int clonesNeeded = NumModels - n;

        List<NetworkModel> clones = new List<NetworkModel>();

        if (n >= NumModels)
        {
            Debug.Log("Models collection is full!");
        }
        else
        {
            for (int i = 0; i < clonesNeeded; i++)
            {
                clones.Add(Models[i % n].DeepCopy()); // add randomization 
                clones[clones.Count() - 1].Randomize(LearningRate);
            }
        }


        Models = new List<NetworkModel>(Models.Concat(clones));
    }



}
