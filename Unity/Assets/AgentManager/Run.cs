using System;
using System.Collections;
using System.Collections.Generic;
using NeuralNetwork;
using UnityEngine;


//Class that represents data gathered from one simulation run
public class Run
{
    public event EventHandler<RunCompletedEventArgs> RunComplete;
    List<GameObject> agents = new List<GameObject>();
    


    [SerializeField]
    GameObject agentPrefab;

    //Creates a new run with num_agents number of randomly initialized agents
    public Run(int num_agents)
    {
        for(int i = 0; i < num_agents; i++)
        {
            agents.Add(CreateNewAgent());
        }
    }

    //Creates a new run with agents initialized with given models
    public Run(List<NetworkModel> models)
    {
        foreach(NetworkModel m in models)
        {
            GameObject a = CreateNewAgent();
            a.GetComponent<Agent>().Network = m; 
        }
    }
    

    //Begins run by activating all agents
    public void BeginRun()
    {
        foreach(GameObject a in agents)
        {
            a.gameObject.SetActive(true);
        }
    }


    //Creates new empty agent, that is inactive
    private GameObject CreateNewAgent()
    {
        GameObject agent = GameObject.Instantiate(agentPrefab);
        agent.SetActive(false);
        return agent;
    }


    public class RunCompletedEventArgs : EventArgs
    {
    
    }

}