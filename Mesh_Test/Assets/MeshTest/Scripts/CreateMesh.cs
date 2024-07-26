using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    public bool Inverse;
    //public GameObject originObj;
    Vector3[] originVertices;
    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        GameObject createobj = new GameObject();

        MeshFilter objmesh = createobj.AddComponent<MeshFilter>();
        MeshRenderer renderer = createobj.AddComponent<MeshRenderer>();
        objmesh.mesh = Instantiate(meshFilter.mesh);
        renderer.material = GetComponent<MeshRenderer>().material;

       
                
        Vector3[] originVertices = meshFilter.mesh.vertices;
        Vector3[] vertices = new Vector3[originVertices.Length];

        for (int i =0; i < originVertices.Length; i++)
        {
            vertices[i] = transform.localToWorldMatrix.MultiplyPoint(originVertices[i]);
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] *= 4.0f;
        }

        //gameObject.transform.localScale = new Vector3(2)
        meshFilter.mesh.vertices = vertices;
        //gameObject.transform.localScale = new Vector3(4, 4, 4);
   
        for (int i = 0; i < originVertices.Length; i++)
        {
            vertices[i] = transform.localToWorldMatrix.inverse.MultiplyPoint(vertices[i]);
        }

        meshFilter.mesh.vertices = vertices;

    }

}
