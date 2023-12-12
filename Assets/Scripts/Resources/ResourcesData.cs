using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceCard
{
    public Sprite sprite;
    public string name;
    public int quantity;
}

[System.Serializable]
public class ResourcesData
{
    public List<ResourceCard> collectibleResources;
    public List<ResourceCard> mineableResources;
}
