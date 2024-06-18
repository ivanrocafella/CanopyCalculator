using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class InputFieldValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject emInputField;
    public bool isValid;
    string tagInputField;
    static string inputText;
    static string errorMessage;
    public void ValidateInputField()
    {
        inputText = inputField.text;
        tagInputField = inputField.tag;
        emInputField = GameObject.FindGameObjectWithTag($"Em{tagInputField}");
        if (IsInputValid(inputText))
        {
            // The input is valid
            emInputField.GetComponent<TMP_Text>().text = string.Empty;
            isValid = true;
            Debug.Log("Input is valid: " + inputText);
        }
        else
        {
            // The input is invalid                       
            emInputField.GetComponent<TMP_Text>().text = errorMessage;
            isValid = false;
            Debug.Log($"Input is invalid ({errorMessage}): {inputText}");
        }
    }

    private bool IsInputValid(string inputText)
    {
        string commoText;
        bool isFloat;
        float value;

        if (inputText.Contains('.'))
        { 
             commoText = inputText.Replace('.', ',');
             isFloat = float.TryParse(commoText, out value);     
        }
        else
             isFloat = float.TryParse(inputText, out value);


        if (!isFloat)
        {
            errorMessage = "Введите число";
            return false;
        }
        else
        {
            // Check length validation (add your own minimum and maximum)
            switch (tagInputField)
            {
                case "SpanInput":
                    if (value < 1 || value > 20)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 1 до 20";
                        return false;
                    }
                    break;
                case "LengthInput":
                    if (value < 2 || value > 40)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 2 до 40";
                        return false;
                    }
                    break;
                case "HeightInput":
                    if (value < 1 || value > 10)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 1 до 10";
                        return false;
                    }
                    break;
                case "SlopeInput":
                    if (value < 7 || value > 45)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 7 до 45";
                        return false;
                    }
                    break;
                case "CountStepColumnInput":
                    if (value < 1 || value > 10)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 1 до 10";
                        return false;
                    }
                    break;
                case "OutputRafterInput":
                    if (value < 10 || value > 50)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 10 до 50";
                        return false;
                    }
                    break;
                case "OutputGirderInput":
                    if (value < 10 || value > 40)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 10 до 40";
                        return false;
                    }
                    break;
                case "StepRafterInput":
                    if (value < 40)
                    {
                        errorMessage = "Значение должно быть не меньше 40";
                        return false;
                    }
                    break;
                case "StepGirderInput":
                    if (value < 20)
                    {
                        errorMessage = "Значение должно быть не меньше 20";
                        return false;
                    }
                    break;
                case "WorkLoadInput":
                    if (value < 0 || value > 500)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 0 до 500";
                        return false;
                    }
                    break;
                default:
                    break;
            }
        }

        // Implement custom validation logic if needed

        // If no checks failed, consider the input valid
        return true;
    }

    private bool IsInteger(string inputText)
    {
        int value;
        return int.TryParse(inputText, out value);
    }
}
