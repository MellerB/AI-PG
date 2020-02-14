using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputMonitor : MonoBehaviour
{


    public Agent Target;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
            Debug.LogWarning("Target is null, no data will be shown.");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            string input_data = "";
            string output_data = "";
            for (int i = 0; i < Target.lastInputs.Count; i++)
            {
                input_data += "Input " + i + " --- " + Target.lastInputs[i] + "\n";
            }

            for (int i = 0; i < Target.lastOutputs.Count; i++)
            {
                output_data += "Output " + i + " --- " + Target.lastOutputs[i] + "\n";
            }

            text.text = input_data + output_data;
        }

    }
}
