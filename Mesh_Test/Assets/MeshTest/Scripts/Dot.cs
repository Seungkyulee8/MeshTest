using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriIndexInfo
{
    public int srcIdx = -1;
    public int tarIdx = -1;
    public int remainIdx = -1;
    public Vector3 centerPos = Vector3.zero;
    public TriIndexInfo(int cSi, int cTi, int cRi)
    {
        srcIdx = cSi;
        tarIdx = cTi;
        remainIdx = cRi;
    }
}

public class Dot : MonoBehaviour
{
    MeshFilter filter;
    public Texture2D texture;
    public string shaderName = "Unlit/Texture";

    [Range(0, 10)] public int iterations = 0;
    private int prevNum;
    private int currentIteration = 0;

    Vector3[] originVectors;
    Vector2[] originUVs;
    Vector3[] originNormals;
    int[] originTriangls;


    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find(shaderName));
        renderer.material.mainTexture = texture;

        originVectors = filter.mesh.vertices;
        originUVs = filter.mesh.uv;
        originNormals = filter.mesh.normals;
        originTriangls = filter.mesh.triangles;
        prevNum = iterations;
    }

    void Update()
    {
        if (prevNum != iterations)
        {
            Repeat(iterations);
            prevNum = iterations;
        }
        
    }
    public void ReturnOrigin()
    {
        filter.mesh = new Mesh();
        filter.mesh.vertices = originVectors;
        filter.mesh.uv = originUVs;
        filter.mesh.normals = originNormals;
        filter.mesh.triangles = originTriangls;
    }
    public void Repeat(int num)
    {
        ReturnOrigin();
        for (int i = 0; i < num; i++)
        {
            Expand(filter.mesh);

        }
    }

    public int[] calcTriIdxs(int s, int e, int re, int add)
    {
        return new int[]{
            s,
            add,
            re,
            add,
            e,
            re };
    }

    void Expand(Mesh mesh)
    {
        var vertices = new List<Vector3>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();
        var triangles = new List<int>();
      
        vertices.AddRange(mesh.vertices);
        uvs.AddRange(mesh.uv);
        normals.AddRange(mesh.normals);


        int[] refTriangles = mesh.triangles;
        int triCount = refTriangles.Length;

        TriIndexInfo[] tiis = new TriIndexInfo[3];
        tiis[0] = new TriIndexInfo(0, 1, 2);
        tiis[1] = new TriIndexInfo(1, 2, 0);
        tiis[2] = new TriIndexInfo(2, 0, 1);

        for (int i = 0; i < triCount; i+=3)
        {
            float maxDis = float.MinValue;
            TriIndexInfo maxTii = null;

            for (int j = 0; j < tiis.Length; j++)
            {
                int triIdxS = refTriangles[i + tiis[j].srcIdx];
                int triIdxE = refTriangles[i + tiis[j].tarIdx];


                float curDis = Vector3.Distance(vertices[triIdxS], vertices[triIdxE]);
                tiis[j].centerPos = (vertices[triIdxS] + vertices[triIdxE]) / 2.0f;

                if (maxDis < curDis)
                {
                    maxDis = curDis;
                    maxTii = tiis[j];
                }
            }

            vertices.Add(maxTii.centerPos);
            Vector2 newUV = (uvs[refTriangles[i + maxTii.srcIdx]] + uvs[refTriangles[i + maxTii.tarIdx]]) / 2f;
            Vector3 newNormal = (normals[refTriangles[i + maxTii.srcIdx]] + normals[refTriangles[i + maxTii.tarIdx]]).normalized;
            normals.Add(newNormal);
            uvs.Add(newUV);
            int[] newTri = calcTriIdxs(
                refTriangles[i + maxTii.srcIdx],
                refTriangles[i + maxTii.tarIdx],
                refTriangles[i + maxTii.remainIdx],
                vertices.Count - 1);
            triangles.AddRange(newTri);
        }

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    void debugVector(Vector3 curVec)
    {
        Debug.Log(curVec.x + " " + curVec.y + " " + curVec.z);
    }

    void debugVector(Vector2 curVec)
    {
        Debug.Log(curVec.x + " " + curVec.y);
    }
}
