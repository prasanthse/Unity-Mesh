using System;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    #region VARIABLES
    private Mesh mesh;
    private bool canAnimateMesh;
    #endregion

    #region UNITY CALLBACKS
    private void Start()
    {
        // Create a new mesh instance
        mesh = new Mesh();

        // Assign the mesh to the Meshfilter
        GetComponent<MeshFilter>().mesh = mesh;

        //CreateTriangle();
        CreateCube();
    }

    private void Update()
    {
        if (canAnimateMesh)
        {
            transform.Rotate(new Vector3(0.5f, 1, 0.5f) * Time.deltaTime * 50);
        }
    }
    #endregion

    #region TRIANGLE
    private void CreateTriangle()
    {
        // Define arrays for vertices, UVs, and triangles
        Vector3[] vertices = new Vector3[3]; // A triangle has 3 vertex
        Vector2[] uv = new Vector2[3]; // Each vertex should have a currosponding UV coordinate 
        int[] triangles = new int[3]; // A triangle has 3 indices

        const float TRIANGLE_SIZE = 20f;

        // To keep the pivot point in center of the triangle
        Vector3 originPosition = new Vector3(-TRIANGLE_SIZE, -TRIANGLE_SIZE, 0) * 0.5f;

        // Set the vertex positions (in local space)
        vertices[0] = originPosition;
        vertices[1] = originPosition + Vector3.up * TRIANGLE_SIZE;
        vertices[2] = originPosition + new Vector3(TRIANGLE_SIZE, TRIANGLE_SIZE, 0);

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

        // For testing purpose
        canAnimateMesh = false;
    }
    #endregion

    #region CUBE
    private void CreateCube()
    {
        List<Vector3> totalVertices = new List<Vector3>();
        List<Vector2> totalUVs = new List<Vector2>();
        List<int> totalTriangles = new List<int>();

        const float CUBE_SIZE = 10;

        // To keep the pivot point in center of the cube
        Vector3 originPosition = Vector3.one * (-CUBE_SIZE) * 0.5f;

        // Front side
        CreateSide(originPosition, CUBE_SIZE, Vector3.up, Vector3.right, ref totalVertices, ref totalUVs, ref totalTriangles);

        // Back side
        CreateSide(originPosition + Vector3.forward * CUBE_SIZE, CUBE_SIZE, Vector3.right, Vector3.up, ref totalVertices, ref totalUVs, ref totalTriangles);

        // Top side
        CreateSide(originPosition + Vector3.up * CUBE_SIZE, CUBE_SIZE, Vector3.forward, Vector3.right, ref totalVertices, ref totalUVs, ref totalTriangles);

        // Bottom side
        CreateSide(originPosition, CUBE_SIZE, Vector3.right, Vector3.forward, ref totalVertices, ref totalUVs, ref totalTriangles);

        // Left side
        CreateSide(originPosition + Vector3.forward * CUBE_SIZE, CUBE_SIZE, Vector3.up, Vector3.back, ref totalVertices, ref totalUVs, ref totalTriangles);

        // Right side
        CreateSide(originPosition + Vector3.right * CUBE_SIZE, CUBE_SIZE, Vector3.up, Vector3.forward, ref totalVertices, ref totalUVs, ref totalTriangles);

        mesh.vertices = totalVertices.ToArray();
        mesh.uv = totalUVs.ToArray();
        mesh.triangles = totalTriangles.ToArray();

        // For testing purpose
        canAnimateMesh = true;
    }

    private void CreateSide(
        Vector3 originPosition,
        float size,
        Vector3 secondVertexDirection,
        Vector3 thirdVertexDirection,
        ref List<Vector3> totalVertices,
        ref List<Vector2> totalUVs,
        ref List<int> totalTriangles
    )
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];
        
        vertices[0] = originPosition;
        vertices[1] = originPosition + secondVertexDirection * size;
        vertices[2] = originPosition + (secondVertexDirection + thirdVertexDirection) * size;
        vertices[3] = originPosition + thirdVertexDirection * size;

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        int vertexCount = totalVertices.Count;

        triangles[0] = vertexCount + 0;
        triangles[1] = vertexCount + 1;
        triangles[2] = vertexCount + 2;

        triangles[3] = vertexCount + 0;
        triangles[4] = vertexCount + 2;
        triangles[5] = vertexCount + 3;

        totalVertices.AddRange(vertices);
        totalUVs.AddRange(uv);
        totalTriangles.AddRange(triangles);
    }
    #endregion
}