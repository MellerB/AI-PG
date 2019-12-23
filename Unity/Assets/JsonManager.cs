using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using NeuralNetwork;
using Newtonsoft.Json;
public class JsonManager
{
    protected string timeString;

    public JsonManager()
    {
        timeString = DateTime.Now.ToString("yy-MM-dd_HH:mm:ss");
        timeString = timeString.Replace(' ','_');
    }

    public void saveModelsList(List<NetworkModel> models)
    {
        string jsonString = JsonConvert.SerializeObject(models);
        string path = Application.dataPath+"/jsonModels/"+timeString+".json";
        File.WriteAllText(path,jsonString);
    }

}