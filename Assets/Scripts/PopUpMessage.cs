using System.Collections;
using TMPro;
using UnityEngine;

// Displays the Correct or Wrong popup message above the laptop when select button is pressed
public class PopupMessage : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float moveDistance = 50f; // Distance the text moves
    public float fadeDuration = 2f; // Duration of the fade

    private Vector3 _startPosition;
    private Color _startColor;
    private Coroutine _currentCoroutine; // used to prevent overlap between messages

    void Start()
    {
        _startPosition = popupText.rectTransform.localPosition; // The message starts behind the laptop
        _startColor = popupText.color; 
    }

    public void ShowMessage(string message, bool isCorrect)
    {
        popupText.text = message;
        popupText.color = isCorrect ? Color.green : Color.red;

        // Stop the current animation if it is happening, to start a new one
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            ResetPopup();
        }

        _currentCoroutine = StartCoroutine(AnimatePopup());
    }

    // Makes the popup message travel upwards over time, eventually fading to transparent
    private IEnumerator AnimatePopup()
    {
        float elapsedTime = 0f;
        float startFadeTime = 0f;

        Vector3 endPos = _startPosition + new Vector3(0, moveDistance, 0);
        Color endColor = new Color(popupText.color.r, popupText.color.g, popupText.color.b, 0);

        while (elapsedTime < fadeDuration)
        {
            popupText.rectTransform.localPosition = Vector3.Lerp(_startPosition, endPos, elapsedTime / fadeDuration); // Lerp betwen start and end positions
            elapsedTime += Time.deltaTime; // Increment by time passed

            // Halfway through the animation, begin fading to transparent
            if (elapsedTime > fadeDuration / 2)
            {
                popupText.color = Color.Lerp(popupText.color, endColor, startFadeTime / (fadeDuration / 2));
                startFadeTime += Time.deltaTime;
            }
            yield return null;
        }
        // Finally, put text back behind laptop
        ResetPopup();
    }

    private void ResetPopup()
    {
        popupText.rectTransform.localPosition = _startPosition;
    }
}

