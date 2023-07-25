using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestRoundedCorner : MonoBehaviour
{
    public float xSize, ySize, zSize; // u.m. = μμ
    public int xSizeQuan, ySizeQuan, zSizeQuan; // u.m. = 1
    public float roundness; // u.m. = μμ
    public int roundnessQuan; // u.m. = 1

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    // Start is called before the first frame update
    private void Awake()
    {
        xSizeQuan = (int)xSize * 1000;
        ySizeQuan = (int)ySize * 1000;
        zSizeQuan = (int)zSize * 1000;
        roundnessQuan = (int)roundness * 1000;
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Angle";
        CreateVertices();
        //CreateTriangles();
    }

    private void CreateVertices()
    {
        int cornerVertices = 6;
        int edgeVertices = (xSizeQuan + ySizeQuan - 2) * 2;
        int faceVertices = 0;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[vertices.Length];

        int v = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                SetVertex(v++, x, y, 0);
            }
            for (int z = 1; z <= zSize; z++)
            {
                SetVertex(v++, xSize, y, z);
            }
            for (int x = xSize - 1; x >= 0; x--)
            {
                SetVertex(v++, x, y, zSize);
            }
            for (int z = zSize - 1; z > 0; z--)
            {
                SetVertex(v++, 0, y, z);
            }
        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                SetVertex(v++, x, ySize, z);
            }
        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                SetVertex(v++, x, 0, z);
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    private void SetVertex(int i, int x, int y, int z)
    {
        Vector3 inner = vertices[i] = new Vector3(x, y, z);

        if (x < roundness)
        {
            inner.x = roundness;
        }
        else if (x > xSize - roundness)
        {
            inner.x = xSize - roundness;
        }
        if (y < roundness)
        {
            inner.y = roundness;
        }
        else if (y > ySize - roundness)
        {
            inner.y = ySize - roundness;
        }
        if (z < roundness)
        {
            inner.z = roundness;
        }
        else if (z > zSize - roundness)
        {
            inner.z = zSize - roundness;
        }

        normals[i] = (vertices[i] - inner).normalized;
        vertices[i] = inner + normals[i] * roundness;
    }
}
