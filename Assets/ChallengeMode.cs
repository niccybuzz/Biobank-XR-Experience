using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChallengeMode : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;
    private GameObject facePanels;
    private float lastTickTime = 0f;
    private int score = 0;
    public AudioSource tick;
    private BeginCountdown countdown;

    public float timeRemaining = 60f;
    private bool timerIsRunning = false;

    public int Score { get => score; set => score = value; }

    void Start()
    {
        scoreText.text = score.ToString();
        facePanels = GameObject.Find("FacePanels");
        if (facePanels!=null)
        {
            Debug.LogWarning("Face panels found");
        }
        countdown = GameObject.Find("321Go").GetComponent<BeginCountdown>();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
    
    public void StartCountdown()
    {
        StartCoroutine("PreCountdown");
    }
    
    private IEnumerator PreCountdown()
    {
        countdown.ThreeTwoOneGo();
        yield return new WaitForSeconds(3);
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayScore();
            }
            if (timeRemaining <= 5)
            {
                // Check if 1 second has passed since the last tick
                if (Time.time >= lastTickTime + 1f)
                {
                    tick.Play();
                    lastTickTime = Time.time;  // Update the last tick time
                }
            }

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayScore()
    {

        facePanels.SetActive(true);
        FinalScore finalScore = facePanels.GetComponentInChildren<FinalScore>();
        finalScore.ShowFinalScore(score);
    }
}
