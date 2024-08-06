using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float moveDistance = 50f; // Distance the text moves
    public float fadeDuration = 2f; // Duration of the fade

    private Vector3 startPos;
    private Color startColor;
    private Coroutine currentCoroutine;

    void Start()
    {
        startPos = popupText.rectTransform.localPosition;
        startColor = popupText.color;
    }

    public void ShowMessage(string message, bool isCorrect)
    {
        popupText.text = message;
        popupText.color = isCorrect ? Color.green : Color.red;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            ResetPopup();
        }

        currentCoroutine = StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        float elapsedTime = 0f;
        float startFadeTime = 0f;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);
        Color endColor = new Color(popupText.color.r, popupText.color.g, popupText.color.b, 0);

        while (elapsedTime < fadeDuration)
        {
            popupText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;

            if (elapsedTime > fadeDuration / 2)
            {
                popupText.color = Color.Lerp(popupText.color, endColor, startFadeTime / (fadeDuration / 2));
                startFadeTime += Time.deltaTime;
            }
            yield return null;
        }


        ResetPopup();
    }

    private void ResetPopup()
    {
        popupText.rectTransform.localPosition = startPos;
    }
}

