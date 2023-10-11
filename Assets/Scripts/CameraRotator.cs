using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private float xRot;
    private float yRot;
    public float sensitivityMouse = -1.0f;
    public float sensitivityScroll = 100f;
    private Vector3 rotate;
    private Vector3 position;
    void Start()
    {
    }
    void Update()
    {
        yRot = Input.GetAxis("Mouse X");
        xRot = Input.GetAxis("Mouse Y");
        rotate = new Vector3(xRot, yRot * sensitivityMouse, 0);
        if (Input.GetKey(KeyCode.Mouse0))
            transform.eulerAngles = transform.eulerAngles + rotate;
    }
}
