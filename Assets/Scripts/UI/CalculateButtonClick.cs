using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateButtonClick : MonoBehaviour
{
    private Component comp;
    public void ButtonClicked()
    {
        comp = GameObject.FindGameObjectWithTag("Canopy").GetComponent<CanopyGenerator>();
        Debug.Log("ButtonClicked");
    }
}   
