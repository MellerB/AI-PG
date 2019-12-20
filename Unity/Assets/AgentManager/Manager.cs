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
        public int NumAgents{ get; }
        public double LearningRate{ get; }
        Manager(List<Agent> agents,double learningRate = 0.1f)
        {
            LearningRate = learningRate;
            Agents = agents;
            NumAgents = agents.Count;
        }

        void SaveTop(int n)
        {
            if(n>Agents.Count)
            {
            Debug.Log("Provided number is lower than count of agents!");
            }
            else
            {
                //sort Agents and kill all that are not in top n
            }
        }

        void Expand()
        {  
            int n = Agents.Count;

            int clonesNeeded = NumAgents-n;

            List<Agent> clones = new List<Agent>();

            if(n >= NumAgents)
            {
                Debug.Log("Agents collection is full!");
            }
            else
            {
                for (int i = 0; i < clonesNeeded; i++)
                {
                    clones.Add(Agents[i%n].DeepCopy());
                }
            }


            Agents=Agents.Concat(clones);
        }


    }
}