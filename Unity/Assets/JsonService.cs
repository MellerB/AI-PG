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
        string timeString = DateTime.Now.ToString("yy-MM-dd_HH:mm:ss");
        timeString = timeString.Replace(' ', '_');

	JsonSerializerSettings settings = new JsonSerializerSettings();
	settings.NullValueHandling = NullValueHandling.Include;
	settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

        string jsonString = JsonConvert.SerializeObject(models);
        string path = Application.dataPath + "/jsonModels/" + timeString + ".json";
        File.WriteAllText(path, jsonString);
    }

    public static List<NetworkModel> LoadModelsList(TextAsset jsonFile)
    {
        List<NetworkModel> models = JsonConvert.DeserializeObject<List<NetworkModel>>(jsonFile.ToString());
    	return models;
    }

}
