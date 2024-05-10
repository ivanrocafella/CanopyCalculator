using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeshes : MonoBehaviour
{
    private void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // Get the mesh from the MeshFilter component
            Mesh mesh = meshFilter.mesh;

            // Do something with the mesh
            Debug.Log("Mesh vertices count: " + mesh.vertices.Length);
            Debug.Log("Mesh triangles count: " + mesh.triangles.Length);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
