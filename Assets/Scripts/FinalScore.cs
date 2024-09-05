using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Displays the final score on a face panel after challenge mode
 * Activates the star images depending on user's score
 */
public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI header;

    private GameObject challengeMode;

    public Image star1;
    public Image star2;
    public Image star3;

    // Score categories can be defined for each scene
    public int minimum_1star;
    public int minimum_2star;
    public int minimum_3star;

    public void ShowFinalScore(int score)
    {
        finalScoreText.text = "Final score: " + score.ToString();
        if (score >= minimum_1star)
        {
            star1.gameObject.SetActive(true);
            header.text = "Great work!";
        } else
        {
            header.text = "Oh dear.. try again?";
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
