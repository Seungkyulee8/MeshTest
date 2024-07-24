using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriIndexInfo
{
    public TriIndexInfo()
    {
        ;
    }

    public TriIndexInfo(int cSi, int cTi, int cRi)
    {
        srcIdx = cSi;
        tarIdx = cTi;
        remainIdx = cRi;
    }

    //public int[] calcTriangles(int baseIdx, int addIdx)
    //{
    //    return new int []{
    //        baseIdx + srcIdx,
    //        addIdx,
    //        baseIdx + remainIdx,
    //        addIdx,
    //        baseIdx + tarIdx,
    //        baseIdx + remainIdx };
    //}

    public int srcIdx = -1;
    public int tarIdx = -1;
    public int remainIdx = -1;
    public Vector3 centerPos = Vector3.zero;
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

    private void Start()
    {
        //for (int i = 0; i < 1; i++)
        //{
        //    Expand(filter.mesh);
        //}        
    }
    void Expand(Mesh mesh)
    {
        //Debug.Log("mesh.uv.Length : " + mesh.uv.Length);
        //foreach (Vector2 vec in mesh.uv)
        //{
        //    Debug.Log(vec);
        //}
        //Debug.Log("----------------------------");

        //Debug.Log("mesh.normals.Length : " + mesh.normals.Length);
        //foreach (Vector3 vec in mesh.normals)
        //{
        //    Debug.Log(vec);
        //}
        //Debug.Log("----------------------------");
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
            //foreach (Vector3 num in normals)
            //{
            //    debugVector(num);
            //}
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
        //foreach (Vector3 num in normals)
        //{
        //    debugVector(num);
        //}
        //for (int i = 0; i < mesh.triangles.Length; i += 3)
        //{
        //    Vector3 v0 = vertices[mesh.triangles[i]];
        //    Vector3 v1 = vertices[mesh.triangles[i + 1]];
        //    Vector3 v2 = vertices[mesh.triangles[i + 2]];

        //    Vector3 center = Vector3.zero;
        //    Vector3 remain = Vector3.zero;
        //    float distance01 = Vector3.Distance(v0, v1);
        //    float distance12 = Vector3.Distance(v1, v2);
        //    float distance20 = Vector3.Distance(v2, v0);
        //    int centerIndex = 0;
        //    if (distance01 > distance12 && distance01 > distance20)
        //    {
        //        center = (v0 + v1) / 2;

        //        vertices.Add(center);
        //        //int centerIndex = vertices.Count - 1;

        //        //triangles.Add(mesh.triangles[i]);
        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i + 2]);

        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i + 1]);
        //        //triangles.Add(mesh.triangles[i + 2]);
        //    }
        //    else if (distance12 > distance01 && distance12 > distance20)
        //    {
        //        center = (v1 + v2) / 2;
        //        vertices.Add(center);
        //       // int centerIndex = vertices.Count - 1;

        //        //triangles.Add(mesh.triangles[i + 1]);
        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i]);

        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i + 2]);
        //        //triangles.Add(mesh.triangles[i]);
        //    }
        //    else if (distance20 > distance12 && distance20 > distance01)
        //    {
        //        center = (v2 + v0) / 2;
        //        vertices.Add(center);
        //        //int centerIndex = vertices.Count - 1;

        //        //triangles.Add(mesh.triangles[i + 2]);
        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i + 1]);

        //        //triangles.Add(centerIndex);
        //        //triangles.Add(mesh.triangles[i]);
        //        ////triangles.Add(mesh.triangles[i + 1]);
        //    }
        //    centerIndex = vertices.Count - 1;

        //    triangles.Add(centerIndex);
        //    triangles.Add(mesh.triangles[i]);
        //    triangles.Add(mesh.triangles[i + 1]);

        //    triangles.Add(centerIndex);
        //    triangles.Add(mesh.triangles[i + 1]);
        //    triangles.Add(mesh.triangles[i + 2]);

        //}



        //mesh.vertices = vertices.ToArray();
        //mesh.triangles = triangles.ToArray();
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
