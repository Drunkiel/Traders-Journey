using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SomeData
{
    public string name;
    public Sprite[] sprites;
    public Vector2 size;
    public Price[] prices;
    public Production[] productions;
}

public class DataUI : MonoBehaviour
{
    public SomeData _data;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform costSectionTransform;
    [SerializeField] private Transform productionSectionTransform;
    [SerializeField] private Transform infoSectionTransform;

    public virtual void SetData()
    {

    }

    public void UpdateData()
    {
        ClearPreviousData();
        SetData();
        nameText.text = _data.name;

        if (costSectionTransform != null)
        {
            //Spawning cells for Cost section
            for (int i = 0; i < _data.prices.Length; i++)
            {
                GameObject newCell = Instantiate(cellPrefab, costSectionTransform);
                newCell.transform.GetChild(1).GetComponent<Image>().sprite = ResourcesData.instance.GetSprite(_data.prices[i].resources);
                newCell.transform.GetChild(2).GetComponent<TMP_Text>().text = _data.prices[i].quantity.ToString();
            }
        }

        if (productionSectionTransform != null)
        {
            //Spawning cells for Production section
            for (int i = 0; i < _data.productions.Length; i++)
            {
                GameObject newCell = Instantiate(cellPrefab, productionSectionTransform);
                newCell.transform.GetChild(1).GetComponent<Image>().sprite = ResourcesData.instance.GetSprite(_data.productions[i].resources);
                string stringCorrection(int i)
                {
                    if (i == 1) return " day";
                    else return " days";
                }
                int productionTime = _data.productions[i].productionTime;
                newCell.transform.GetChild(2).GetComponent<TMP_Text>().text = _data.productions[i].quantity + " in " + productionTime + stringCorrection(productionTime);
            }
        }

        if (infoSectionTransform != null)
        {
            //Spawning cell for Info section
            GameObject cell = Instantiate(cellPrefab, infoSectionTransform);
            cell.transform.GetChild(1).GetComponent<Image>();
            cell.transform.GetChild(2).GetComponent<TMP_Text>().text = _data.size.x + "x" + _data.size.y;
        }
    }

    private void ClearPreviousData()
    {
        //Clear Cost section
        if (costSectionTransform != null && costSectionTransform.childCount > 2)
        {
            for (int i = 2; i < costSectionTransform.childCount; i++)
            {
                GameObject childObject = costSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }

        //Clear Production section
        if (productionSectionTransform != null && productionSectionTransform.childCount > 2)
        {
            for (int i = 2; i < productionSectionTransform.childCount; i++)
            {
                GameObject childObject = productionSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }

        //Clear Info section
        if (infoSectionTransform != null && infoSectionTransform.childCount > 2)
        {
            for (int i = 2; i < infoSectionTransform.childCount; i++)
            {
                GameObject childObject = infoSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }
    }
}
