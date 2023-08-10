using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

    public int stage = 0;

    //Arrays of shapes needed per stage
    private int[] firstStage = { 0, 3, 4, 5, 6, 7, 9, 10 };
    private int[] secondStage = { 0, 1, 2, 3, 3, 4, 5, 7, 8, 9, 10 };
    private int[] thirdStage = { 0, 1, 1, 2, 3, 5, 6, 7, 8, 8, 9 };
    private List<int[]> stagesShapes = new List<int[]>();

    void Start()
    {
        stagesShapes.Add(firstStage);
        stagesShapes.Add(secondStage);
        stagesShapes.Add(thirdStage);

        for (int i = 0; i < shapeList.Count; i++)
        {
            var shapeIndex = stagesShapes[stage][i];
            shapeList[i].CreateShape(shapeData[shapeIndex]);
        }
    }

    //Function to return current selected shape
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

    //Function to reset all shapes in stage
    public void ResetAllShapes()
    {
        foreach (var shape in shapeList)
        {
            if (shape.IsOnStartPosition() == false || shape.IsAnyOfShapeSquareActive() == false)
            {
                shape.ResetShape(); //activate shape, return shape to start position
            }   
        }
    }
}
