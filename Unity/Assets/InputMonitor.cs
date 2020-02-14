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
<<<<<<< HEAD

=======
        if (Target == null)
            Debug.LogWarning("Target is null, no data will be shown.");
>>>>>>> b38aa2f88f6dcf7e31c7af9d59350c6ab508aa23
    }

    // Update is called once per frame
    void FixedUpdate()
    {
<<<<<<< HEAD
        string input_data = "";
        string output_data = "";
        for (int i = 0; i < Target.lastInputs.Count; i++)
=======
        if (Target != null)
>>>>>>> b38aa2f88f6dcf7e31c7af9d59350c6ab508aa23
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

<<<<<<< HEAD
        for (int i = 0; i < Target.lastOutputs.Count; i++)
        {
            output_data += "Output " + i + " --- " + Target.lastOutputs[i] + "\n";
        }


        text.text = input_data + output_data;
=======
>>>>>>> b38aa2f88f6dcf7e31c7af9d59350c6ab508aa23
    }
}
