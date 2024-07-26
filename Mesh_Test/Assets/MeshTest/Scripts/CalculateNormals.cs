using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class FindCrossVector3
{
    Vector3 mainVec;
    Vector3 other1Vec;
    Vector3 other2Vec;

    public FindCrossVector3(Vector3 main, Vector3 other1, Vector3 other2)
    {
        mainVec = main;
        other1Vec = other1;
        other2Vec = other2;
        
    }
    public (Vector3, Vector3) CalculateCross(bool setNormal = false)
    {
        Vector3 mainOther1 = other1Vec - mainVec;
        Vector3 mainOther2 = other2Vec - mainVec;
        Vector3 normal = setNormal ? Vector3.Cross(mainOther1.normalized, mainOther2.normalized).normalized
            : Vector3.Cross(mainOther1, mainOther2);        
        return (mainVec, normal);
    }
    public void PrintDebug()
    {
        //Debug.Log("mainVec : " + mainVec.ToString());
        //Debug.Log("other1Vec : " + other1Vec.ToString());
        //Debug.Log("other2Vec : " + other2Vec.ToString());
        //Debug.Log("------------------------------------------------------------------------------------------------");
        Debug.DrawLine(mainVec, other1Vec, Color.yellow);
        Debug.DrawLine(mainVec, other2Vec, Color.green);
    }
}
public class CalculateNormals : MonoBehaviour
{
    public bool setNormal = false;
    public MeshFilter meshFilter;
    public int number = 0;
    int verticeCount = 0;
    bool isPrintDebug = true;
    //Vector3 sum;
    Vector3 origin;
    List<FindCrossVector3> lists = new List<FindCrossVector3>();
    FindCrossVector3 check;
    private void Start()
    {
        CalculateNormal(number);
    }
    public void CalculateNormal(int num)
    {
        if (num < 0 || num >= meshFilter.mesh.vertexCount)
        {
            return;
        }

        lists.Clear();
        //sum = Vector3.zero;
        var vertices = new List<Vector3>();
        vertices.AddRange(meshFilter.mesh.vertices);
        int[] Triangles = meshFilter.mesh.triangles;
        verticeCount = vertices.Count;
        Vector3 targetVertex = vertices[num];

        int remain = num % 3;
        int curNum = (num / 3) * 3;

        //Debug.Log(curNum + " " + remain);

        //Vector3 p0 = vertices[Triangles[curNum + 0]];
        //Vector3 p1 = vertices[Triangles[curNum + 1]];
        //Vector3 p2 = vertices[Triangles[curNum + 2]];

        //if (remain == 0)
        //{
        //    FindCrossVector3 find = new FindCrossVector3(p0, p1, p2);
        //    lists.Add(find);
        //}
        //else if (remain == 1)
        //{
        //    FindCrossVector3 find = new FindCrossVector3(p1, p2, p0);
        //    lists.Add(find);
        //}
        //else if (remain == 2)
        //{
        //    FindCrossVector3 find = new FindCrossVector3(p2, p0, p1);
        //    lists.Add(find);
        //}
        //check = new FindCrossVector3(p0, p1, p2);
        for (int i = 0; i < Triangles.Length; i += 3)
        {


            for (int j = 0; j < 3; j++)
            {
                if (vertices[Triangles[i + j]] == targetVertex)
                {
                    if (j == 0)
                    {
                        FindCrossVector3 find = new FindCrossVector3(vertices[Triangles[i + j]], vertices[Triangles[i + (j + 1)]], vertices[Triangles[i + (j + 2)]]);
                        lists.Add(find);
                    }
                    else if (j == 1)
                    {
                        FindCrossVector3 find = new FindCrossVector3(vertices[Triangles[i + j]], vertices[Triangles[i + (j + 1)]], vertices[Triangles[i + (j - 1)]]);
                        lists.Add(find);
                    }
                    else if (j == 2)
                    {
                        FindCrossVector3 find = new FindCrossVector3(vertices[Triangles[i + j]], vertices[Triangles[i + (j - 2)]], vertices[Triangles[i + (j - 1)]]);
                        lists.Add(find);
                    }
                    break;
                }
            }

        }

    }
    public void ShowRay()
    {
        //var crossResult = check.CalculateCross();
        //Debug.DrawRay(crossResult.Item1, crossResult.Item2, Color.blue);
        //check.PrintDebug();
        Vector3 sum = Vector3.zero;
        foreach (FindCrossVector3 a in lists)
        {
            var crossResult = a.CalculateCross(setNormal);
            if (origin != crossResult.Item1)
            {
                origin = crossResult.Item1;
            }
            sum = sum + crossResult.Item2;
            Vector3 dir = crossResult.Item2;
            Debug.DrawRay(origin, dir, Color.blue);
            a.PrintDebug();
        }

        Debug.DrawRay(origin, sum, Color.red);
        if (isPrintDebug)
        {
            //Debug.Log("------------------------------------------------------------------------------------------------");
            foreach (FindCrossVector3 b in lists)
            {
                b.PrintDebug();
            }
            //Debug.Log("Sum of normals (normalized): " + sum.normalized);


            isPrintDebug = false;
        }


    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //            isPrintDebug = true;

    //            CalculateNormal();


    //    }
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {

    //            isPrintDebug = true;

    //            CalculateNormal();

    //    }
    //    ShowRay();

    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (number > 0)
            {
                isPrintDebug = true;
                number--;
                CalculateNormal(number);
            }


        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (number < verticeCount)
            {
                isPrintDebug = true;
                number++;
                CalculateNormal(number);
            }
        }
        ShowRay();

    }
}
