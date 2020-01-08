using UnityEngine;

public class LifetimeScoreRule : MonoBehaviour, IScoringRule
{
    float score = 0;

    void FixedUpdate()
    {
        score++;
    }

    public float GetScore()
    {
        return score;
    }
}