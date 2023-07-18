using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{

    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D interactableCursor;
    [SerializeField] private Texture2D NPCCursor;
    [SerializeField] private Texture2D collectibleCursor;
    [SerializeField] private Texture2D WateringCansPuzzle_waterCursor;
    [SerializeField] private Texture2D WateringCansPuzzle_fertilizerCursor;
    [SerializeField] private Texture2D paintBucketCursor;


    private void Awake()
    {
        if (defaultCursor != null)
        {
            Cursor.SetCursor(defaultCursor, default, default);
        }
        else
        {
            Cursor.SetCursor(default, default, default);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDefaultCursor()
    {
        if (defaultCursor != null)
        {
            Cursor.SetCursor(defaultCursor, default, default);
        }
        else
        {
            Cursor.SetCursor(default, default, default);
        }
    }

    public void setInteractableCursor()
    {
        Cursor.SetCursor(interactableCursor, default, default);
    }

    public void setNPCCursor()
    {
        Cursor.SetCursor(NPCCursor, default, default);
    }

    public void setCollectibleCursor()
    {
        Cursor.SetCursor(collectibleCursor, default, default);
    }

    public void setWateringCansPuzzle_waterCursor()
    {
        Cursor.SetCursor(WateringCansPuzzle_waterCursor, default, default);
    }

    public void setWateringCansPuzzle_fertilizerCursor()
    {
        Cursor.SetCursor(WateringCansPuzzle_fertilizerCursor, default, default);
    }

    public void setPaintBucketCursor()
    {
        Cursor.SetCursor(paintBucketCursor, default, default);
    }
}
