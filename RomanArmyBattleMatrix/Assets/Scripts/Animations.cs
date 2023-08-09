using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Animations : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject twoxOneMatrix;
    public GameObject twoxTwoMatrix;

    public GameObject calcZoneRef;

    public GameObject testReference, testReference2;

    public List<GameObject> symbols = new List<GameObject>();

    public float adjust_x;
    public float adjust_y;
    public float offset;

    public float adjust_x_ex;
    public float adjust_y_ex;
    public float offset_ex;

    public int[,] result;

    public GameObject animationResidue;

    public GameObject barracks;

    public GameObject battlefield;

    void Start()
    {
        //StartCoroutine(ExecuteAnimations());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ExplodeMatrix(GameObject matrix)
    {
        List<GameObject> children = new List<GameObject>();

        bool change_Orientation = true;

        Vector3 finalPos = calcZoneRef.transform.position;

        finalPos.x = calcZoneRef.transform.position.x - adjust_x_ex - offset_ex;
        finalPos.y = adjust_y_ex;

        int counter = 1;

        for (int i = 0; i < matrix.transform.GetChild(1).childCount; i++)
        {
            Debug.Log("i = " + i);
            {


                children.Add(matrix.transform.GetChild(1).GetChild(i).gameObject);

                matrix.transform.GetChild(1).GetChild(i).transform.DOMove(finalPos, 1);

                finalPos.x += adjust_x_ex * 2;

                counter++;

                if (counter > matrix.transform.childCount / 2 && change_Orientation)
                {
                    finalPos.x = calcZoneRef.transform.position.x - adjust_x - offset_ex;

                    finalPos.y -= adjust_y_ex * 2;

                    change_Orientation = false;
                }

                matrix.transform.GetChild(1).GetChild(i).SetParent(animationResidue.transform);

                i = i - 1;
            }
        }

    }

    void GenerateParts(GameObject matrix)
    {
        #region Code (Too long)

        Debug.Log("Running func - Generate Parts.");

        matrix.GetComponent<MatrixIdentity>().ReDetermineSize();

        matrix.GetComponent<MatrixIdentity>().AssignElementsPreDef();

        if (matrix.GetComponent<MatrixIdentity>().dimensions == "2x2")
        {
            Debug.Log("Running func - Generate Parts.");

            List<GameObject> parts = new List<GameObject>();

            parts.Add(twoxOneMatrix);
            parts.Add(twoxOneMatrix);

            int pos = -1;
            Vector3 finalPos = calcZoneRef.transform.position;

            finalPos.x = calcZoneRef.transform.position.x - adjust_x - offset;
            finalPos.y = adjust_y;

            int matrixColumnNumber = 0;

            for (int j = 0; j < 2; j++)
            {
                foreach (GameObject part in parts)
                {
                    part.GetComponent<MatrixIdentity>().dimensions = "2x1";

                    part.GetComponent<MatrixIdentity>().ReDetermineSize();

                    part.GetComponent<MatrixIdentity>().AssignElementsPreDef();

                    GameObject a = Instantiate(part, new Vector3(matrix.transform.position.x + pos, matrix.transform.position.y, matrix.transform.position.z), Quaternion.identity);
                    pos *= -1;

                    a.transform.SetParent(animationResidue.transform);

                    a.GetComponent<MatrixIdentity>().dimensions = "2x1";

                    a.GetComponent<MatrixIdentity>().ReDetermineSize();

                    a.GetComponent<MatrixIdentity>().AssignElementsPreDef();

                    Debug.Log("Length of a is " + a.GetComponent<MatrixIdentity>().Definition.GetLength(0));

                    for (int i = 0; i < a.GetComponent<MatrixIdentity>().Definition.GetLength(0); i++)
                    {
                        Debug.Log("i = " + i + " column = " + matrixColumnNumber);

                        Debug.Log(a.GetComponent<MatrixIdentity>().Definition.GetLength(0) + " " + a.GetComponent<MatrixIdentity>().Definition.GetLength(1));

                        Debug.Log(matrix.GetComponent<MatrixIdentity>().Definition.GetLength(0) + " " + matrix.GetComponent<MatrixIdentity>().Definition.GetLength(1));

                        a.GetComponent<MatrixIdentity>().Definition[i, 0] = matrix.GetComponent<MatrixIdentity>().Definition[i, matrixColumnNumber];
                    }

                    a.GetComponent<MatrixIdentity>().AssignElementsRefresh();

                    part.GetComponent<MatrixIdentity>().AssignElementsPreDef();

                    a.transform.DOMove(finalPos, 1);

                    finalPos.x += adjust_x * 2;

                    matrixColumnNumber++;

                    Debug.Log("Creating Parts.");
                }

                DisplaySymbols(new Vector3(calcZoneRef.transform.position.x, finalPos.y, calcZoneRef.transform.position.z));

                matrixColumnNumber = 0;

                finalPos.x = calcZoneRef.transform.position.x - adjust_x - offset;

                finalPos.y -= adjust_y * 2;
            }

        }

        #endregion
    }

    void DisplaySymbols(Vector3 pos)
    {

        GameObject t = Instantiate(symbols[0], pos, Quaternion.identity);

        t.transform.SetParent(animationResidue.transform);

        t.GetComponent<TextMeshPro>().DOFade(1, 2);
    }

    public void ResultCheck()
    {
        for(int i = 0; i<2; i++)
        {
            for(int j=0; j<2; j++)
            {
                Debug.Log(result[i, j]);
            }
        }
    }

    public void CreateResultantColumns(int[,] a)
    {
        if(a.GetLength(0) == 2 && a.GetLength(1) == 2)
        {
            Vector3 finalPos = calcZoneRef.transform.position;

            int offset = -1;

            finalPos.y = adjust_y_ex;

            int columnPos = 0;

            for (int k=0; k<2; k++)
            {
                GameObject x = Instantiate(twoxOneMatrix, finalPos, Quaternion.identity);

                x.transform.SetParent(animationResidue.transform);

                x.transform.GetChild(0).gameObject.SetActive(false);

                x.GetComponent<MatrixIdentity>().dimensions = "2x1";

                x.GetComponent<MatrixIdentity>().ReDetermineSize();

                x.GetComponent<MatrixIdentity>().AssignElementsPreDef();

                for (int l = 0; l < 2; l++)
                {

                    x.GetComponent<MatrixIdentity>().Definition[l, 0] = a[l, columnPos];
                }

                x.GetComponent<MatrixIdentity>().AssignElementsRefresh();

                x.transform.DOMove((new Vector3(calcZoneRef.transform.position.x + offset, calcZoneRef.transform.position.y, calcZoneRef.transform.position.z)), 1);
                
                columnPos++;

                offset += 2;

                finalPos.y -= 2 * adjust_x_ex;
            }
        }
    }

    public void displayResult_in_Anim()
    {
                GameObject ans = Instantiate(GetComponent<MatrixFunctions>().twoxtwo_matrix, GetComponent<MatrixFunctions>().ansLoc);

                ans.GetComponent<MatrixIdentity>().Definition = result;

                ans.GetComponent<MatrixIdentity>().isAttacker = true;

                ans.GetComponent<MatrixIdentity>().enemyMatrix = GetComponent<MatrixFunctions>().enemyMatrixRef;

                ans.GetComponent<MatrixIdentity>().solverRef = gameObject;

                ans.GetComponent<BoxCollider2D>().enabled = true;

                ans.GetComponent<MatrixIdentity>().AssignElementsRefresh();

                ans.transform.SetParent(battlefield.transform);
    }

    public IEnumerator ExecuteAnimations(GameObject powerUp, GameObject playerMatrix)
    {
        GenerateParts(powerUp);

        yield return new WaitForSeconds(2);

        ExplodeMatrix(playerMatrix);

        yield return new WaitForSeconds(2);

        foreach (Transform child in animationResidue.transform)
        {
            Destroy(child.gameObject);
        }

        CreateResultantColumns(result);


        yield return new WaitForSeconds(1);

        foreach (Transform child in animationResidue.transform)
        {
            Destroy(child.gameObject);
        }

        displayResult_in_Anim();

        barracks.transform.DOMoveX(-50, 2);
        battlefield.transform.DOMoveX(0, 2);

        yield return null;
    }
}
