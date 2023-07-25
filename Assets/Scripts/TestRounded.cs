using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestRounded : MonoBehaviour
{
    public float cornerRadius = 0.1f;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
    }

    Vector3[] GenerateVertices()
    {
        Vector3[] vertices = new Vector3[8];

        // Define the cube vertices
        Vector3 frontBottomLeft = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 frontBottomRight = new Vector3(0.5f, -0.5f, -0.5f);
        Vector3 frontTopLeft = new Vector3(-0.5f, 0.5f, -0.5f);
        Vector3 frontTopRight = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 backBottomLeft = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 backBottomRight = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 backTopLeft = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 backTopRight = new Vector3(0.5f, 0.5f, 0.5f);

        // Apply corner rounding
        float roundedRadius = cornerRadius * Mathf.Sqrt(2f);
        vertices[0] = frontBottomLeft + new Vector3(cornerRadius, cornerRadius, 0);
        vertices[1] = frontBottomRight + new Vector3(-cornerRadius, cornerRadius, 0);
        vertices[2] = frontTopLeft + new Vector3(cornerRadius, -cornerRadius, 0);
        vertices[3] = frontTopRight + new Vector3(-cornerRadius, -cornerRadius, 0);
        vertices[4] = backBottomLeft + new Vector3(cornerRadius, cornerRadius, 0);
        vertices[5] = backBottomRight + new Vector3(-cornerRadius, cornerRadius, 0);
        vertices[6] = backTopLeft + new Vector3(cornerRadius, -cornerRadius, 0);
        vertices[7] = backTopRight + new Vector3(-cornerRadius, -cornerRadius, 0);

        return vertices;
    }

    int[] GenerateTriangles()
    {
        // Define the cube triangles (6 faces, 2 triangles each, 3 vertices per triangle)
        int[] triangles = new int[]
        {
            0, 1, 2, 2, 1, 3, // Front face
            1, 5, 3, 3, 5, 7, // Right face
            5, 4, 7, 7, 4, 6, // Back face
            4, 0, 6, 6, 0, 2, // Left face
            2, 3, 6, 6, 3, 7, // Top face
            4, 5, 0, 0, 5, 1  // Bottom face
        };

        return triangles;
    }
}
