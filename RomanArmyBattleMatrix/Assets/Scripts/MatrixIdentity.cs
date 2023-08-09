using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MatrixIdentity : MonoBehaviour
{
    public List<int> elements = new List<int>();

    public int[,] Definition;

    public bool Declare;

    public bool isPowerUp;

    public bool isAttacker;

    public bool isEnemy;

    public int numberOfElements;

    public string dimensions;

    public string size;

    public GameObject playerArray;

    public GameObject solverRef;

    public GameObject enemyMatrixParent;

    public GameObject enemyMatrix;

    public GameObject skull;

    void Start()
    {
        if (isPowerUp)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            transform.GetChild(2).gameObject.SetActive(true);
        }

        #region Determining Size

        numberOfElements = transform.GetChild(1).childCount;

        switch (dimensions)
        {
            case "1x2":

                size = "1x2";

                Definition = new int[1, 2];

                break;

            case "2x1":

                size = "2x1";

                Definition = new int[2, 1];

                break;

            case "2x2":

                size = "2x2";

                Definition = new int[2, 2];

                break;

            default:

                size = string.Empty;
                break;
        }

        #endregion

        if (Declare)
        {
            AssignElementsPreDef();
        }

        //enemyMatrix = enemyMatrixParent.transform.GetChild(0).gameObject;
    }

    private void OnMouseDown()
    {
        if (isPowerUp)
        {
            solverRef.GetComponent<Animations>().result = solverRef.GetComponent<MatrixFunctions>().Multiply(Definition, playerArray.GetComponent<MatrixIdentity>().Definition, gameObject, playerArray);

            solverRef.GetComponent<Animations>().ResultCheck();

            //StartCoroutine(solverRef.GetComponent<Animations>().ExecuteAnimations(gameObject, playerArray));
        }

        if (isAttacker && enemyMatrix != null)
        {
            if (enemyMatrix.GetComponent<MatrixIdentity>().Definition.GetLength(0) == Definition.GetLength(0) && enemyMatrix.GetComponent<MatrixIdentity>().Definition.GetLength(1) == Definition.GetLength(1))
            {
                Definition = solverRef.GetComponent<MatrixFunctions>().Subtract(Definition, enemyMatrix.GetComponent<MatrixIdentity>().Definition);

                enemyMatrix.GetComponent<MatrixIdentity>().Die(enemyMatrix);

                Destroy(enemyMatrix,5);

                AssignElementsRefresh();
            }
        }
    }

    public void AssignElementsPreDef()
    {
        int counter = 0;

        for (int i = 0; i < Definition.GetLength(0); i++)
        {
            for (int j = 0; j < Definition.GetLength(1); j++)
            {
                Definition[i, j] = elements[counter];

                var textObj = transform.GetChild(1).GetChild(counter).gameObject;

                textObj.GetComponent<TextMeshPro>().text = Definition[i, j].ToString();

                counter++;
            }
        }
    }

    public void AssignElementsRefresh()
    {
        int counter = 0;

        for (int i = 0; i < Definition.GetLength(0); i++)
        {
            for (int j = 0; j < Definition.GetLength(1); j++)
            {
                //Definition[i, j] = elements[counter];

                var textObj = transform.GetChild(1).GetChild(counter).gameObject;

                textObj.GetComponent<TextMeshPro>().text = Definition[i, j].ToString();

                counter++;
            }
        }
    }

    public void ReDetermineSize()
    {
        switch (dimensions)
        {
            case "1x2":

                size = "1x2";

                Definition = new int[1, 2];

                break;

            case "2x1":

                size = "2x1";

                Definition = new int[2, 1];

                break;

            case "2x2":

                size = "2x2";

                Definition = new int[2, 2];

                break;

            default:

                size = string.Empty;
                break;
        }
    }

    public void Die(GameObject a)
    {
        try
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
        catch
        {
            Debug.Log("No rigidbody on object.");
        }

        var deathIcon = Instantiate(skull, transform.position, Quaternion.identity);

        deathIcon.transform.SetParent(gameObject.transform);

        for (int j = 0; j < a.transform.childCount-1; j++)
        {
            a.transform.GetChild(1).GetChild(j).gameObject.SetActive(false);
        }

            bool forceApplied = false;

        if(!forceApplied)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(1, 3), Random.Range(12, 14)), ForceMode2D.Impulse);

            gameObject.GetComponent<Rigidbody2D>().AddTorque(2, ForceMode2D.Impulse);

            forceApplied = true;
        }
    }
}
