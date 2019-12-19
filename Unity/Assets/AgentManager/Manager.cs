using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
namespace AgentManager
{
    public class Manager
    {  
        List<Agent> Agents = new List<Agent>();
        public double LearningRate{ get; }
        Manager(double learningRate = 0.1f)
        {
            LearningRate = learningRate;
        }

        void SaveTop(int n)
        {
            if(n>Agents.Count)
            {
            Debug.Log("Provided number is lower than count of agents");
            }

        }


    }
}