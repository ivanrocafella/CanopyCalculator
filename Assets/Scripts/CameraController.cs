using Assets.Models;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Material = Assets.Models.Material;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    private float speed = 2.0f;
    void Start()
    {
        string path = Path.Combine(Application.dataPath,"JSONs","Materials.json");
        Debug.Log(path);
        Material material = new();
        //FileAction<Material>.WriteAndSerialyze(path, material);
        List<Material> materials = FileAction<Material>.ReadAndDeserialyze(path);
        Debug.Log("");
     }
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}
