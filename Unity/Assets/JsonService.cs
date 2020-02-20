using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using NeuralNetwork;
using Newtonsoft.Json;
public static class JsonService
{
    public static void SaveModelsList(List<NetworkModel> models)
    {
        try
        {
            string timeString = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
            timeString = timeString.Replace(' ', '_');

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Include;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

            string jsonString = JsonConvert.SerializeObject(models);
            string path = Application.dataPath + "/jsonModels/" + timeString + ".json";
            if(!Directory.Exists(Application.dataPath + "/jsonModels/"))
                Directory.CreateDirectory(Application.dataPath + "/jsonModels/");
            File.WriteAllText(path, jsonString);

        }
        catch(IOException)
        {
            Debug.LogError("Could not save model file");
        }
    }

    public static List<NetworkModel> LoadModelsList(TextAsset jsonFile)
    {
        List<NetworkModel> models = JsonConvert.DeserializeObject<List<NetworkModel>>(jsonFile.ToString());
    	return models;
    }

}
