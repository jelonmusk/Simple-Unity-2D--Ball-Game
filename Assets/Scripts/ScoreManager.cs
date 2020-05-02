using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI currentScoreText,bestScoreText;

    public static ScoreManager instance = null;

    public  int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        currentScore = 0;
        bestScoreText.text = PlayerPrefs.GetInt("Best").ToString();
    }


    public void addScore()
    {
        currentScore += 1;
        currentScoreText.text = currentScore.ToString();
        if(currentScore>PlayerPrefs.GetInt("Best",0))//if we get pass best score then 
        {
            bestScoreText.text = currentScore.ToString();
            PlayerPrefs.SetInt("Best", currentScore);
        }
    }
}
