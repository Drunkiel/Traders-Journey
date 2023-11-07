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
        if(_buildingID.buildingSprites.Length == 0) Debug.LogError("Not enough sprites to show: " + gameObject.name);

        switch (SetByBuildingSize(_buildingID.size))
        {
            case 0:
                SetActive(0);
                singleImage.sprite = _buildingID.buildingSprites[0];
                break;

            case 1:
                SetActive(1);
                for (int i = 0; i < 2; i++)
                    doubleImage[i].sprite = _buildingID.buildingSprites[i];
                break;

            //!TODO change it to work on vertical
            case 2:
                SetActive(1);
                for (int i = 0; i < 2; i++)
                    doubleImage[i].sprite = _buildingID.buildingSprites[i];
                break;

            case 3:
                SetActive(2);
                for (int i = 0; i < 4; i++)
                    quadroImage[i].sprite = _buildingID.buildingSprites[i];
                break;
        }
    }

    private int SetByBuildingSize(Vector2 size)
    {
        if (size.x == 0 || size.y == 0) Debug.LogError("Building is too small: " + gameObject.name);
        if (size.x == 1 && size.y == 1) return 0; //1:1
        if (size.x == 2 && size.y == 1) return 1; //2:1
        if (size.x == 1 && size.y == 2) return 2; //1:2
        if (size.x == 2 && size.y == 2) return 3; //2:2

        return 0;
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
