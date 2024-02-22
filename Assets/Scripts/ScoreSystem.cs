using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public int gameScore;

    public void AddScore(int scoreGained)
    {
        score += scoreGained;
        gameScore += scoreGained/2;
    }

    public void RemoveScore(int scoreLost) 
    {
        score -= scoreLost;
    }
}
