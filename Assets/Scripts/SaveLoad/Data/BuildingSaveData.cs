using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingSaveData
{
    public List<Vector3> positions = new List<Vector3>();
    public List<BuildingID> _buildings = new List<BuildingID>();
}