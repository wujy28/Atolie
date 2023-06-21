using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text percentText, normalText, goodText, perfectText, missText, finalScoreText;

    [SerializeField] private GameObject welcomeScreen;

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

    // Start is called before the first frame update
    void Start()
    {
        welcomeScreen.SetActive(true);

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;

                music.Play();
            }
        } else
        {
            if (!music.isPlaying && !resultsScreen.activeInHierarchy)
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

        //currentScore += scorePerNote * currentMultiplier;
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
}
