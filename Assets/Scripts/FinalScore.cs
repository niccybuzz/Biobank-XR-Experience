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

    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Final score: " + score.ToString();
        if (score >= 2)
        {
            star1.gameObject.SetActive(true);
        }

        if (score >= 4)
        {
            star2.gameObject.SetActive(true);
        }

        if (score >= 6)
        {
            star3.gameObject.SetActive(true);
        }
    }
}
