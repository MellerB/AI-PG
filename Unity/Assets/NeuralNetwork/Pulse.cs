using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace NeuralNetwork
{
    [Serializable]
    public class Pulse
    {	
        [JsonProperty]
        public double Value { get; set; }
    }
}






