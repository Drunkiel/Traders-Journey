using UnityEngine;

public class SingleChunk : MonoBehaviour
{
    public bool isOwned;

    public void BuyChunk()
    {
        isOwned = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
