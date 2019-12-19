using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    NetworkModel model = new NetworkModel();
    model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
    model.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
    model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));
      
    model.Build();
    model.Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
