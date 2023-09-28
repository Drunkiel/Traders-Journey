using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public Sprite singleSprite;
    public Sprite[] moreSprites;
    public string buildingName;

    [SerializeField] private Image singleImage;
    [SerializeField] private Image[] moreImages;
}
