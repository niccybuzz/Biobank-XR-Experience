using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    private GameObject challengeMode;
    public Image star1;
    public Image star2;
    public Image star3;

    public int minimum_1star;
    public int minimum_2star;
    public int minimum_3star;


    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Final score: " + score.ToString();
        if (score >= minimum_1star)
        {
            star1.gameObject.SetActive(true);
        }

        if (score >= minimum_2star)
        {
            star2.gameObject.SetActive(true);
        }

        if (score >= minimum_3star)
        {
            star3.gameObject.SetActive(true);
        }
    }
}
