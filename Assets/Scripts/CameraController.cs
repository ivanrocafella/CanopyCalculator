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
    private float x;
    private float y;
    public float sensitivity = -1.0f;
    private Vector3 rotate;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
     }
    void Update()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x, y * sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }
}
