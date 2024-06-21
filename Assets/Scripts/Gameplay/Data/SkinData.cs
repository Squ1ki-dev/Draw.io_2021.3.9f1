using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "Data/Skin")]
public class SkinData : ScriptableObject
{
    public ColorData Color;
    public BrushData Brush;
}