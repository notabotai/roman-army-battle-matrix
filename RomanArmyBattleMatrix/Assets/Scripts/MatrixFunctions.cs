using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixFunctions : MonoBehaviour
{
    public GameObject twoxone_matrix;
    public GameObject onextwo_matrix;
    public GameObject twoxtwo_matrix;

    public int[,] myArray = new int[2, 2] { { 1, 2 }, { 3, 4 } };
    public int[,] myArrayTwo = new int[2, 1] { { 2 }, { 4 } };

    public Transform ansLoc;

    public GameObject enemyMatrixRef;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int[,] Multiply(int[,] a, int[,] b)
    {
        if (a.GetLength(1) == b.GetLength(0))
        {
            int[,] result = new int[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = RowColumnMultiplier(a, b, i, j);
                }
            }

            ShowAnswerArray(a, b, result);

            return result;
        }
        else
        {
            Debug.Log("Matrices can't be multiplied.");

            return null;
        }
    }

    private int RowColumnMultiplier(int[,] a, int[,] b, int rowNumber, int columnNumber)
    {
        int sum = 0;

        for (int i = 0; i < a.GetLength(1); i++)
        {
            sum += a[rowNumber, i] * b[i, columnNumber];
        }

        Debug.Log("Sum is " + sum);

        return sum;
    }

    public int[,] Subtract(int[,] a, int[,] b)
    {
        if (a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1))
        {
            int[,] result = new int[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }

            return result;
        }
        else
        {
            return null;
        }
    }

    private void ShowAnswerArray(int[,] x, int[,] y, int[,] resultRef)
    {
        if (x.GetLength(0) == 2 && y.GetLength(1) == 1)
        {
            GameObject ans = Instantiate(twoxone_matrix, ansLoc);

            ans.GetComponent<MatrixIdentity>().Definition = resultRef;

            ans.GetComponent<MatrixIdentity>().isAttacker = true;

            ans.GetComponent<MatrixIdentity>().enemyMatrix = enemyMatrixRef;

            ans.GetComponent<MatrixIdentity>().solverRef = gameObject;

            ans.GetComponent<BoxCollider2D>().enabled = true;

           ans.GetComponent<MatrixIdentity>().AssignElementsRefresh();
        }

        if (x.GetLength(0) == 1 && y.GetLength(1) == 2)
        {
            GameObject ans = Instantiate(onextwo_matrix, ansLoc);

            ans.GetComponent<MatrixIdentity>().Definition = resultRef;

            ans.GetComponent<MatrixIdentity>().isAttacker = true;

            ans.GetComponent<MatrixIdentity>().enemyMatrix = enemyMatrixRef;

            ans.GetComponent<MatrixIdentity>().solverRef = gameObject;

            ans.GetComponent<BoxCollider2D>().enabled = true;

            ans.GetComponent<MatrixIdentity>().AssignElementsRefresh();
        }

        if (x.GetLength(0) == 2 && y.GetLength(1) == 2)
        {
            GameObject ans = Instantiate(twoxtwo_matrix, ansLoc.position, Quaternion.identity);

            ans.GetComponent<MatrixIdentity>().Definition = resultRef;

            ans.GetComponent<MatrixIdentity>().isAttacker = true;

            ans.GetComponent<MatrixIdentity>().enemyMatrix = enemyMatrixRef;

            ans.GetComponent<MatrixIdentity>().solverRef = gameObject;

            ans.GetComponent<BoxCollider2D>().enabled = true;

            ans.GetComponent<MatrixIdentity>().AssignElementsRefresh();
        }
    }
}
