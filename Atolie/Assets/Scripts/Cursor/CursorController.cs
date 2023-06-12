using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D interactableCursor;
    [SerializeField] private Texture2D NPCCursor;
    [SerializeField] private Texture2D collectibleCursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDefaultCursor()
    {
        Cursor.SetCursor(default, default, default);
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
}
