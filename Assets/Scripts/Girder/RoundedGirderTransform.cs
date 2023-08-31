using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundedGirderTransform : MonoBehaviour
{
    private Girder girder;
    public Direction Direction;
    // Start is called before the first frame update
    void Start()
    {
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(girder.Material.Length / 2 - girder.Material.Radius, 0, -girder.Material.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-girder.Material.Length / 2, 0, -(girder.Material.Length / 2 - girder.Material.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-girder.Material.Length / 2 + girder.Material.Radius, 0, girder.Material.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(girder.Material.Length / 2, 0, (girder.Material.Length / 2 - girder.Material.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }
}