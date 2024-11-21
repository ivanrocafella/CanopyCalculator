using Assets.Models;
using Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Accessibility;
using Material = UnityEngine.Material;
using Mesh = UnityEngine.Mesh;

namespace Assets.Utils
{
    public static class ValAction
    {
        public static bool withRadius = false; // Setting roundness profile or not 
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

        public static float GetPricePlayerPrefs(string nameProfile) => PlayerPrefs.GetFloat(nameProfile);
        public static void SetPricePlayerPrefs(string nameProfile, float value) => PlayerPrefs.SetFloat(nameProfile, value);
        public static float GetDollarRatePlayerPrefs() => PlayerPrefs.GetFloat("DollarRate");
        public static void ApplyMaterial(Mesh mesh, GameObject gameObject, Material material)
        {
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = material;
        }
    }
}
