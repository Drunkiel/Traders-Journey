using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingSaveData
{
    public List<Vector3> positions = new();
    public List<string> _buildingNames = new();
}