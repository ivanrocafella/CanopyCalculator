using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class FlatSideGenerator : MonoBehaviour
{
    private ColumnBody columnBody = new();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRectangleMesh();
    }

    // Update is called once per frames
    void Update()
    {
        
    }

    void GenerateRectangleMesh()
    {
        Vector3 vectorByWidth = new(columnBody.width, 0, 0);
        Vector3 vectorByHeight = new(0, columnBody.height, 0);
        Vector3 vectorByLength = new(0, 0, columnBody.length);        

        Mesh mesh = new();

        var corner0 = -vectorByWidth / 2 - vectorByLength / 2 - vectorByHeight / 2;
        var corner1 = vectorByWidth / 2 + vectorByLength / 2 + vectorByHeight / 2;

        var combine = new CombineInstance[6];
        combine[0].mesh = Quad(corner0, vectorByLength, vectorByWidth);
        combine[1].mesh = Quad(corner0, vectorByWidth, vectorByHeight);
        combine[2].mesh = Quad(corner0, vectorByHeight, vectorByLength);
        combine[3].mesh = Quad(corner1, -vectorByWidth, -vectorByLength);
        combine[4].mesh = Quad(corner1, -vectorByHeight, -vectorByWidth);
        combine[5].mesh = Quad(corner1, -vectorByLength, -vectorByHeight);

        mesh.CombineMeshes(combine, true, false);
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Material material = new(Shader.Find("Standard"));
        material.color = Color.black;
        meshRenderer.material = material;
    }

    public static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
    {
        var normal = Vector3.Cross(length, width).normalized;
        var mesh = new Mesh
        {
            vertices = new[] { origin, origin + length, origin + length + width, origin + width },
            normals = new[] { normal, normal, normal, normal },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
            triangles = new[] { 0, 1, 2, 0, 2, 3 }
        };
        return mesh;
    }
}
