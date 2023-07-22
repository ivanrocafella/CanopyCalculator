using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f;

    public GameObject pref;
    public Vector3 prefPos = new Vector3(0, 0, 0);
    public List<GameObject> testGameOgjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input for rotation (e.g., from keyboard or touch/mouse input)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Calculate the rotation amount based on the input and rotation speed
        float horizontalRotation = horizontalInput * rotationSpeed;

        // Apply the rotation around the y-axis (upwards) to the camera
        transform.Rotate(Vector3.up, horizontalRotation * Time.deltaTime);

        // Debug.Log(GameObject.FindGameObjectsWithTag("Test"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject tmp = Instantiate(pref, new Vector3(Random.RandomRange(-rotationSpeed, rotationSpeed), Random.RandomRange(-rotationSpeed, rotationSpeed), Random.RandomRange(-rotationSpeed, rotationSpeed)), Quaternion.identity);
                testGameOgjects.Add(tmp);
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (GameObject go in testGameOgjects)
            {
                Destroy(go);
            }
        }
    }
}
