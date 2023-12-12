using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isDevMode = true;
    public static bool isGamePaused;
    [SerializeField] private Price[] firstResources;

    public void GiveFirstResources()
    {
        if (CycleController.instance.day != 1) return;

/*        for (int i = 0; i < firstResources.Length; i++)
        {
            ResourcesData.instance.AddResources(firstResources[i].resources, firstResources[i].quantity);
        }*/
    }
}
