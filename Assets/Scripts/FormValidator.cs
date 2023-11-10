using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;

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
    private float valueLengthInput; // u.m. = mm
    private float valueSpanInput; // u.m. = mm
    private float valueStepRafterInput; // u.m. = mm
    private float valueStepGirderInput; // u.m. = mm

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
            string commoTextLengthInput;
            string commoTextSpanInput;
            string commoTextStepRafterInput;
            string commoTextStepGirderInput;

            if (LengthInput.text.Contains('.'))
            {
                commoTextLengthInput = LengthInput.text.Replace('.', ',');
                valueLengthInput = float.Parse(commoTextLengthInput) * 1000;
            }
            else
                valueLengthInput = float.Parse(LengthInput.text) * 1000;
            if (SpanInput.text.Contains('.'))
            {
                commoTextSpanInput = SpanInput.text.Replace('.', ',');
                valueSpanInput = float.Parse(commoTextSpanInput) * 1000;
            }
            else
                valueSpanInput = float.Parse(SpanInput.text) * 1000;
            if (StepRafterInput.text.Contains('.'))
            {
                commoTextStepRafterInput = StepRafterInput.text.Replace('.', ',');
                valueStepRafterInput = float.Parse(commoTextStepRafterInput) * 10;
            }
            else
                valueStepRafterInput = float.Parse(StepRafterInput.text) * 10;
            if (StepGirderInput.text.Contains('.'))
            {
                commoTextStepGirderInput = StepGirderInput.text.Replace('.', ',');
                valueStepGirderInput = float.Parse(commoTextStepGirderInput) * 10;
            }
            else
                valueStepGirderInput = float.Parse(StepGirderInput.text) * 10;


            if (valueLengthInput < valueStepRafterInput || valueSpanInput < valueStepGirderInput)
            {
                if (valueLengthInput < valueStepRafterInput)
                {
                    messageStepRafterInput = $"Значение должно быть не больше {valueLengthInput / 10} см";
                    emStepRafterInput.GetComponent<TMP_Text>().text = messageStepRafterInput;
                }
                if (valueSpanInput < valueStepGirderInput)
                {
                    messageStepGirderInput = $"Значение должно быть не больше {valueSpanInput / 10} см";
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
