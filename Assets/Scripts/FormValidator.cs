using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormValidator : MonoBehaviour
{
    public TMP_InputField SpanInput;
    public TMP_InputField LengthInput;
    public TMP_InputField HeightInput;
    public TMP_InputField SlopeInput;
    public TMP_InputField CountStepColumnInput;
    public TMP_InputField OutputRafterInput;
    public TMP_InputField OutputGirderInput;
    public TMP_InputField StepRafterInput;
    public TMP_InputField StepGirderInput;
    public Button Button;
    private GameObject emStepRafterInput;
    private GameObject emStepGirderInput;
    private string messageStepRafterInput;
    private string messageStepGirderInput;
    private int valueLengthInput; // u.m. = mm
    private int valueSpanInput; // u.m. = mm
    private int valueStepRafterInput; // u.m. = mm
    private int valueStepGirderInput; // u.m. = mm

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string tagStepRafterInput = StepRafterInput.tag;
        emStepRafterInput = GameObject.FindGameObjectWithTag($"Em{tagStepRafterInput}");
        string tagStepGirderInput = StepGirderInput.tag;
        emStepGirderInput = GameObject.FindGameObjectWithTag($"Em{tagStepGirderInput}");
        if (IsValid())
        {
            valueLengthInput = int.Parse(LengthInput.text) * 1000;
            valueSpanInput = int.Parse(SpanInput.text) * 1000;
            valueStepRafterInput = int.Parse(StepRafterInput.text) * 10;
            valueStepGirderInput = int.Parse(StepGirderInput.text) * 10;

            if (valueLengthInput < valueStepRafterInput || valueSpanInput < valueStepGirderInput)
            {
                if (valueLengthInput < valueStepRafterInput)
                {
                    messageStepRafterInput = $"Значение должно быть не больше {int.Parse(LengthInput.text) * 100}";
                    emStepRafterInput.GetComponent<TMP_Text>().text = messageStepRafterInput;
                }
                if (valueSpanInput < valueStepGirderInput)
                {
                    messageStepGirderInput = $"Значение должно быть не больше {int.Parse(SpanInput.text) * 100}";
                    emStepGirderInput.GetComponent<TMP_Text>().text = messageStepGirderInput;
                }
                Button.interactable = false;
            }
            else
                Button.interactable = true;
        }
        else         
            Button.interactable = false;    
    }

    private bool IsValid()
    {
        return (SpanInput.GetComponent<InputFieldValidator>().isValid
                && LengthInput.GetComponent<InputFieldValidator>().isValid
                && HeightInput.GetComponent<InputFieldValidator>().isValid
                && SlopeInput.GetComponent<InputFieldValidator>().isValid)
                && CountStepColumnInput.GetComponent<InputFieldValidator>().isValid
                && OutputRafterInput.GetComponent<InputFieldValidator>().isValid
                && OutputGirderInput.GetComponent<InputFieldValidator>().isValid
                && StepRafterInput.GetComponent<InputFieldValidator>().isValid
                && StepGirderInput.GetComponent<InputFieldValidator>().isValid;
    }
}
