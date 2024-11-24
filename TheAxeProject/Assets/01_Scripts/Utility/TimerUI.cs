using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    
    private void Update()
    {
        _timer.text = string.Format("{0:D2}:{1:D2}", GameManager.Instance.CurrentGameMinute, (int)GameManager.Instance.CurrentGameTime);
    }
}
