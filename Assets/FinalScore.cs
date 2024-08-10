using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    private GameObject challengeMode;
    public Image star1;
    public Image  star2;
    public Image star3;
    void Start()
    {
        challengeMode = GameObject.Find("ChallengeMode");
        if (challengeMode != null )
        {
            Debug.LogWarning("Challenge mode found");
        }
    }

    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Final score: "+score.ToString();
        if (score >=2)
        {
           star1.gameObject.SetActive(true);
        } 
        
        if (score >= 4)
        {
            star2.gameObject.SetActive(true);
        } 
        
        if (score >=6)
        {
            star3 .gameObject.SetActive(true);
        }
    }
}
