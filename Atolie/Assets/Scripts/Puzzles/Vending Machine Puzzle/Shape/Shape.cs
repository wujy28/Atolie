using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;

    [HideInInspector]
    public ShapeData CurrentShapeData;

    public int TotalSquareNumber { get; set; }

    private List<GameObject> currShape = new List<GameObject>();
    private Vector3 shapeStartScale;
    private RectTransform _transform;
    private bool shapeDraggable = true;
    private Canvas canvas;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool shapeActive = true;

    public void Awake()
    {
        shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _transform = this.GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        shapeDraggable = true;
        startPosition = _transform.localPosition;
        startRotation = _transform.localRotation;
        shapeActive = true;
    }

    private void OnEnable()
    {
        TangramEvents.MoveShapeToStartPosition += MoveShapeToStartPosition;
    }

    private void OnDisable()
    {
        TangramEvents.MoveShapeToStartPosition -= MoveShapeToStartPosition;
    }

    //Function to check if the shape is at its beginning position
    public bool IsOnStartPosition()
    {
        return _transform.localPosition == startPosition;
    }

    //Function to check if shape is active
    public bool IsAnyOfShapeSquareActive()
    {
        foreach (var square in currShape)
        {
            if (square.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    //Function to deactivate the square of the shape
    public void DeactivateSquare()
    {
        if (shapeActive)
        {
            foreach (var square in currShape)
            {
                square?.GetComponent<ShapeSquare>().DeactivateShape();
            }
        }

        shapeActive = false;
    }

    //Function to activate the square of the shape
    public void ActivateSquare()
    {
        if (!shapeActive)
        {
            foreach (var square in currShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
        }

        shapeActive = true;
    }

    //Function to get new shape
    public void RequestNewShape(ShapeData shapeData)
    {
        _transform.localPosition = startPosition;
        CreateShape(shapeData);
    }

    //Function to create a shape
    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;
       TotalSquareNumber = GetNumberOfSquares(shapeData);

        while (currShape.Count <= TotalSquareNumber)
        {
            currShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        foreach (var square in currShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        //set postiion to form final shape
        for (var row = 0; row < shapeData.rows; row++)
        {
            for ( var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    currShape[currentIndexInList].SetActive(true);
                    currShape[currentIndexInList].GetComponent<RectTransform>().localPosition = 
                        new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), 
                        GetYPositionForShapeSquare(shapeData, row, moveDistance));

                    currentIndexInList++;
                }
            }
        }
    }

    //Function to get the x coordinate of shape square
    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if (shapeData.columns > 1)
        {
            float startXPos;
            if (shapeData.columns % 2 != 0)
                startXPos = (shapeData.columns / 2) * moveDistance.x * -1;
            else
                startXPos = ((shapeData.columns / 2) - 1) * moveDistance.x * -1 - moveDistance.x / 2;
            shiftOnX = startXPos + column * moveDistance.x;

        }
        return shiftOnX;
    }

    //Function to get the y coordinate of shape square
    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (shapeData.rows > 1)
        {
            float startYPos;
            if (shapeData.rows % 2 != 0)
                startYPos = (shapeData.rows / 2) * moveDistance.y;
            else
                startYPos = ((shapeData.rows / 2) - 1) * moveDistance.y + moveDistance.y / 2;
            shiftOnY = startYPos - row * moveDistance.y;
        }
        return shiftOnY;
    }

    //Function to get the number of squares in a shape
    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;

        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if (active)
                {
                    number++;
                }
            }
        }

        return number;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this.gameObject.transform.Rotate(new Vector3(0f, 0f, -90f));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchorMin = new Vector2(0, 0);
        _transform.anchorMax = new Vector2(0, 0);
        _transform.pivot = new Vector2(0, 0);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, 
            eventData.position, Camera.main, out pos);
        _transform.localPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeStartScale;
        TangramEvents.CheckIfShapeCanBePlaced();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    //Function to move shape back to start position
    private void MoveShapeToStartPosition()
    {
        _transform.transform.localPosition = startPosition;
    }

    //Function to reset shape
    public void ResetShape()
    {
        if (!shapeActive)
        {
            for (int i = 0; i < currShape.Count - 1; i++)
            {
                currShape[i]?.GetComponent<ShapeSquare>().ActivateShape();
            }
            this.gameObject.transform.rotation = startRotation;
        }

        shapeActive = true;

        if (!IsOnStartPosition())
        {
            MoveShapeToStartPosition();
        }
    }
}
