using System.Collections;
using TMPro;
using UnityEngine;

/**
 * Used to control the countdown prior to beginning Challenge Mode
 * Displays the big number on the screen and plays the beeps
 */
public class BeginCountdown : MonoBehaviour
{
    [SerializeField]
    AudioSource bip;

    [SerializeField]
    AudioSource beeep;

    [SerializeField]
    TextMeshProUGUI countdownText;

    private int countdownNumber = 3;

    //starts the countdown by calling the IEnumarator
    public void ThreeTwoOneGo()
    {
        countdownText.text = countdownNumber.ToString();
        StartCoroutine("Countdown");
    }

    // Plays a short beep every second until "Go" where it plays a longer beep
    private IEnumerator Countdown()
    {
        while (countdownNumber > 0)
        {
            bip.Play();
            yield return new WaitForSeconds(1);
            countdownNumber--;
            countdownText.text = countdownNumber.ToString();
        }
        if (countdownNumber == 0)
        {
            beeep.Play();
            countdownText.text = "Go!";
            countdownText.color = Color.green;
            yield return new WaitForSeconds (1);
            countdownText.gameObject.SetActive(false);
        }
    }
}

