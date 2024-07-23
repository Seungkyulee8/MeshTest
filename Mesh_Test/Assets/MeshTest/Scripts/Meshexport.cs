using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Meshexport : MonoBehaviour
{
    public void ExportMeshToObjAsync(Mesh mesh, string filePath)
    {
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Dictionary<Vector3, int> vertexList = new Dictionary<Vector3, int>();
        Dictionary<Vector2, int> uvList = new Dictionary<Vector2, int>();
        Dictionary<Vector3, int> normalList = new Dictionary<Vector3, int>();
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            // 정점 데이터 기록
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = vertices[i];
                if (!vertexList.ContainsKey(vertex))
                {
                    vertexList.Add(vertex, vertexList.Count + 1);
                    sw.WriteLine($"v {vertex.x} {vertex.y} {vertex.z}");                    
                }
            }
            // UV 데이터 기록
            for (int i = 0; i < uv.Length; i++)
            {
                Vector2 uvCoord = uv[i];
                if (!uvList.ContainsKey(uvCoord))
                {
                    uvList.Add(uvCoord, uvList.Count + 1);
                    sw.WriteLine($"vt {uvCoord.x} {uvCoord.y}");                    
                }
            }
            // 노멀 데이터 기록
            for (int i = 0; i < normals.Length; i++)
            {
                Vector3 normal = normals[i];
                if (!normalList.ContainsKey(normal))
                {
                    normalList.Add(normal, normalList.Count + 1);
                    sw.WriteLine($"vn {normal.x} {normal.y} {normal.z}");                    
                }
            }
            // face 데이터 기록
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int v1 = vertexList[vertices[triangles[i]]];
                int v2 = vertexList[vertices[triangles[i + 1]]];
                int v3 = vertexList[vertices[triangles[i + 2]]];

                int vt1 = uvList[uv[triangles[i]]];
                int vt2 = uvList[uv[triangles[i + 1]]];
                int vt3 = uvList[uv[triangles[i + 2]]];

                int vn1 = normalList[normals[triangles[i]]];
                int vn2 = normalList[normals[triangles[i + 1]]];
                int vn3 = normalList[normals[triangles[i + 2]]];

                sw.WriteLine($"f {v1}/{vt1}/{vn1} {v2}/{vt2}/{vn2} {v3}/{vt3}/{vn3}");                
            }
        }
    }
}
