using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public static int populationQuantity;
    public static int coinsQuantity;
    public static int suppliesQuantity;
    public static int wheatQuantity;
    public static int meatQuantity;
    public static int stoneQuantity;
    public static int goldQuantity;
    public static int silverQuantity;
    public static int diamondQuantity;

    public static int GetQuantity(Resources resources)
    {
        int quantity = 0;
        switch (resources)
        {
            case Resources.Coins:
                quantity = coinsQuantity;
                break;
            case Resources.Wheat:
                quantity = wheatQuantity;
                break;
            case Resources.Meat:
                quantity = meatQuantity;
                break;
            case Resources.Stone:
                quantity = stoneQuantity;
                break;
            case Resources.Gold:
                quantity = goldQuantity;
                break;
            case Resources.Silver:
                quantity = silverQuantity;
                break;
            case Resources.Diamond:
                quantity = diamondQuantity;
                break;
        }

        return quantity;
    }
}
