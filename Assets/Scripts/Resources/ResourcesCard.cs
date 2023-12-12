using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCard : MonoBehaviour
{
    public Image image;
    public TMP_Text nameText;
    public TMP_Text quantityText;

    public void UpdateData(ResourceCard _resourceCard)
    {
        image.sprite = _resourceCard.sprite;
        nameText.text = _resourceCard.name;
        quantityText.text = _resourceCard.quantity.ToString();
    }
}
