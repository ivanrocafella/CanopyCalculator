using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ProfilePipeData", menuName = "ProfilePipe Data", order = 51)]
public class ProfilePipeData : ScriptableObject
{
    [SerializeField]
    private string profilePipeName;
    [SerializeField] 
    private float height;
    [SerializeField]
    private float length;
    [SerializeField]
    private float thickness;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float area; // u.m. = sm2 
    [SerializeField]
    private float momentInertia; // u.m. = sm4 
    [SerializeField]
    private float momentResistance; // u.m. = sm3
    [SerializeField] 
    private float weightMeter; // u.m. = kg
    [SerializeField]
    private string gost;
    public float pricePerM; // u.m. = $ / m

    public string Name { get => profilePipeName; }
    public float Height { get => height; }
    public float Length { get => length; }
    public float Thickness { get => thickness; }
    public float Radius { get => radius; }
    public float Area { get => area; }
    public float MomentInertia { get => momentInertia; }
    public float MomentResistance { get => momentResistance; }
    public float WeightMeter { get => weightMeter; }
    public string Gost { get => gost; }
    public float RadiusInertia { get => (float)Math.Round(Mathf.Pow(momentInertia / area, 0.5f), 4); } // u.m. = sm
}
