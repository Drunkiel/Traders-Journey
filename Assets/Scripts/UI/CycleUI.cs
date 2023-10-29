using TMPro;
using UnityEngine;

public class CycleUI : MonoBehaviour
{
    public TMP_Text weekText;
    public TMP_Text dayText;

    private void Start()
    {
        UpdateTexts();
        CycleController.instance.endDayEvent.AddListener(() => UpdateTexts());
    }

    private void UpdateTexts()
    {
        weekText.text = "Week " + CycleController.instance.week;
        dayText.text = "Day " + CycleController.instance.day;
    }
}
