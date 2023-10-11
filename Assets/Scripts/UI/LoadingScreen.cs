using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private GameObject playBTN;

    private bool isGameLoaded;

    private void Update()
    {
        CheckIfLoaded();
    }

    public void CheckIfLoaded()
    {
        if (!isGameLoaded)
        {
            if (GroundGenerator.isGroundGenerated && LakeGenerator.isLakeGenerated && EnvironmentGenerator.isEnviromentGenerated)
            {
                isGameLoaded = true;
                playBTN.SetActive(true);
            }
        }
        else Destroy(GetComponent<LoadingScreen>());
    }
}
