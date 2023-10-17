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
            Debug.Log("Input is valid: " + inputText);
            isValid = true;
            emInputField.GetComponent<TMP_Text>().text = string.Empty;
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
        bool isInt = int.TryParse(inputText, out int value);

        if (!isInt)
        {
            errorMessage = "Поле обязательно для заполнения";
            return false;
        }
        else
        {
            // Check length validation (add your own minimum and maximum)
            switch (tagInputField)
            {
                case "SpanInput":
                    if (value <= 0 || value > 20)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 1 до 20";
                        return false;
                    }
                    break;
                case "LengthInput":
                    if (value <= 2 || value > 40)
                    {
                        errorMessage = "Значение должно быть в диапазоне от 2 до 40";
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
