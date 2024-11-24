using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameEndText;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _surviorTimeText;
    [SerializeField] private Image _infoPanel;
    [SerializeField] private Image _endPanel;
    [SerializeField] private Color _clearColor;
    [SerializeField] private Color _overColor;

    [SerializeField] private float _lerpTime = 1f;

    private void Start()
    {
        _infoPanel.gameObject.SetActive(false);
        GameManager.Instance.OnGameClearEvent += GameClear;

        _endPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameClearEvent -= GameClear;
    }

    public void GameClear()
    {
        _infoPanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(true);
        _gameEndText.text = "Game Clear";
        _endPanel.color = _clearColor;
        TextSetting();
        StartCoroutine(LerpAlphaColor(_gameEndText.color));
        StartCoroutine(LerpAlphaColor(_endPanel.color, 0.9f));
    }
    
    public void GameOver()
    {
        _infoPanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(true);
        _gameEndText.text = "Game Over";
        _endPanel.color = _overColor;
        TextSetting();
        StartCoroutine(LerpAlphaColor(_gameEndText.color));
        StartCoroutine(LerpAlphaColor(_endPanel.color, 0.9f));
    }

    private void TextSetting()
    {
        _surviorTimeText.text = $"살아남은 시간 : {string.Format("{0:D2}:{1:D2}", GameManager.Instance.CurrentGameMinute, (int)GameManager.Instance.CurrentGameTime)}";
        _killText.text = $"적 처치 횟수 : {GameManager.Instance.CurrentEnemyKillCount}";
    }

    private IEnumerator LerpAlphaColor(Color color, float endValue = 1f)
    {
        float time = 0f;
        float startColor = color.a;

        while (time < _lerpTime)
        {
            color.a = Mathf.Lerp(startColor, endValue, time / _lerpTime);
            time += Time.deltaTime;
            yield return null;
        }

        color.a = endValue;
        _infoPanel.gameObject.SetActive(true);
    }
}
