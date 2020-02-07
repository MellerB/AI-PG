using UnityEngine;


//Calculates score based on total moved distance
public class MovementScoreRule : MonoBehaviour, IScoringRule
{

    float score = 0;
    Vector3 lastPos;

    void Start()
    {
        lastPos = this.transform.position;
    }


    void FixedUpdate()
    {
        score += Vector3.Distance(this.transform.position, lastPos);
    }



    public float GetScore()
    {
        return score;
    }
}