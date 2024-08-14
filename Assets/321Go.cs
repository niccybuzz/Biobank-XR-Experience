using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeginCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int countdownNumber = 3;

    public void ThreeTwoOneGo()
    {
        countdownText.text = countdownNumber.ToString();
        StartCoroutine("Countdown");
    }

    private IEnumerator Countdown()
    {
        while (countdownNumber > 0)
        {
            yield return new WaitForSeconds(1);
            countdownNumber--;
            countdownText.text = countdownNumber.ToString();
        }
        if (countdownNumber == 0)
        {
            countdownText.text = "Go!";
            countdownText.color = Color.green;
            yield return new WaitForSeconds (1);
            countdownText.gameObject.SetActive(false);
        }
    }
}

