using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsidePartRadiusGenerator : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    public float roundness = 0.005f;    

    // Start is called before the first frame update
    void Start()
    {
        GenerateAangleMesh();
    }

    // Update is called once per frames
    void Update()
    {

    }

   void GenerateAangleMesh()
   {
       Vector3 vectorByX = new(columnBody.RadiusProfile, 0, 0);
       Vector3 vectorByZ = new(0, 0, columnBody.RadiusProfile);
       Vector3 vectorByY = new(0, columnBody.Height, 0);

       Mesh mesh = new();

       var corner0 = - vectorByX / 2 - vectorByZ / 2 - vectorByY / 2;

       var combine = new CombineInstance[2];
       combine[0].mesh = Quad(corner0, vectorByX, vectorByY);
       combine[1].mesh = Quad(corner0, vectorByZ, vectorByY);

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
