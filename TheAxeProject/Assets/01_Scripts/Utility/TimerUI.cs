using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timer;
    
    private void Update()
    {
        // TODO : 시간 초 UI 완성
        // TODO : 15분 되면 게임 끝나고 재시작 만들기
        _timer.text = string.Format("{0:D2}:{1:D2}", GameManager.Instance.CurrentGameMinute, (int)GameManager.Instance.CurrentGameTime);
    }
}
