using System.Collections.Generic;
using TMPro;
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
    public TMP_Text coinsText;
    public List<ResourceCard> collectibleResources;
    public List<ResourceCard> mineableResources;
    public List<ShowHide> _showHides = new(); //0-coins, 1-collectable, 2-mineable
}
