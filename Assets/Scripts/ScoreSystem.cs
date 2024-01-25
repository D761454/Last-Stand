using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int score;

    public void AddScore(int scoreGained)
    {
        score += scoreGained;
    }

    public void RemoveScore(int scoreLost) 
    {
        score -= scoreLost;
    }
}
