using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DDRManager : MonoBehaviour
{
    public AudioSource music;

    public bool startPlaying;

    public BeatScroller beatScroller;

    public static DDRManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public int currentCombo;

    public Text scoreText;
    public Text comboText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentText, normalText, goodText, perfectText, missText, finalScoreText, failedScoreText;

    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private GameObject failedScreen;

    // Screens
    [SerializeField] private GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;
    private bool puzzlePaused;

    // Interaction to play after completing puzzle (in this case, get ticket)
    [SerializeField] private Interaction postPuzzleInteraction;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        welcomeScreen.SetActive(true);

        // To be consistent with the other puzzles
        puzzleCompletionScreen.SetActive(false);
        exitGameConfirmationScreen.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Puzzle);

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        puzzlePaused = false;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPlaying = true;
                beatScroller.hasStarted = true;

                music.Play();
            }
        } else
        {
            if (!music.isPlaying && !resultsScreen.activeInHierarchy && !puzzlePaused)
            {
                if (currentScore < 34000)
                {
                    failedScreen.SetActive(true);
                    failedScoreText.text = "Your score was: " + currentScore.ToString();
                    
                } else
                {
                    resultsScreen.SetActive(true);
                    scoreText.enabled = false;
                    comboText.enabled = false;

                    normalText.text = normalHits.ToString();
                    goodText.text = goodHits.ToString();
                    perfectText.text = perfectHits.ToString();
                    missText.text = missedHits.ToString();

                    float totalHit = normalHits + goodHits + perfectHits;
                    float percentHit = (totalHit / totalNotes) * 100f;
                    percentText.text = percentHit.ToString("F1") + "%";

                    finalScoreText.text = currentScore.ToString();

                    Invoke("OpenPuzzleCompletionScreen", 7);
                }
            }
        }
    }

    public void CloseWelcomeScreen()
    {
        welcomeScreen.SetActive(false);
    }

    public void NoteHit()
    {
        Debug.Log("Note Hit");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        currentCombo++;
        comboText.text = "Combo: " + currentCombo;

        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Note Missed");

        currentMultiplier = 1;
        multiplierTracker = 0;

        currentCombo = 0;

        comboText.text = "Combo: " + currentCombo;

        missedHits++;
    }

    // Reloads scene for player to retry minigame
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Functions for puzzle pause, exit and completion
    public void PausePuzzle()
    {
        puzzlePaused = true;
        exitGameConfirmationScreen.SetActive(true);
        Time.timeScale = 0;
        music.Pause();
    }

    public void ResumePuzzle()
    {
        puzzlePaused = false;
        Time.timeScale = 1;
        exitGameConfirmationScreen.SetActive(false);
        music.Play();
    }

    public void ExitPuzzle()
    {
        Time.timeScale = 1;
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.ChangeScene(GameScene.Arcade);
    }

    private void OpenPuzzleCompletionScreen()
    {
        puzzleCompletionScreen.SetActive(true);
    }

    public void ExitCompletedPuzzle()
    {
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.Arcade);
    }
}
