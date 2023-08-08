using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrayDisplay : MonoBehaviour
{
    NewBehaviourScript x;

    public int[,] myArray = new int[2, 2] { { 1, 2 }, { 3, 4 } };


    public int[,] myArrayTwo = new int[2, 2] { { 2, 3 }, { 4, 5 } };


    public TextMeshPro One, Two, Three, Four; 

    public int topLeft;
    public int topRight;
    public int bottomLeft;
    public int bottomRight; 


    void Start()
    {

        for (int i = 0; i < myArray.GetLength(0); i++)
        {
            for (int j = 0; j < myArray.GetLength(1) - (myArray.GetLength(1) / 2); j++)
            {
                int x = j;

                Debug.Log(myArray[i, j] + " " + myArray[i, x + 1]);
            }
        }

        myArray[0,0] = myArray[0,0] * myArrayTwo[0,0];
        myArray[0, 1] = myArray[0, 1] * myArrayTwo[0, 1];
        myArray[1, 0] = myArray[1, 0] * myArrayTwo[1, 0];
        myArray[1, 1] = myArray[1, 1] * myArrayTwo[1, 1];

        One.text = myArray[0, 0].ToString();
        Two.text = myArray[0, 1].ToString();
        Three.text = myArray[1, 0].ToString();
        Four.text = myArray[1, 1].ToString();

    }

        // Update is called once per frame
        void Update()
        {
            topLeft = myArray[1,1];
            topRight = myArray[1,2];
            bottomLeft = myArray[2,1];
            bottomRight = myArray[2,2];
        }
}
