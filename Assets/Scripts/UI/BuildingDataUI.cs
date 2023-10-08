using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform costSectionTransform;
    [SerializeField] private Transform productionSectionTransform;
    [SerializeField] private Transform infoSectionTransform;

    public void UpdateData(BuildingID _buildingID)
    {
        ClearPreviousData();
        nameText.text = _buildingID.name;

        //Spawning cells for Cost section
        for (int i = 0; i < _buildingID.prices.Length; i++)
        {
            GameObject newCell = Instantiate(cellPrefab, costSectionTransform);
            newCell.transform.GetChild(1).GetComponent<Image>();
            newCell.transform.GetChild(2).GetComponent<TMP_Text>().text = _buildingID.prices[i].quantity.ToString();
        }

        //Spawning cells for Production section
        for (int i = 0; i < _buildingID.productions.Length; i++)
        {
            GameObject newCell = Instantiate(cellPrefab, productionSectionTransform);
            newCell.transform.GetChild(1).GetComponent<Image>();
            string stringCorrection(int i)
            {
                if (i == 1) return " day";
                else return " days";
            }
            int productionTime = _buildingID.productions[i].productionTime;
            newCell.transform.GetChild(2).GetComponent<TMP_Text>().text = _buildingID.productions[i].quantity + " in " + productionTime + stringCorrection(productionTime);
        }
    }

    private void ClearPreviousData()
    {
        //Clear Cost section
        if (costSectionTransform.childCount > 2)
        {
            for (int i = 2; i < costSectionTransform.childCount; i++)
            {
                GameObject childObject = costSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }

        //Clear Production section
        if (productionSectionTransform.childCount > 2)
        {
            for (int i = 2; i < productionSectionTransform.childCount; i++)
            {
                GameObject childObject = productionSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }

        //Clear Info section
        if (infoSectionTransform.childCount > 2)
        {
            for (int i = 2; i < infoSectionTransform.childCount; i++)
            {
                GameObject childObject = infoSectionTransform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }
    }
}
