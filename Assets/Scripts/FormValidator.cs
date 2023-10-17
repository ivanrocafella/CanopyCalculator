using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormValidator : MonoBehaviour
{
    public TMP_InputField SpanInput;
    public TMP_InputField LengthInput;
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsValid())
            Button.interactable = true;
        else
            Button.interactable = false;
            
    }

    private bool IsValid()
    {
        return (SpanInput.GetComponent<InputFieldValidator>().isValid
                && LengthInput.GetComponent<InputFieldValidator>().isValid);
    }
}
