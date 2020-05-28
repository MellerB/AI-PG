using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using NeuralNetwork;
using System.IO;

public class ModelManager
{
    public List<NetworkModel> Models = new List<NetworkModel>();
    public int NumModels { get; }
    public double LearningRate { get; }


    public ModelManager(List<NetworkModel> models, double learningRate = 0.2f)
    {
        LearningRate = learningRate;
        Models = models;
        NumModels = models.Count;
    }

    public ModelManager(List<NetworkModel> models,String pathToWeights, int modelsToBe, double learningRate = 0.2f)
    {
        LearningRate = learningRate;
        NumModels = modelsToBe;
        Models = new List<NetworkModel>();
        var files = Directory.EnumerateFiles(pathToWeights)
                     .OrderByDescending(filename => filename);
    
        foreach (var filename in files)
        {
            Models.Add(new NetworkModel(File.ReadAllText(filename)));
        }

    }

    public void SaveToFiles(int n)
    {
        string timeString = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
        timeString = timeString.Replace(' ', '_');

        
        string dirPath = Application.dataPath + "/Weights/" + timeString+ "/";
        Directory.CreateDirectory(dirPath);
        string filePath;
        for (int i = 0; i < n; i++)
        {
            filePath = "No"+i.ToString()+".w";
            File.WriteAllText(dirPath + filePath, Models[i].ToString());
        }
    }
    

    public void SaveTop(int n, Run r)
    {
        Models = r.results.OrderBy(x => x.score).Select(x => x.model).ToList();
        if (n > Models.Count)
        {
            Debug.Log("Provided number is lower than count of models!");
        }
        else
        {
            Models.RemoveRange(0, n);
            SaveToFiles(n);
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
