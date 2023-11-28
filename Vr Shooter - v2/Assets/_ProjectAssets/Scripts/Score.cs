using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshPro TXT;
    int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        TXT = gameObject.GetComponent<TextMeshPro>();
        totalScore = 0;
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        TXT.text = "Score = " + totalScore;
    }
}