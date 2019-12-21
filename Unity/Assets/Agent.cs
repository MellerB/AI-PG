using NeuralNetwork;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Agent : MonoBehaviour
{
    NetworkModel network;
    public NetworkModel Network
    {
        get {return network;}
        private set {network = value;}
    }

    //Target cookie jar for every agent that exists
    public static Transform cookieJar;

    public List<double> lastInputs = new List<double>();
    public List<double> lastOutputs = new List<double>();


    private float ForceMultiplier = 10.0f;

    void Awake()
    {
        cookieJar = GameObject.Find("cookieJar").transform;
        network = new NetworkModel();
        network.Layers.Add(new NeuralLayer(9, 0.0,ActivationFunc.BinaryStep, "INPUT"));
        network.Layers.Add(new NeuralLayer(15, 0.0,ActivationFunc.Linear, "HIDDEN"));
        network.Layers.Add(new NeuralLayer(2, 0.0,ActivationFunc.Tanh, "OUTPUT"));
        network.Build();

        network.Randomize(0.4);
    }

    void Start()
    {


    }


    void FixedUpdate()
    {
        lastOutputs = network.Decide(GatherInputs());
        ParseOutput(lastOutputs); 
    }




    ///<summary>Parses output of a neural network</summary>
    ///Activations go as follows:
    ///[0] - force on X axis
    ///[1] - force on Z axis
    private void ParseOutput(List<double> activations)
    {   
        this.GetComponent<Rigidbody>().AddForce(new Vector3((float)activations[0] * ForceMultiplier, 0.0f, (float)activations[1] * ForceMultiplier));
    }

    //Gathers inputs from enviroment
    //WARNING: If something fucks up with normalization, this is the func you are looking for 
    private List<double> GatherInputs()
    {
        List<double> results = new List<double>();
        //1. raycast 8 diffrent rays in 8 diffrent directions and get distance from the walls
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {

                if(i == 0 && j == 0)
                    continue;


                Vector3 dir = new Vector3(i, 0, j);
                dir.Normalize();
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, dir, out hit, 100.0f, 1 << 10))
                {
                    results.Add((double)hit.distance/100.0f);
                    Debug.DrawRay(transform.position, hit.point - transform.position, Color.black, 0.01f, true);
                }
                else 
                {
                    results.Add(1.0f);
                }
            }
        }

        //2. get distance from the cookie jar
        results.Add(Vector3.Distance(this.transform.position, cookieJar.position)/100.0f);
        lastInputs = results;
        return results;
    }




}





