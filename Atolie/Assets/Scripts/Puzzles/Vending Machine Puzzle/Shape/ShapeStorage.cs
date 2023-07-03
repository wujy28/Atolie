using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    //public SquareTextureData squareTextureData; //testing

    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

    public int stage = 0;

    private int[] firstStage = { 0, 3, 4, 5, 6, 7, 9, 10 };
    private int[] secondStage = { 0, 1, 2, 3, 3, 4, 5, 7, 8, 9, 10 };
    private int[] thirdStage = { 0, 1, 1, 2, 3, 5, 6, 7, 8, 8, 9 };
    private List<int[]> stagesShapes = new List<int[]>();

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (var shape in shapeList)
        {
            var shapeIndex = firstStage[i]; //determine which shape we get
            shape.CreateShape(shapeData[shapeIndex]);
        }*/
        stagesShapes.Add(firstStage);
        stagesShapes.Add(secondStage);
        stagesShapes.Add(thirdStage);

        for (int i = 0; i < shapeList.Count; i++)
        {
            var shapeIndex = stagesShapes[stage][i];
            shapeList[i].CreateShape(shapeData[shapeIndex]);
        }

        //squareTextureData.SetStartColor(); //testing
    }

    public Shape GetCurrentSelectedShape()
    {
        foreach (var shape in shapeList)
        {
            if (shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
            {
                return shape;
            }
        }

        Debug.LogError("There is no shape selected");
        return null;
    }

    public void ResetAllShapes()
    {
        foreach (var shape in shapeList)
        {
            if (shape.IsOnStartPosition() == false || shape.IsAnyOfShapeSquareActive() == false)
            {
                shape.ResetShape(); //activate shape, return shape to startPos
            }   
        }
    }

    /*private void UpdateSquareColor()
    {
        int squareIndex = 0;

        if (squareIndex >= squareTextureData.tresholdVal)
        {
            squareTextureData.UpdateColors(squareIndex);
        }
    }*/
}
