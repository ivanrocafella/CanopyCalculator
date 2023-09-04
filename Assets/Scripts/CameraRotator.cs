using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private float xRot;
    private float yRot;
    private float coordPos;
    public float sensitivityMouse = -1.0f;
    public float sensitivityScroll = 100f;
    private Vector3 rotate;
    private Vector3 position;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        yRot = Input.GetAxis("Mouse X");
        xRot = Input.GetAxis("Mouse Y");
        coordPos = Input.GetAxis("Mouse ScrollWheel");
        rotate = new Vector3(xRot, yRot * sensitivityMouse, 0);
        transform.eulerAngles = transform.eulerAngles + rotate;

        position = new Vector3(coordPos * sensitivityScroll, coordPos * sensitivityScroll, coordPos * sensitivityScroll);
        transform.position = transform.position - position;
    }
}
