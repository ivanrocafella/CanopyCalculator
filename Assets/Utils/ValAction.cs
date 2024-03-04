using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils
{
    public static class ValAction
    {
        public static float ToFloat(string textInput)
        {
            string commoTextInput;
            float value;
            if (textInput.Contains('.'))
            {
                commoTextInput = textInput.Replace('.', ',');
                value = float.Parse(commoTextInput);
            }
            else
                value = float.Parse(textInput);
            return value;
        }

        public static float GetPricePmPlayerPrefs(string nameProfile) => PlayerPrefs.GetFloat($"{nameProfile}PricePm");
        public static void SetPricePmPlayerPrefs(string nameProfile, float value) => PlayerPrefs.SetFloat($"{nameProfile}PricePm", value);
        public static float GetDollarRatePlayerPrefs() => PlayerPrefs.GetFloat("DollarRate");
    }
}
