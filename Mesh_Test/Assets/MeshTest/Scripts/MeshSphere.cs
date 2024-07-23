using UnityEngine;
using System.Collections.Generic;

public class MeshSphere : MonoBehaviour
{
    public Texture2D texture;
    [Min(3)] public int horizontal = 32;
    [Min(2)] public int vertical = 32;
    public string shaderName = "Unlit/Texture";

    [Header("Flip")]
    public bool flipU;
    public bool flipV;
    public bool flipTri;

    private void Awake()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find(shaderName));
        renderer.material.mainTexture = texture;

        Mesh mesh = new Mesh();

        /** Vertices & UV **/
        int vertLen = (horizontal + 1) * (vertical + 1);
        Vector3[] vertices = new Vector3[vertLen];
        Vector2[] uv = new Vector2[vertLen];

        for (int i = 0; i < vertical + 1; ++i)
        {
            float r = Mathf.Sin(Mathf.PI * i / vertical); //각 층의 반지름
            float h = Mathf.Cos(Mathf.PI * i / vertical); //각 층의 높이
            for (int j = 0; j < horizontal + 1; ++j)
            {
                Vector3 v3; //각 층을 회전하면서 정점 지정
                v3.x = Mathf.Sin(2f * Mathf.PI * j / horizontal) * r;
                v3.y = h;
                v3.z = Mathf.Cos(2f * Mathf.PI * j / horizontal) * r;
                vertices[i * (horizontal + 1) + j] = v3;

                Vector2 v2;
                v2.x = flipU ? ((float)j / horizontal) : (1f - (float)j / horizontal);
                v2.y = flipV ? (1f - (h + 1) * 0.5f) : ((h + 1) * 0.5f);
                uv[i * (horizontal + 1) + j] = v2;
            }
        }

        /** Triangles **/
        List<int> triangles = new List<int>();
        for (int i = 0; i < vertical; ++i)
        {
            int baseIdx = (horizontal + 1) * i;
            for (int j = 0; j < horizontal; ++j)
            {
                //각 층마다 두 개의 삼각형을 이용해 사각형을 구성
                if (flipTri)
                {
                    triangles.Add(baseIdx + j);
                    triangles.Add(baseIdx + j + 1);
                    triangles.Add(baseIdx + j + horizontal + 2);

                    triangles.Add(baseIdx + j + horizontal + 2);
                    triangles.Add(baseIdx + j + horizontal + 1);
                    triangles.Add(baseIdx + j);
                }
                else
                {
                    triangles.Add(baseIdx + j);
                    triangles.Add(baseIdx + j + horizontal + 1);
                    triangles.Add(baseIdx + j + horizontal + 2);

                    triangles.Add(baseIdx + j + horizontal + 2);
                    triangles.Add(baseIdx + j + 1);
                    triangles.Add(baseIdx + j);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        filter.mesh = mesh;
    }
}