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
      model.Layers.Add(new NeuralLayer(9, 0.1,ActivationFunc.Tanh, "INPUT"));
      model.Layers.Add(new NeuralLayer(11, 0.1,ActivationFunc.Tanh, "HIDDEN"));
      model.Layers.Add(new NeuralLayer(4, 0.1,ActivationFunc.Tanh, "OUTPUT"));

      model.Build();
      model.Print();
      Debug.Log(model.Decide(new List<double>(){1.0f,1.0f})[0]);
      Debug.Log(UnityEngine.Random.Range(0.0f,1.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
