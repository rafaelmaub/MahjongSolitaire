using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLayoutData", menuName = "Custom/Layout Data")]
public class LayoutData : ScriptableObject
{
    [SerializeField] private Vector2Int layoutSize = new Vector2Int(3, 3); // Default layout size is 3x3
    [SerializeField] private bool[] matrix;

    public Vector2Int LayoutSize => layoutSize;

    public bool[] Matrix => matrix;

    public void ResizeMatrix()
    {
        matrix = new bool[layoutSize.x * layoutSize.y];
    }

    public bool GetMatrixElement(int row, int col)
    {
        int index = row * layoutSize.x + col;
        if (index >= 0 && index < matrix.Length)
        {
            return matrix[index];
        }
        return false;
    }

    public void SetMatrixElement(int row, int col, bool value)
    {
        int index = row * layoutSize.x + col;
        if (index >= 0 && index < matrix.Length)
        {
            matrix[index] = value;
        }
    }

    public static bool[,] ConvertTo2DArray(bool[] array, int width, int height)
    {
        bool[,] result = new bool[width, height];
        //Vector2Int origin = new Vector2Int(0, 0);

        // Iterate through the matrix
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                result[x, y] = array[(y * height) + x];
            }
        }

        return result;
    }
}