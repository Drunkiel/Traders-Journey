using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingNameText;
    [SerializeField] private Image singleImage;
    [SerializeField] private Image[] doubleImage;
    [SerializeField] private Image[] quadroImage;

    public void UpdateData(BuildingID _buildingID)
    {
        buildingNameText.text = _buildingID.buildingName;

        switch (_buildingID.buildingSprites.Length)
        {
            default:
                Debug.LogError("Not enough sprites");
                break;

            case 1:
                SetActive(0);
                singleImage.sprite = _buildingID.buildingSprites[0];
                break;

            case 2:
                SetActive(1);
                for (int i = 0; i < 2; i++)
                    doubleImage[i].sprite = _buildingID.buildingSprites[i];
                break;

            case 4:
                SetActive(2);
                for (int i = 0; i < 4; i++)
                    quadroImage[i].sprite = _buildingID.buildingSprites[i];
                break;
        }
    }

    private void SetActive(int i)
    {
        if (i == 0) singleImage.gameObject.SetActive(true);
        else singleImage.gameObject.SetActive(false);

        if (i == 1) doubleImage[0].transform.parent.gameObject.SetActive(true);
        else doubleImage[0].transform.parent.gameObject.SetActive(false);

        if (i == 2) quadroImage[0].transform.parent.gameObject.SetActive(true);
        else quadroImage[0].transform.parent.gameObject.SetActive(false);
    }
}
