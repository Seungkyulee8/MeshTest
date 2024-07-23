using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Texture2D texture;
    public string shaderName = "Standard";

    private void Awake()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find(shaderName));
        renderer.material.mainTexture = texture;

        Mesh mesh = new Mesh();

        /** Vertices **/
        Vector3[] vertices = new Vector3[]
        {
            
        };

        /** UV **/
        Vector2[] uv = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
        };

        /** Triangles **/
        int[] triangles = new int[]
        {
            0, 1, 2,
            3, 0, 2,

            4, 5, 6,
            7, 4, 6,

            8, 9, 10,
            11, 8, 10,

            12, 13, 14,
            15, 12, 14,

            16, 17, 18,
            19, 16, 18,

            20, 21, 22,
            23, 20, 22,
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        filter.mesh = mesh;

        // 노말 벡터를 출력합니다.
        foreach (Vector3 isnormal in mesh.normals)
        {
            Debug.Log(isnormal);
        }
    }

}
