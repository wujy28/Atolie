using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public ShapeStorage shapeStorage;
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();

    private Indicator indicator; //tbc

    private void OnEnable()
    {
        TangramEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
    }

    private void OnDisable()
    {
        TangramEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    void Start()
    {
        indicator = GetComponent<Indicator>(); //tbc
        CreateGrid();
    }

    //Function to create grid for puzzle
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquarePositions();
    }

    //Function to spawn grid for puzzle
    private void SpawnGridSquares()
    {
        int squareIndex = 0;

        for (int row = 0; row < this.rows; ++row)
        {
            for (int col = 0; col < this.columns; ++col)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SquareIndex = squareIndex;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                squareIndex++;
            }
        }
    }

    //Function to set positions of grid squares based on specifications provided
    private void SetGridSquarePositions()
    {
        int col = 0;
        int row = 0;
        Vector2 squareGapNo = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + everySquareOffset;
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + everySquareOffset;

        foreach(GameObject square in _gridSquares)
        {
            if (col + 1 > columns)
            {
                squareGapNo.x = 0;
                //go to next column
                col = 0;
                row++;
                rowMoved = false;
            }

            var posXOffset = _offset.x * col + (squareGapNo.x * squaresGap);
            var posYOffset = _offset.y * row + (squareGapNo.y * squaresGap);

            if (col > 0 && col % 3 == 0)
            {
                squareGapNo.x++;
                posXOffset += squaresGap;
            }

            if (row > 0 && row % 3 == 0 && rowMoved == false)
            {
                rowMoved = true;
                squareGapNo.y++;
                posYOffset += squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posXOffset, 
                startPosition.y - posYOffset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + posXOffset, 
                startPosition.y - posYOffset, 0.0f);

            col++;
        }
    }

    //Function to check if shape can be placed in the grid
    private void CheckIfShapeCanBePlaced()
    {
        var squareIndexes = new List<int>();

        foreach (var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if (gridSquare.Selected && !gridSquare.SquareOccupied)
            {
                squareIndexes.Add(gridSquare.SquareIndex);
                gridSquare.Selected = false;
            }
        }

        var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
        if ( currentSelectedShape == null)
        {
            return; //There is no selected shape
        }

        if (currentSelectedShape.TotalSquareNumber == squareIndexes.Count)
        {
            foreach (var squareIndex in squareIndexes)
            {
                _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }

            currentSelectedShape.DeactivateSquare();

            CheckIfCompleted();

        } else
        {
            TangramEvents.MoveShapeToStartPosition();
        }

    }

    //Function to check if grid is fully filled with shapes
    void CheckIfCompleted()
    {
        List<int> grid = new List<int>();

        List<int[]> gridData = new List<int[]>();
        gridData.Add(indicator.firstStageGridData);
        gridData.Add(indicator.secondAndThirdStageGridData);
        gridData.Add(indicator.secondAndThirdStageGridData);

        foreach (var squareIndex in gridData[TangramManager.instance.currentStage])
        {
            grid.Add(squareIndex);
        }

        CheckIfSquaresAreCompleted(grid);
    }

    //Helper function to check if puzzle is completed, and move on when it is completed
    private void CheckIfSquaresAreCompleted(List<int> data)
    {
        List<int> completedSquaresIndex = new List<int>();

        foreach (var squareIndex in data)
        {
            var squareCompleted = true;
            var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();

            if (comp.SquareOccupied == false)
            {
                squareCompleted = false;
            }

            if (squareCompleted)
            {
                completedSquaresIndex.Add(squareIndex);
            }
        }

        if (completedSquaresIndex.Count == data.Count)
        {
            foreach (var squareIndex in completedSquaresIndex)
            {

                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp.Deactivate();

                var comp2 = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp2.ClearOccupied();
            }

            if (TangramManager.instance.currentStage < 2)
            {
                TangramManager.instance.OpenStageCompletedScreen(); //show stage completion screen
                
            }
            else if (TangramManager.instance.currentStage == 2)
            {
                TangramManager.instance.OpenPuzzleCompletionScreen(); //show puzzle completion screen
            }
        }

    }

    //Function to reset grid
    public void ResetGrid()
    {
        for (int i = 0; i < _gridSquares.Count; i++)
        {
            var comp = _gridSquares[i].GetComponent<GridSquare>();
            comp.Deactivate();

            comp.ClearOccupied();
        }   
    }
}
