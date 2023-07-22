using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBodyGenerator : MonoBehaviour
{
    Mesh mesh;

    [Header("Mesh Settings")]
    public float width = 1f;
    public float length = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CreateMesh();
        transform.position = new Vector3(0f,0f,-50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMesh()
    {
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        // Define the vertices of the mesh
        vertices[0] = new Vector3(-width / 2f, 0f, -length / 2f);
        vertices[1] = new Vector3(width / 2f, 0f, -length / 2f);
        vertices[2] = new Vector3(-width / 2f, 0f, length / 2f);
        vertices[3] = new Vector3(width / 2f, 0f, length / 2f);

        // Define UV coordinates (optional)
        uv[0] = new Vector2(0f, 0f);
        uv[1] = new Vector2(1f, 0f);
        uv[2] = new Vector2(0f, 1f);
        uv[3] = new Vector2(1f, 1f);

        // Define the triangles of the mesh (vertex order is important)
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Assign the data to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();

        // Create a mesh filter and renderer for visualization
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the mesh to the mesh filter
        meshFilter.mesh = mesh;

        // You can assign a material to the mesh renderer to give it a visual appearance
        // For simplicity, I'm assuming you already have a material in your project.
        // If not, you can create one or use Unity's built-in materials.
        // Example:
        Material material = new(Shader.Find("Standard"));
        material.color = Color.black;
        meshRenderer.material = material;
    }
}
