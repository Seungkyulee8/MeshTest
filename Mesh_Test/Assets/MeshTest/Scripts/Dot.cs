using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    MeshFilter filter;
    
    public Texture2D texture;
    public string shaderName = "Unlit/Texture";
    public Vector3[] Vertices;
    public int[] Triangles;
    public int index = 1;
    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find(shaderName));
        renderer.material.mainTexture = texture;
    }
    void Start()
    {
        LongDistance(filter.mesh);
    }
    void LongDistance(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        List<Vector3> longestEdges = new List<Vector3>();

        for (int i = 0; i < triangles.Length; i += 3)
        {            
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            Vector3 center = new Vector3(0,0,0);
            float distance01 = Vector3.Distance(v0, v1);
            float distance12 = Vector3.Distance(v1, v2);
            float distance20 = Vector3.Distance(v2, v0);
            int total = vertices.Length+1;
            if(distance01 > distance12 && distance01 > distance20)
            {
                center = (v0 + v1) / 2;
                //vertices.
                int[] a = new int[]
                {
                    triangles[i], triangles[vertices.Length], triangles[i+2],
                    triangles[i+2], triangles[vertices.Length], triangles[i+1]
                };
            }
            if (distance12 > distance01 && distance12 > distance20)
            {
                center = (v1 + v2) / 2;
                for (int k = 0; k < total; k++)
                {
                    if (k == total - 1)
                    {
                        vertices[k] = center;
                    }
                }
                int[] a = new int[]
                {
                    triangles[i+1], triangles[vertices.Length],triangles[i],
                    triangles[i], triangles[vertices.Length], triangles[i+2]
                };
            }
            if (distance20 > distance12 && distance20 > distance01)
            {
                center = (v2 + v0) / 2;
                for (int k = 0; k < total; k++)
                {
                    if (k == total - 1)
                    {
                        vertices[k] = center;
                    }
                }
                int[] a = new int[]
                {
                    triangles[i+2], triangles[vertices.Length],triangles[i+1],
                    triangles[vertices.Length], triangles[i], triangles[i+1]
                };
            }

        }
    }
    void AAA(List<Vector3> list, int[] eTriangles)
    {
        Vector3 newVertex = (list[0] + list[1]) / 2;

        for(int i = 0; i<eTriangles.Length; i++)
        {
            Vertices[eTriangles[i]] = list[i]; 
        }
        //Vector3[] newVectices = new Vector3[]
        //{
        //    //list[0],
        //    //list[1],
        //    //list[2],
        //    //newVertex,
        //};
        int[] triangles = new int[]
        {
            //eTriangles[0],me,eTriangles[2],
            eTriangles[1],eTriangles[2],
        };

      

    }

}
