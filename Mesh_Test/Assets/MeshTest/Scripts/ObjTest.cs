using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class ObjTest : MonoBehaviour
{
    public Material material;
    GameObject n = null;
    string filePath2 = "/Users/leeseunggyu/Downloads/cube2.obj";
    List<Vector3> tempVertices = new List<Vector3>();
    List<Vector2> tempUVs = new List<Vector2>();
    List<Vector3> tempNormals = new List<Vector3>();
    List<int> triangles = new List<int>();
    public Dictionary<HashClass, int> HashDict = new Dictionary<HashClass, int>();

    string filePath = "/Users/leeseunggyu/Downloads/model_mesh2407161405/model_mesh2407161405.obj";
    void Start()
    {
        string[] fileLines = File.ReadAllLines(filePath);
        int newIndex = 0;

        foreach (string line in fileLines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith("m") || line.StartsWith("o")) continue;
            string[] parts = line.Split(' ');
            switch (parts[0])
            {
                case "v":
                    Vector3 vertex = new Vector3(
                        float.Parse(parts[1]),
                        float.Parse(parts[2]),
                        float.Parse(parts[3]));
                    tempVertices.Add(vertex);
                    break;
                case "vt":
                    Vector2 uv = new Vector2(
                        float.Parse(parts[1]),
                        float.Parse(parts[2]));
                    tempUVs.Add(uv);
                    break;
                case "vn":
                    Vector3 normal = new Vector3(
                        float.Parse(parts[1]),
                        float.Parse(parts[2]),
                        float.Parse(parts[3]));
                    tempNormals.Add(normal);
                    break;
                case "f":
                    for (int i = 1; i <= 3; i++)
                    {
                        string[] index = parts[i].Split('/');
                        int vertexIndex = int.Parse(index[0]) - 1;
                        int uvIndex = int.Parse(index[1]) - 1;
                        int normalIndex = int.Parse(index[2]) - 1;

                        Vector3 V = tempVertices[vertexIndex];
                        Vector2 Vt = tempUVs[uvIndex];
                        Vector3 Vn = tempNormals[normalIndex];

                        HashClass hash = new HashClass(V, Vt, Vn);
                        if (!HashDict.ContainsKey(hash))
                        {
                            HashDict.Add(hash, newIndex);
                            newIndex++;
                        }
                        triangles.Add(HashDict[hash]);
                    }
                    break;

            }

        }
        List<Vector3> hashV = new List<Vector3>();
        List<Vector2> hashVt = new List<Vector2>();
        List<Vector3> hashVn = new List<Vector3>();

        foreach (HashClass hash in HashDict.Keys)
        {
            hashV.Add(hash.vertex);
            hashVt.Add(hash.uv);
            hashVn.Add(hash.normal);
        }
        Debug.Log("hashV.vertex.Count : " + hashV.Count);
        Debug.Log("hashVt.Count : " + hashVt.Count);
        Debug.Log("hashVn.Count : " + hashVn.Count);
        //Debug.Log("faceCount : " + faceCount);

        Debug.Log("HashDict.Count : " + HashDict.Count);
        Debug.Log("triangles.Count : " + triangles.Count);
        //Debug.Log("-------------------------------------------------------------");
        SetResult(hashV, hashVt, hashVn, triangles);
    }

    public void SetResult(List<Vector3> vertices, List<Vector2> uv, List<Vector3> normals, List<int> triangles)
    {
        int max = 256 * 256;
        List<Vector3> SetV = new List<Vector3>();
        List<Vector2> SetVt = new List<Vector2>();
        List<Vector3> SetVn = new List<Vector3>();
        List<int> triangles2 = new List<int>();
        Dictionary<int, int> list = new Dictionary<int, int>();
        int sum = 0;
        for (int i = 0; i < triangles.Count; i += 3)
        {
            for (int j = 0; j < 3; j++)
            {
                
                int trindex = triangles[i + j];
                if (!list.ContainsKey(trindex))
                {                    
                    list[trindex] = SetV.Count;
                    SetV.Add(vertices[trindex]);
                    SetVt.Add(uv[trindex]);
                    SetVn.Add(normals[trindex]);
  
                }
    

                triangles2.Add(list[trindex]);                
            }
            if (SetV.Count >= max - 3 || (triangles2.Count / 3) >= max || i == triangles.Count-3)
            {
                CreateMesh(SetV, SetVt, SetVn, triangles2);
                SetV.Clear();
                SetVt.Clear();
                SetVn.Clear();
                sum += triangles2.Count;
                triangles2.Clear();

            }
            
        }
        Debug.Log(triangles.Count);
        Debug.Log(sum);

    }

    public void CreateMesh(List<Vector3> Mesh_vertices, List<Vector2> Mesh_uv, List<Vector3> Mesh_normals, List<int> Mesh_triangles)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Mesh_vertices.ToArray();
        mesh.uv = Mesh_uv.ToArray();
        mesh.normals = Mesh_normals.ToArray();
        mesh.triangles = Mesh_triangles.ToArray();

        GameObject obj = new GameObject();
        obj.name = "Mesh";
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = material;

    }

}

