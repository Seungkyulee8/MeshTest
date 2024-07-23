using UnityEngine;

public class MeshSquare : MonoBehaviour
{
    public Texture2D texture;
    public string shaderName = "Unlit/Texture";

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
            new Vector3(-1f, -1f, 0f),
            new Vector3(-1f, 1f, 0f),
            new Vector3(1f, 1f, 0f),
            new Vector3(1f, -1f, 0f)
        };
        Vector3[] normal = new Vector3[]
        {
            //new Vector3(1f, 0f, 0f),
            //new Vector3(1f, 0f, 0f),
            //new Vector3(1f, 0f, 0f),
            //new Vector3(1f, 0f, 0f)
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, 0f, -1f)
        };

        /** UV **/
        Vector2[] uv = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f)
        };

        /** Triangles **/
        int[] triangles = new int[]
        {
            0, 1, 2,
            3, 2, 0

        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.normals = normal;

        // 노말 벡터를 출력합니다.
        foreach (Vector3 isnormal in normal)
        {
            Debug.Log(isnormal);
        }

        //mesh.RecalculateNormals();
        //Debug.DrawRay(Vector3.zero, new Vector3(1f, 0f, 0f), Color.green);
        filter.mesh = mesh;
    }

    private void Update()
    {
        Debug.DrawRay(new Vector3(-1f, -1f, 0f), new Vector3(1f, 0f, 0f), Color.red);
        Debug.DrawRay(new Vector3(-1f, 1f, 0f), new Vector3(0f, 1f, 0f), Color.blue);
        Debug.DrawRay(new Vector3(1f, 1f, 0f), new Vector3(0f, 0f, 1f), Color.yellow);
        Debug.DrawRay(new Vector3(1f, -1f, 0f), new Vector3(0f, 0f, -1f), Color.green);
    }
}