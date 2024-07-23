using UnityEngine;
using System.IO;


public class MeshCube : MonoBehaviour
{
    public Texture2D texture;
    public string shaderName = "Standard";

    void Start()
    {

        // Create the mesh
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.mainTexture = texture;
        Mesh mesh = new Mesh();

        //Vector3[] reVertices = new Vector3[]
        //{
        //    new Vector3(0f, 0f, 0f),
        //    new Vector3(0f, 4f, 0f),
        //    new Vector3(4f, 4f, 0f),
        //    new Vector3(4f, 0f, 0f),

        //    new Vector3(0f, 0f, 4f),
        //    new Vector3(0f, 4f, 4f),
        //    new Vector3(4f, 4f, 4f),
        //    new Vector3(4f, 0f, 4f)
        //};


        Vector3[] reVertices1 = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 4f, 0f),
            new Vector3(4f, 4f, 0f),
            new Vector3(4f, 0f, 0f),

            new Vector3(0f, 0f, 4f),
            new Vector3(0f, 4f, 4f),

            new Vector3(4f, 0f, 4f),
            new Vector3(4f, 4f, 4f),

            new Vector3(4f, 0f, 0f),
            new Vector3(4f, 4f, 0f),

            new Vector3(4f, 4f, 0f),
            new Vector3(0f, 4f, 0f),

            new Vector3(4f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };

        //UVs
        Vector2[] uv = new Vector2[]
       {
            //red
            new Vector2(0.75f, 0.5f),
            new Vector2(0.75f, 0.75f),
            new Vector2(1f, 0.75f),
            new Vector2(1f, 0.5f),  

            //yellow
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.75f),   

            //blue
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 0.75f),       

            // orange
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0.75f),    

            //green
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),    

            //pupple
            new Vector2(0.25f, 0.25f),
            new Vector2(0.5f, 0.25f),
       };

        //Vector3[] reVertices = new Vector3[]
        //{
        //    new Vector3(0f, 0f, 0f),
        //    new Vector3(0f, 4f, 0f),
        //    new Vector3(4f, 4f, 0f),
        //    new Vector3(4f, 0f, 0f),

        //    new Vector3(0f, 0f, 4f),
        //    new Vector3(0f, 4f, 4f),
        //    new Vector3(4f, 4f, 4f),
        //    new Vector3(4f, 0f, 4f)
        //};

        //Vector3[] reVertices2 = new Vector3[]
        //{
        //    new Vector3(0f, 0f, 4f),
        //    new Vector3(0f, 0f, 4f),
        //    new Vector3(0f, 0f, 4f),
        //    new Vector3(0f, 4f, 4f),
        //    new Vector3(0f, 4f, 4f),
        //    new Vector3(0f, 4f, 4f),
        //    new Vector3(4f, 4f, 4f),
        //    new Vector3(4f, 4f, 4f),
        //    new Vector3(4f, 4f, 4f),
        //    new Vector3(4f, 0f, 4f),
        //    new Vector3(4f, 0f, 4f),
        //    new Vector3(4f, 0f, 4f),
        //    new Vector3(0f, 0f, 0f),
        //    new Vector3(0f, 0f, 0f),
        //    new Vector3(0f, 0f, 0f),
        //    new Vector3(0f, 4f, 0f),
        //    new Vector3(0f, 4f, 0f),
        //    new Vector3(0f, 4f, 0f),
        //    new Vector3(4f, 4f, 0f),
        //    new Vector3(4f, 4f, 0f),
        //    new Vector3(4f, 4f, 0f),
        //    new Vector3(4f, 0f, 0f),
        //    new Vector3(4f, 0f, 0f),
        //    new Vector3(4f, 0f, 0f)
        //};
        Vector3[] reVertices2 = new Vector3[]
       {
            new Vector3(0f, 0f, 4f),
            new Vector3(0f, 0f, 4f),
            new Vector3(0f, 0f, 4f),

            new Vector3(0f, 4f, 4f),
            new Vector3(0f, 4f, 4f),
            new Vector3(0f, 4f, 4f),

            new Vector3(4f, 4f, 4f),
            new Vector3(4f, 4f, 4f),
            new Vector3(4f, 4f, 4f),

            new Vector3(4f, 0f, 4f),
            new Vector3(4f, 0f, 4f),
            new Vector3(4f, 0f, 4f),


            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 0f, 0f),

            new Vector3(0f, 4f, 0f),
            new Vector3(0f, 4f, 0f),
            new Vector3(0f, 4f, 0f),

            new Vector3(4f, 4f, 0f),
            new Vector3(4f, 4f, 0f),
            new Vector3(4f, 4f, 0f),

            new Vector3(4f, 0f, 0f),
            new Vector3(4f, 0f, 0f),
            new Vector3(4f, 0f, 0f),
       };

        Vector3[] normals = new Vector3[]
        {
            Vector3.forward,
            Vector3.left,
            Vector3.down,

            Vector3.forward,
            Vector3.left,
            Vector3.up,

            Vector3.forward,
            Vector3.right,
            Vector3.up,
           
            Vector3.forward,
            Vector3.right,
            Vector3.down,

            Vector3.back,
            Vector3.left,
            Vector3.down,

            Vector3.back,
            Vector3.left,
            Vector3.up,

            Vector3.back,
            Vector3.right,
            Vector3.up,

            Vector3.back,
            Vector3.right,
            Vector3.down
        };

        // Triangles
        int[] triangles = new int[]
        { 
        };

        mesh.vertices = reVertices2;
        mesh.uv = uv;

        mesh.normals = normals;
        //mesh.RecalculateNormals();
        mesh.triangles = triangles;
        filter.mesh = mesh;
        // 노말 벡터를 출력합니다.
        foreach (Vector3 isnormal in mesh.normals)
        {
            Debug.Log(isnormal);
        }
    }

    //private void Update()
    //{
    //    Debug.DrawRay(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), Color.red);
    //    Debug.DrawRay(new Vector3(0f, 4f, 0f), new Vector3(0f, 1f, 0f), Color.blue);
    //    Debug.DrawRay(new Vector3(4f, 4f, 0f), new Vector3(0f, 0f, 1f), Color.yellow);
    //    Debug.DrawRay(new Vector3(4f, 0f, 0f), new Vector3(0f, 0f, -1f), Color.green);
    //}

}