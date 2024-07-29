using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    Vector3[] cubeOriginVertices;
    Vector3[] sphereOriginVertices;

    Vector3[] cubeNormals;
    Vector3[] sphereNormals;

    Vector3[] worldCubeVertices;
    Vector3[] worldSphereVertices;

    Vector3[] worldCubeNormals;
    Vector3[] worldSphereNormals;
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
        cubeOriginVertices = cube.GetComponent<MeshFilter>().mesh.vertices;
        sphereOriginVertices = sphere.GetComponent<MeshFilter>().mesh.vertices;


        cubeNormals = cube.GetComponent<MeshFilter>().mesh.normals;        
        sphereNormals = sphere.GetComponent<MeshFilter>().mesh.normals;

        worldCubeVertices = new Vector3[cubeOriginVertices.Length];
        worldSphereVertices = new Vector3[sphereOriginVertices.Length];

        worldCubeNormals = new Vector3[cubeNormals.Length];
        worldSphereNormals = new Vector3[sphereNormals.Length];

        for (int i = 0; i < cubeOriginVertices.Length; i++)
        {
            worldCubeVertices[i] = cube.transform.localToWorldMatrix.MultiplyPoint(cubeOriginVertices[i]);
        }

        for (int i = 0; i < cubeNormals.Length; i++)
        {
            worldCubeNormals[i] = cube.transform.localToWorldMatrix.MultiplyVector(cubeNormals[i]);
        }
        for (int i = 0; i < sphereNormals.Length; i++)
        {
            worldSphereNormals[i] = sphere.transform.localToWorldMatrix.MultiplyVector(sphereNormals[i]);
        }

        for (int i = 0; i < sphereOriginVertices.Length; i++)
        {
            worldSphereVertices[i] = sphere.transform.localToWorldMatrix.MultiplyPoint(sphereOriginVertices[i]);
        }
        for (int i = 0; i < worldCubeVertices.Length; i++)
        {
            worldCubeVertices[i] *= 2.0f;
        }

        for (int i = 0; i < sphereOriginVertices.Length; i++)
        {
            worldSphereVertices[i] /= 2.0f;
        }
        for (int i = 0; i < cubeOriginVertices.Length; i++)
        {
            worldCubeVertices[i] = cube.transform.localToWorldMatrix.inverse.MultiplyPoint(worldCubeVertices[i]);
        }

        for (int i = 0; i < sphereOriginVertices.Length; i++)
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
        // 큐브의 정점(Vertex)들과 노멀(Normal)들을 순회하며 레이를 그립니다.
        for (int i = 0; i < worldCubeVertices.Length; i++)
        {
            Vector3 worldVertex = cube.transform.localToWorldMatrix.MultiplyPoint(worldCubeVertices[i]);
            Vector3 worldNormal = cube.transform.localToWorldMatrix.MultiplyVector(worldCubeNormals[i]);
            Debug.DrawRay(worldVertex, worldNormal, Color.red);
        }

        // 구의 정점(Vertex)들과 노멀(Normal)들을 순회하며 레이를 그립니다.
        for (int i = 0; i < worldSphereVertices.Length; i++)
        {
            Vector3 worldVertex = sphere.transform.localToWorldMatrix.MultiplyPoint(worldSphereVertices[i]);
            Vector3 worldNormal = sphere.transform.localToWorldMatrix.MultiplyVector(worldSphereNormals[i]);
            Debug.DrawRay(worldVertex, worldNormal, Color.blue);
        }
    }
}
