using System.Collections.Generic;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject cardPrefab;

    public void FillContent(List<ResourceCard> cards)
    {
        ClearContent();

        for (int i = 0; i < cards.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, content.transform);
            ResourcesCard _resourceCard = newCard.GetComponent<ResourcesCard>();
            _resourceCard.image.sprite = cards[i].sprite;
            _resourceCard.nameText.text = cards[i].name;
            _resourceCard.quantityText.text = cards[i].quantity.ToString();
        }
    }

    public void ClearContent()
    {
        List<GameObject> allCards = new();

        //Adding cards
        for (int i = 0; i < content.transform.childCount; i++)
            allCards.Add(content.transform.GetChild(i).gameObject);

        //Destroying cards
        for (int i = 0; i < allCards.Count; i++)
            Destroy(allCards[i]);
    }
}
