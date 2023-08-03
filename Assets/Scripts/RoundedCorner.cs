using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
public class RoundedCorner : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private int xSize, ySize, zSize; // u.m. = μμ
    private int[] ySizes = new int[2]; // sizes from 0 before ySize
    private int roundness; // u.m. = μμ
    private int thickness; // u.m. = μμ

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    // Start is called before the first frame update

    private void Start()
    {
        xSize = zSize = (int)columnBody.RadiusProfile;
        ySize = (int)columnBody.Height;
        roundness = (int)columnBody.RadiusProfile;
        thickness = (int)columnBody.WidthProfile;
        ySizes[0] = 0;
        ySizes[1] = ySize;
        Generate();
        CreateVertices();
        CreateTriangles();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Material material = new(Shader.Find("Standard"))
        {
            color = Color.black
        };
        meshRenderer.material = material;
        mesh.RecalculateNormals();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Angle";
        WaitForSeconds wait = new WaitForSeconds(0.05f);
    }

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
        int cornerVertices = 12;    
        int edgeVertices = (xSize + zSize - 2) * 2 + (xSize + zSize - 2 * thickness - 2) * 2;
        int faceVertices = 0;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[vertices.Length];
        Debug.Log(vertices.Length);
        int v = 0;
        int i = 0;
        for (int y = 0; y < ySizes.Length; y++) // external rounded side
        {
            for (int x = 0; x <= xSize; x++)
            {
                i++;
                SetVertex(v++, x, ySizes[y], 0);
                //vertices[v++] = new Vector3(x, ySizes[y], 0);
                Debug.Log($"{x}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }
            for (int z = 1; z <= zSize; z++)
            {
                i++;
                SetVertex(v++, xSize, ySizes[y], z);
                //vertices[v++] = new Vector3(xSize, ySizes[y], z);
                Debug.Log($"{z}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }            
        }
        for (int y = ySizes.Length - 1; y >= 0; y--) // internal rounded side
        {                   
            for (int x = 0; x <= xSize - thickness; x++)
            {
                i++;
                SetVertex(v++, x, ySizes[y], thickness);
                //vertices[v++] = new Vector3(x, ySizes[y], thickness);
                Debug.Log($"{x}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }
            for (int z = thickness + 1; z <= zSize; z++)
            {
                i++;
                SetVertex(v++, xSize - thickness, ySizes[y], z);
                //vertices[v++] = new Vector3(xSize - thickness, ySizes[y], z);
                Debug.Log($"{z}\t{ySizes[y]}\t{i}");
                // yield return wait;
            }
        }
        mesh.vertices = vertices;      
        mesh.normals = normals;
    }

    private void CreateTriangles()
    {
        int quads = xSize + zSize + 2 + xSize + zSize - 2 * thickness + (xSize + zSize - thickness) * 2;
        int[] triangles = new int[quads * 6];
        mesh.triangles = triangles;
        int halfRingExternal = (xSize + zSize) + 1;
        int halfRingInternal = (xSize + zSize - 2 * thickness) + 1;
        int t = 0, v = 0;
        for (int q = 0; q < halfRingExternal - 1; q++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + halfRingExternal, v + halfRingExternal + 1);
        }
        v += halfRingExternal + 1;
        for (int q = 0; q < halfRingInternal - 1; q++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + halfRingInternal, v + halfRingInternal + 1);
        }
        v = 0;
        int cornerVar = 0;
        int additVar = 2 * halfRingExternal + halfRingInternal;
        for (int q = 0; q < halfRingExternal - 1; q++, v++)
        {
            if (v == xSize - thickness)
                cornerVar = q + additVar;           
            if (v >= xSize - thickness && v < halfRingExternal - (zSize - thickness) - 1)
            {
                t = SetAngle(triangles, t, v, cornerVar, v + 1);
                additVar--;
            }
            else
                t = SetQuad(triangles, t, v, v + additVar, v + 1, v + additVar + 1);                   
        }
        additVar = halfRingExternal;
        v = halfRingExternal;
        for (int q = 0; q < halfRingExternal - 1; q++, v++)
        {
            if (v == xSize - thickness + halfRingExternal)
                cornerVar = q + 2 * additVar;
            if (v >= xSize + halfRingExternal - thickness && v < halfRingExternal * 2 - (zSize - thickness) - 1)
            {
                t = SetAngle(triangles, t, cornerVar, v, v + 1);
                additVar--;
            }
            else
                t = SetQuad(triangles, t, v + additVar, v, v + additVar + 1, v + 1);
        }
        //v = halfRingExternal  - 1;
        //t = SetQuad(triangles, t, v, v + halfRingExternal + 2 * halfRingInternal, v + halfRingExternal, v + halfRingExternal + halfRingInternal); // end face

        mesh.triangles = triangles;
    }

    private static int SetAngle(int[] triangles, int i, int v00, int v10, int v01)
    {
        triangles[i] = v00;
        triangles[i + 1] = v01;
        triangles[i + 2] = v10;
        return i + 3;
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
        int radius; int radiusWithThick;
        int sizeByX; int sizeByZ;
        int zWithoutThick;
        Vector3 inner = vertices[i] = new Vector3(x, y, z);
        if (i < (xSize + zSize + 1) * 2)
        {
            sizeByX = xSize;
            sizeByZ = zSize;
            radius = roundness;
            zWithoutThick = z;
            radiusWithThick = radius;
        }
        else
        {
            sizeByX = xSize - thickness;
            sizeByZ = zSize - thickness;
            radius = roundness - thickness;
            zWithoutThick = z - thickness;
            radiusWithThick = radius + thickness;
        }
        if (x > sizeByX - radius)
        {
            inner.x = sizeByX - radius;
        }
        if (zWithoutThick < radius)
        {
            inner.z = radiusWithThick;
        }
        else if (z > sizeByZ - radius)
        {
            if (zWithoutThick < sizeByZ / 2)
                inner.z = sizeByZ - radius;
        }

        normals[i] = (vertices[i] - inner).normalized;
        vertices[i] = inner + normals[i] * radius;
    }
}
