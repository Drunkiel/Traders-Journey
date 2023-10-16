using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform costSectionTransform;
    [SerializeField] private Transform infoSectionTransform;

    public void UpdateData(EnvironmentID _environmentID)
    {
        ClearPreviousData();
        nameText.text = _environmentID.objectName;

        //Spawning cells for Cost section
        for (int i = 0; i < _environmentID.prices.Length; i++)
        {
            GameObject newCell = Instantiate(cellPrefab, costSectionTransform);
            newCell.transform.GetChild(1).GetComponent<Image>().sprite = ResourcesData.instance.GetSprite(_environmentID.prices[i].resources);
            newCell.transform.GetChild(2).GetComponent<TMP_Text>().text = _environmentID.prices[i].quantity.ToString();
        }

        //Spawning cell for Info section
        GameObject cell = Instantiate(cellPrefab, infoSectionTransform);
        cell.transform.GetChild(1).GetComponent<Image>();
        cell.transform.GetChild(2).GetComponent<TMP_Text>().text = _environmentID.size.x + "x" + _environmentID.size.y;
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
