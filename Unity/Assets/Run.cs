using System;
using System.Collections;
using System.Collections.Generic;
using NeuralNetwork;
using UnityEngine;
using System.Linq;


//Class that represents data gathered from one simulation run
public class Run
{
    public string runName = "Run #N";
    public event EventHandler<List<AgentResult>> RunComplete;
    public List<GameObject> agents = new List<GameObject>();
    public List<AgentResult> results = new List<AgentResult>();
 
    public static GameObject agentPrefab;

    //Creates a new run with num_agents number of randomly initialized agents
    public Run(int num_agents)
    {
        for (int i = 0; i < num_agents; i++)
        {
            agents.Add(CreateNewAgent());
        }
    }

    //Creates a new run with agents initialized with given models
    public Run(List<NetworkModel> models)
    {
        foreach (NetworkModel m in models)
        {
            GameObject a = CreateNewAgent();
            a.GetComponent<Agent>().network = m;
            agents.Add(a);
        }
    }

    //Begins run by activating all agents
    public void BeginRun()
    {
        foreach (GameObject a in agents)
        {
            a.gameObject.SetActive(true);
        }
    }

    //Called when all agents died
    private void EndRun()
    {
        Debug.Log(runName + " ended");
        string resultString = "";
        for (int i = 0; i < results.Count; i++)
        {
            resultString += "Agent #" + i + " | Score: " + results[i].score;
        }
        Debug.Log(resultString);
        agents.Clear();
        RunComplete(this, results);
    }

    private void AgentDied(Agent a)
    {
        Debug.Log("Agent Died");
        results.Insert(0,new AgentResult(a.Score, a.network));
        agents.Remove(a.gameObject);
        if (agents.Count == 0)
        {
            EndRun();
        }
    }


    //Creates new empty agent, that is inactive
    private GameObject CreateNewAgent()
    {
        GameObject agent = GameObject.Instantiate(agentPrefab);
        agent.SetActive(false);
        agent.GetComponent<Agent>().deathCallback = AgentDied;
        return agent;
    }


    public class AgentResult
    {
        public double score;
        public NetworkModel model;

        public AgentResult(double score, NetworkModel model)
        {
            this.score = score;
            this.model = model.DeepCopy();
        }
    }

}

