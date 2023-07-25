using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayDisplay : MonoBehaviour
{
    public int[,] myArray = new int[2, 2] { { 1, 2 }, { 3, 4 } };

    public int[,] myArrayTwo = new int[2, 2]; 
    void Start()
    {

        for (int i = 0; i < myArray.GetLength(0); i++)
        {
            for (int j = 0; j < myArray.GetLength(1)-(myArray.GetLength(1)/2); j++)
            {
                int x = j;

                Debug.Log(myArray[i, j]+" "+ myArray[i, x+1]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
