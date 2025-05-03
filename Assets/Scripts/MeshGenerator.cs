using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private void Start()
    {
        // Create a new mesh instance
        mesh = new Mesh();

        // Assign the mesh to the Meshfilter
        GetComponent<MeshFilter>().mesh = mesh;

        CreateTriangle();
    }

    private void CreateTriangle()
    {
        // Define arrays for vertices, UVs, and triangles
        Vector3[] vertices = new Vector3[3]; // A triangle has 3 vertex
        Vector2[] uv = new Vector2[3]; // Each vertex should have a currosponding UV coordinate 
        int[] triangles = new int[3]; // A triangle has 3 indices

        // Set the vertex positions (in local space)
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 20, 0);
        vertices[2] = new Vector3(20, 20, 0);

        // Set UV coordinates corresponding to each vertex
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);

        // Define the order of vertices to form the triangle
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Assign the arrays to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}