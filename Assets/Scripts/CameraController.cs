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
    private Camera m_Camera;
    private float targetZoom;
    private float zoomFactor = 10f;
    [SerializeField]
    private float zoomLerpSpeed = 10;

    private void Start()
    {
        m_Camera = Camera.main;
        targetZoom = m_Camera.fieldOfView;
    }

    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log($"{scrollData}");      
        if (Input.GetKey(KeyCode.LeftControl))
        {
            targetZoom -= scrollData * zoomFactor;
            m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }            
    }
}
