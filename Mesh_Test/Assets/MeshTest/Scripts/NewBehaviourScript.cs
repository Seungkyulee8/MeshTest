using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public MeshFilter meshFilter;
    
    int length;
    // Start is called before the first frame update
    void Start()
    {        
        length = meshFilter.mesh.vertices.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<length; i++)
        {
            Debug.DrawRay(meshFilter.mesh.vertices[i], meshFilter.mesh.normals[i], Color.red);                        
        }
 
    }
}
