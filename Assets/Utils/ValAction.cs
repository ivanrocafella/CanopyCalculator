using Assets.Models;
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
        public static bool withRadius = true; // Setting roundness profile or not 
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
        public static float GetPricePmOfProfilePipe(string nameProfile, List<ProfilePipe> profilePipes) => profilePipes.FirstOrDefault(e => e.Name == nameProfile).PricePerM;
        public static float GetPricePmOfTruss(string nameProfile, List<Truss> trusses) => trusses.FirstOrDefault(e => e.Name == nameProfile).PricePerM;
    }
}
