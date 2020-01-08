using UnityEngine;
using System.Collections.Generic;

public static class ScoreCalculator
{

    
    public static float CalculateScore(GameObject obj)
    {
        //Get all components with given interfaces and map them to theirs type names
        IScoringRule[] rules = obj.GetComponents<IScoringRule>();
        Dictionary<string, IScoringRule> ruleDictionary = new Dictionary<string, IScoringRule>();
        
        foreach(IScoringRule r in rules)
        {
            ruleDictionary.Add(r.GetType().ToString(), r);
        }

        
        return ruleDictionary["MovementScoreRule"].GetScore() + 0.1f*ruleDictionary["LifetimeScoreRule"].GetScore();
    }


     

}