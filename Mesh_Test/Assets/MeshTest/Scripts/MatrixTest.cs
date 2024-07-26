using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    Vector3[] cubeOrigin;
    Vector3[] sphereOrigin;
    //private void Start()
    //{
    //    cubeOrigin = cube.GetComponent<MeshFilter>().mesh.vertices;
    //    sphereOrigin = sphere.GetComponent<MeshFilter>().mesh.vertices;

    //    Vector3[] tempCubeOrigin = new Vector3[cubeOrigin.Length];
    //    Vector3[] tempSphereOrigin = new Vector3[sphereOrigin.Length];
    //    for (int i =0; i<cubeOrigin.Length; i++)
    //    {
    //        tempCubeOrigin[i] = cube.transform.localToWorldMatrix.inverse.MultiplyPoint(cubeOrigin[i]);
    //    }
    //    for(int i = 0; i< sphereOrigin.Length; i++)
    //    {
    //        tempSphereOrigin[i] = sphere.transform.localToWorldMatrix.inverse.MultiplyPoint(sphereOrigin[i]);
    //    }

    //    cubeOrigin = tempCubeOrigin;
    //    sphereOrigin = tempSphereOrigin;

    //}
    private void Start()
    {
        Vector3[] cubeOrigin = cube.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] sphereOrigin = sphere.GetComponent<MeshFilter>().mesh.vertices;
        
        Vector3[] worldCubeVertices = new Vector3[cubeOrigin.Length];
        Vector3[] worldSphereVertices = new Vector3[sphereOrigin.Length];

        for (int i = 0; i < cubeOrigin.Length; i++)
        {
            worldCubeVertices[i] = cube.transform.localToWorldMatrix.MultiplyPoint(cubeOrigin[i]);
        }

        for (int i = 0; i < sphereOrigin.Length; i++)
        {
            worldSphereVertices[i] = sphere.transform.localToWorldMatrix.MultiplyPoint(sphereOrigin[i]);
        }
        for (int i = 0; i < worldCubeVertices.Length; i++)
        {
            worldCubeVertices[i] *= 2.0f;
        }

        for (int i = 0; i < sphereOrigin.Length; i++)
        {
            worldSphereVertices[i] /= 2.0f;
        }
        for (int i = 0; i < cubeOrigin.Length; i++)
        {
            worldCubeVertices[i] = cube.transform.localToWorldMatrix.inverse.MultiplyPoint(worldCubeVertices[i]);
        }

        for (int i = 0; i < sphereOrigin.Length; i++)
        {
            worldSphereVertices[i] = sphere.transform.localToWorldMatrix.inverse.MultiplyPoint(worldSphereVertices[i]);
        }
        Vector3 cubeCentroid = CalculateCentroid(worldCubeVertices);
        Vector3 sphereCentroid = CalculateCentroid(worldSphereVertices);

        Vector3 cubeOffset = -cubeCentroid;
        Vector3 sphereOffset = -sphereCentroid;

        cube.transform.position += cubeOffset;
        sphere.transform.position += sphereOffset;

        cube.GetComponent<MeshFilter>().mesh.vertices = worldCubeVertices;
        sphere.GetComponent<MeshFilter>().mesh.vertices = worldSphereVertices;
    }

    private Vector3 CalculateCentroid(Vector3[] vertices)
    {
        Vector3 centroid = Vector3.zero;
        for (int i = 0; i < vertices.Length; i++)
        {
            centroid += vertices[i];
        }
        centroid /= vertices.Length;
        return centroid;
    }
    private void Update()
    {
        Debug.DrawRay(Vector3.zero, Vector3.one, Color.red);
    }
}
