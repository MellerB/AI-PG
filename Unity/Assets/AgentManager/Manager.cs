using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using NeuralNetwork;

namespace ModelManager
{
    public class Manager
    {  
        List<NetworkModel> Models = new List<NetworkModel>();
        public int NumModels{ get; }
        public double LearningRate{ get; }
        Manager(List<NetworkModel> models,double learningRate = 0.1f)
        {
            LearningRate = learningRate;
            Models = models;
            NumModels = models.Count;
        }

        void SaveTop(int n)
        {
            if(n>Models.Count)
            {
            Debug.Log("Provided number is lower than count of models!");
            }
            else
            {
                //sort Models and kill all that are not in top n
            }
        }

        void Expand()
        {  
            int n = Models.Count;

            int clonesNeeded = NumModels-n;

            List<NetworkModel> clones = new List<NetworkModel>();

            if(n >= NumModels)
            {
                Debug.Log("Models collection is full!");
            }
            else
            {
                for (int i = 0; i < clonesNeeded; i++)
                {
                    clones.Add(Models[i%n].DeepCopy()); // add randomization 
                }
            }


            Models=new List<NetworkModel>(Models.Concat(clones));
        }


    }
}