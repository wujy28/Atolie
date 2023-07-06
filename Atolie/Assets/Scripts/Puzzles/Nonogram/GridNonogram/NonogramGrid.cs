using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();

    private NonogramAnswer answer;

    void Start()
    {
        answer = GetComponent<NonogramAnswer>();
        CreateGrid();
    }

    private void Update()
    {
        CheckIfCompleted();
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquarePositions();
    }

    private void SpawnGridSquares()
    {
        int squareIndex = 0;

        for (int row = 0; row < this.rows; ++row)
        {
            for (int col = 0; col < this.columns; ++col)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].GetComponent<NonogramGridSquare>().SquareIndex = squareIndex;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                squareIndex++;
            }
        }
    }

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

    void CheckIfCompleted()
    {
        List<bool> theAnswer = new List<bool>();

        List<bool[]> stageAnswers = new List<bool[]>();
        stageAnswers.Add(this.answer.firstStageAnswer);
        stageAnswers.Add(this.answer.secondStageAnswer);
        stageAnswers.Add(this.answer.thirdStageAnswer);

        foreach (var squareBool in stageAnswers[NonogramManager.instance.currentStage])
        {
            theAnswer.Add(squareBool);
        }

        CheckIfSquaresAreCompleted(theAnswer);
    }

    private void CheckIfSquaresAreCompleted(List<bool> data)
    {
        bool stageCompleted = true;

        for (int i = 0; i < _gridSquares.Count; i++)
        {
            var comp = _gridSquares[i].GetComponent<NonogramGridSquare>();

            if (comp.isColored != data[i])
            {
                stageCompleted = false;
                return;
            }
        }

        if (stageCompleted == true)
        {
            if (NonogramManager.instance.currentStage < 2)
            {
                NonogramManager.instance.OpenStageCompletedScreen(); //show stage completion screen

            }
            else if (NonogramManager.instance.currentStage == 2)
            {
                //shows up too fast?
                NonogramManager.instance.OpenPuzzleCompletionScreen(); //show puzzle completion screen
            }
        }
    }

    public void ResetGrid()
    {
        for (int i = 0; i < _gridSquares.Count; i++)
        {
            var comp = _gridSquares[i].GetComponent<NonogramGridSquare>();
            comp.Deselect();
        }   
    }
}
