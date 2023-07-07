using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public GameObject[] paletteSlots;
    public int currColor = 0;

    //For Paint Bucket Mode
    public bool paintBucketModeOn;
    public string currentColor;
    public Animator buttonAnimator;
    public CursorController cursorController;
    private Transform player;
    private PlayerSettings playerSettings;
    public event PaintBucketDelegate ColoringModeOnEvent;
    public delegate void PaintBucketDelegate(string color, bool on);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    void Start()
    {
        paintBucketModeOn = false;
        currentColor = "noColor";
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSettings = player.GetComponent<PlayerSettings>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    void Update()
    {
        if (paintBucketModeOn)
        {
            // If Left Click
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray at the position of the mouse cursor
                // Get the topmost object (I think) that the ray hit (maximum depth of 10)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10, LayerMask.GetMask("Game World"));
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Collectible"))
                    {
                        Debug.Log("Hit 2D collider: " + hit.collider.name);
                        // Tell the interactable to color itself in with the current selected color
                        hit.collider.GetComponent<Coloring>().colorIn(currentColor);
                    }
                }
            }
        }
    }

    public void AddColor()
    {
        GameObject slot = paletteSlots[currColor];

        if (slot.transform.childCount > 0)
        {
            GameObject color = slot.transform.GetChild(0).gameObject;
            color.SetActive(true);
            currColor++;
        }
    }

    public void togglePaintBucketMode()
    {
        if (paintBucketModeOn)
        {
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            buttonAnimator.SetBool("ColorModeOn", false);
            cursorController.setDefaultCursor();
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.PaintBucketMode);
            buttonAnimator.SetBool("ColorModeOn", true);
            cursorController.setPaintBucketCursor();
        }
        // Notifies subscribers of the state of Paint Bucket Mode and current selected color
        // when this state changes
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }

    public void changeColor(string color)
    {
        currentColor = color;
        // Notifies/updates subscribers of the change in current selected color
        ColoringModeOnEvent?.Invoke(currentColor, paintBucketModeOn);
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Exploration:
                paintBucketModeOn = false;
                break;
            case GameState.Interaction:
                paintBucketModeOn = false;
                break;
            case GameState.PaintBucketMode:
                paintBucketModeOn = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
