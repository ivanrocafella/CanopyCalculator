using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestRoundedCorner : MonoBehaviour
{
    public int xSize, ySize, zSize; // u.m. = μμ
    private int[] ySizes = new int[2]; // sizes from 0 before ySize
    public int roundness; // u.m. = μμ

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    // Start is called before the first frame update
    private void Start()
    {
        ySizes[0] = 0; ySizes[1] = ySize;
        Generate();
        CreateVertices();
        CreateTriangles();
       // transform.position = new Vector3(0, 10, -100);
    }
    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Angle";
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        
    }

   //private void CreateVertices()
   //{
   //    int cornerVertices = 6;
   //    int edgeVertices = (xSizeQuan + ySizeQuan - 2) * 2;
   //    int faceVertices = 0;
   //    vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
   //    normals = new Vector3[vertices.Length];
   //
   //    int v = 0;
   //    for (int x = 0; x <= xSizeQuan; x++)
   //    {
   //        vertices[v++] = new Vector3((float)xSizeQuan / 1000, 0, 0);
   //        yield return wait;
   //    }
   //}

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }       
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(vertices[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(vertices[i], normals[i]);
        }
    }

    private void CreateVertices()
    {
        int cornerVertices = 6;
        int edgeVertices = (xSize + zSize - 2) * 2;
        int faceVertices = 0;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        Debug.Log(vertices.Length);
        int v = 0;
        int i = 0;
        for (int y = 0; y < ySizes.Length; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                i++;
                vertices[v++] = new Vector3(x, ySizes[y], 0);
                Debug.Log($"{x}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }
            for (int z = 1; z <= zSize; z++)
            {
                i++;
                vertices[v++] = new Vector3(zSize, ySizes[y], z);
                Debug.Log($"{z}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }
        }        
        normals = new Vector3[vertices.Length];
        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    private void CreateTriangles()
    {
        int quads = xSize + zSize;
        int[] triangles = new int[quads * 6];
        mesh.triangles = triangles;
        int halfRing = (xSize + zSize) + 1;
        int t = 0, v = 0;
        for (int q = 0; q < halfRing - 1; q++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + halfRing, v + halfRing + 1);
        }
        mesh.triangles = triangles;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
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
