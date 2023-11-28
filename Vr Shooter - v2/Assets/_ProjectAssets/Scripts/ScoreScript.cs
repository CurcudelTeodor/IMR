using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{   
    public static int scoreValue = 0;
    public static string hitStatus;

    Text score;
    Text hit;
    public Text scoreText;
    public Text hitText;
    
    // Start is called before the first frame update
    void Start()
{
    if (scoreText != null)
    {
        score = scoreText;
        hit = hitText;
    }
    else
    {
        Debug.LogError("scoreText variable is not assigned!");
    }
}

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
        hit.text = "Hit? " + hitStatus;
    }
}
