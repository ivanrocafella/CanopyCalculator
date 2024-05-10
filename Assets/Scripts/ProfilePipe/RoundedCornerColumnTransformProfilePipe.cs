using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundedCornerColumnTransformProfilePipe : MonoBehaviour
{
    public Direction Direction;
    public KindLength KindLength;
    private ColumnBody columnBody;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = GameObject.FindGameObjectWithTag("ProfilePipeTest").GetComponent<ProfilePipeGenerator>().ColumnBody;
        columnBody.SetHeight(KindLength);
        transform.localScale = new Vector3(0.1f, 1, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(columnBody.Profile.Height / 2 - columnBody.Profile.Radius, 0, -columnBody.Profile.Height / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-columnBody.Profile.Height / 2, 0, -(columnBody.Profile.Height / 2 - columnBody.Profile.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-columnBody.Profile.Height / 2 + columnBody.Profile.Radius, 0, columnBody.Profile.Height / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(columnBody.Profile.Height / 2, 0, (columnBody.Profile.Height / 2 - columnBody.Profile.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
