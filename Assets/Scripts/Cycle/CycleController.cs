using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CycleController : MonoBehaviour
{
    public static CycleController instance;
    public int week;
    public int day;
    public UnityEvent endDayEvent;

    [SerializeField] private TMP_Text weekText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private GameObject summaryUI;

    private void Awake()
    {
        instance = this;
    }

    public void OpenSummaryUI()
    {
        summaryUI.SetActive(true);
        GameController.isGamePaused = true;
    }

    public void EndDay()
    {
        day++;
        if (day > 7)
        {
            day = 1;
            week++;
        }

        endDayEvent.Invoke();
        GameController.isGamePaused = false;
    }
}
