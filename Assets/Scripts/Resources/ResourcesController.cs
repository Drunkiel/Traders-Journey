using UnityEngine;

public enum Resources
{
    Coins,
    Wheat,
    Meat,
    Stone,
    Gold,
    Silver,
    Diamond
}

public class ResourcesController : MonoBehaviour
{
    public ResourcesData _data;

    [SerializeField] private Transform content;

    private void Start()
    {
        AddResources(Resources.Diamond, 1);
    }

    public void AddResources(Resources resources, int quantity)
    {
        print((int)resources);
        switch (resources)
        {
            case Resources.Coins:
                break;
            case Resources.Wheat:
                break;
            case Resources.Meat:
                break;
            case Resources.Stone:
                break;
            case Resources.Gold:
                break;
            case Resources.Silver:
                break;
            case Resources.Diamond:
                break;
        }
    }
}
