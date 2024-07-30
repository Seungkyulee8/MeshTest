using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    public bool StartReverse;
    public MeshCollider meshCollider;
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

    Mesh cubeMesh;
    Mesh sphereMesh;
    Vector3[] origingNormals;
    Vector3[] reverseNormals;
    int[] origin;
    int[] reverse;

    private void Start()
    {
        cubeMesh = cube.GetComponent<MeshFilter>().mesh;
        sphereMesh = sphere.GetComponent<MeshFilter>().mesh;

        cubeOriginVertices = cubeMesh.vertices;
        sphereOriginVertices = sphereMesh.vertices;

        meshCollider = cube.GetComponent<MeshCollider>();

        cubeNormals = cubeMesh.normals;
        sphereNormals = sphereMesh.normals;

        worldCubeVertices = new Vector3[cubeOriginVertices.Length];
        worldSphereVertices = new Vector3[sphereOriginVertices.Length];

        worldCubeNormals = new Vector3[cubeNormals.Length];
        worldSphereNormals = new Vector3[sphereNormals.Length];

        origingNormals = cubeMesh.normals;
        reverseNormals = origingNormals;
        origin = cubeMesh.triangles;
        reverse = origin;


        //for (int i = 0; i < cubeOriginVertices.Length; i++)
        //{
        //    worldCubeVertices[i] = cube.transform.localToWorldMatrix.MultiplyPoint(cubeOriginVertices[i]);
        //}

        //for (int i = 0; i < cubeNormals.Length; i++)
        //{
        //    worldCubeNormals[i] = cube.transform.localToWorldMatrix.MultiplyVector(cubeNormals[i]);
        //}
        for (int i = 0; i < sphereNormals.Length; i++)
        {
            worldSphereNormals[i] = sphere.transform.localToWorldMatrix.MultiplyVector(sphereNormals[i]);
        }

        for (int i = 0; i < sphereOriginVertices.Length; i++)
        {
            worldSphereVertices[i] = sphere.transform.localToWorldMatrix.MultiplyPoint(sphereOriginVertices[i]);
        }
        //for (int i = 0; i < worldCubeVertices.Length; i++)
        //{
        //    worldCubeVertices[i] *= 2.0f;
        //}
        //for (int i = 0; i < cubeOriginVertices.Length; i++)
        //{
        //    worldCubeVertices[i] = cube.transform.localToWorldMatrix.inverse.MultiplyPoint(worldCubeVertices[i]);
        //}
        //GameObject obj = new GameObject("obj1");

        //MeshFilter a = obj.AddComponent<MeshFilter>();
        //a.mesh = cubeMesh;
        //a.mesh.vertices = worldCubeVertices;
        ////a.mesh.normals = worldCubeNormals;
        //MeshRenderer b = obj.AddComponent<MeshRenderer>();
        //b.material = cube.GetComponent<MeshRenderer>().material;




        //for (int i = 0; i < sphereOriginVertices.Length; i++)
        //{
        //    worldSphereVertices[i] = sphere.transform.localToWorldMatrix.inverse.MultiplyPoint(worldSphereVertices[i]);
        //}
        //cubeMesh.vertices = worldCubeVertices;
        sphereMesh.vertices = worldSphereVertices;

        ReverseFace(true);
        AdjustColliderSize();
        DeForm();

    }

    void AdjustColliderSize()
    {
        meshCollider.sharedMesh = cubeMesh;
    }

    void DeForm()
    {
        RaycastHit hitinfo;
        Vector3[] changeVertices = new Vector3[worldSphereVertices.Length];
        Dictionary<Vector3, List<Vector3>> vertexNormals = new Dictionary<Vector3, List<Vector3>>();

        for (int i = 0; i < worldSphereVertices.Length; i++)
        {
            Vector3 worldVertex = worldSphereVertices[i];
            Vector3 worldNormal = worldSphereNormals[i];

            if (!vertexNormals.ContainsKey(worldVertex))
            {
                vertexNormals[worldVertex] = new List<Vector3> { worldNormal };
            }
            else
            {
                vertexNormals[worldVertex].Add(worldNormal);
            }
        }
        for (int i = 0; i < worldSphereVertices.Length; i++)
        {
            Vector3 worldVertex = worldSphereVertices[i];
            Vector3 NormalSum = Vector3.zero;

            foreach (Vector3 normal in vertexNormals[worldVertex])
            {
                NormalSum += normal;
            }

            if (Physics.Raycast(worldVertex, NormalSum.normalized, out hitinfo))
            {
                changeVertices[i] = hitinfo.point;
            }

        }
        for (int i = 0; i < changeVertices.Length; i++)
        {
            changeVertices[i] = sphere.transform.worldToLocalMatrix.MultiplyPoint(changeVertices[i]);
        }

        sphereMesh.vertices = changeVertices;
    }



    void ReverseFace(bool result)
    {
        for (int i = 0; i < reverse.Length; i += 3)
        {
            int tmp = reverse[i];
            reverse[i] = reverse[i + 2];
            reverse[i + 1] = reverse[i + 1];
            reverse[i + 2] = tmp;
        }

        for (int i = 0; i < origingNormals.Length; i++)
        {
            reverseNormals[i] = -origingNormals[i];
        }
        if (result)
        {
            cubeMesh.triangles = reverse;
            cubeMesh.normals = reverseNormals;
        }


    }
}
