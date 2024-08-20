using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/*
 * This class is for the activity challenge mode, where the user is given a time period (1 min by default) to complete as many activities as possible. 
 */
public class ChallengeMode : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;

    private GameObject facePanels;
    public GameObject scorePanel;

    //Last tick time is used to play a "click" sound in the final 5 seconds of the timer (see Update method)
    private float lastTickTime = 0f;
    private int score = 0;

    public AudioSource tick;
    public AudioSource missionCompleteSound;

    //BeginCountdown is the text panel that counts 3 2 1 go before the actual timer begins
    private BeginCountdown preCountdown;

    public float timeRemaining = 60f;
    private bool timerIsRunning = false;
    public int Score { get => score; set => score = value; }

    void Start()
    {
        // Setting the text on the scoreboard to 0 initially
        scoreText.text = score.ToString();

        //Have to use GameObject.find here because face panels and BeginCountdown are not in the workstation prefab
        facePanels = GameObject.Find("FacePanels");
        preCountdown = GameObject.Find("321Go").GetComponent<BeginCountdown>();
    }

    // Called whenever a successful point is score, updating the scoreboard text
    public void AddPoint()
    {
        if (timerIsRunning)
        {
            score += 1;
            scoreText.text = score.ToString();
        }
    }
    
    public void StartCountdown()
    {
        StartCoroutine("PreCountdown");
    }
    
    // Displaying the 3, 2, 1 GO message before start of the timer, then starting time
    private IEnumerator PreCountdown()
    {
        preCountdown.ThreeTwoOneGo();
        yield return new WaitForSeconds(3);
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            // Updating the timer every second
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else //Stop the timer
            {
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayScore();
                scorePanel.SetActive(false);
            }

            // Playing a "tick" sound in the last 5 seconds
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

    // Used to display remaining time on the scoreboard
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Displays the face panels with the final score after completion
    void DisplayScore()
    {

        missionCompleteSound.Play();
        facePanels.SetActive(true);

        //Using GameObject.Find here again because the face panels are not inside this prefab
        FinalScore finalScore = facePanels.GetComponentInChildren<FinalScore>();
        finalScore.ShowFinalScore(score);
    }
}
