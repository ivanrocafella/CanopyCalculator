using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "New ProfilePipeDataList", menuName = "ProfilePipe DataList", order = 52)]
public class ProfilePipeDataList : ScriptableObject
{
    public ProfilePipeData[] profilePipesData;
}
