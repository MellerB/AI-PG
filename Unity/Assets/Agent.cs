using System;
using NeuralNetwork;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Agent : MonoBehaviour
{
    public NetworkModel network;

    //Target cookie jar for every agent that exists (?TODO?: handle null case?)
    public static Transform cookieJar;


    //function to call when this agent dies
    public Action<Agent> deathCallback;
    public List<double> lastInputs = new List<double>(); //inputs that were fed in previous frame (for UI and debugging)
    public List<double> lastOutputs = new List<double>(); //outputs that were outputted in previous frame (for UI and debugging)
    private float ForceMultiplier = 10.0f;

    //View arc in radians
    [SerializeField]
    private float viewArc = 2.0f;

    public float ViewArc
    {
        get { return viewArc; }
        set
        {
            viewArc = value;
            arcStep = viewArc / (float)rayCount; // recalculate arcStep
        }
    }

    [SerializeField]
    //Number of rays that will be cast
    private int rayCount = 8;


    private float arcStep = 0f;



    private Rigidbody _rigidbody;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();


        arcStep = viewArc / (float)rayCount;

        cookieJar = GameObject.Find("cookieJar").transform;
        network = new NetworkModel();
        network.Layers.Add(new NeuralLayer(1 + rayCount, 0.0, ActivationFunc.Linear, "INPUT")); //rayCount + one for CookieJar position 
        network.Layers.Add(new NeuralLayer(11, 0.0, ActivationFunc.Linear, "HIDDEN"));
        network.Layers.Add(new NeuralLayer(2, 0.0, ActivationFunc.Tanh, "OUTPUT"));
        network.Build();
        network.Randomize(0.5);
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
        _rigidbody.AddForce(new Vector3((float)activations[0] * ForceMultiplier, 0.0f, (float)activations[1] * ForceMultiplier));
    }

    //Gathers inputs from enviroment
    private List<double> GatherInputs()
    {
        List<double> results = new List<double>();
        //1. raycast 
        for (int i = 0; i < rayCount; i++)
        {
            float curr_arc = i * arcStep * Mathf.PI;
            Vector3 dir = new Vector3(Mathf.Cos(curr_arc), 0, Mathf.Sin(curr_arc));
            dir.Normalize(); //OPTM: Not needed as Cos and Sin are in [-1,1]?
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, dir, out hit, 100.0f, 1 << 10))
            {
                results.Add((double)hit.distance / 100.0f);
                Debug.DrawRay(transform.position, hit.point - transform.position, Color.black, 0.01f, true);
            }
            else
            {
                results.Add(1.0f); // if nothing was hit, add max
            }
        }

        //2. get distance from the cookie jar
        results.Add(Vector3.Distance(this.transform.position, cookieJar.position) / 100.0f);
        lastInputs = results;
        return results;
    }

    public void OnCollisionEnter(Collision c)
    {
        //if layer is wall layer
        if (c.collider.gameObject.layer == 10)
        {
            //die...
            this?.deathCallback(this);
            Destroy(this.gameObject);
        }
    }

    //EDITOR
    void OnValidate()
    {
        //Because Unity does not support property exposing to the Inspector, we use OnValidate (called whenever, whatever changed by the Inspecotr)
        //And force property to fire.
        ViewArc = viewArc;
    }


}





