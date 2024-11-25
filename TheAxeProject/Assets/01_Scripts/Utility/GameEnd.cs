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
        Time.timeScale = 0;
        StartCoroutine(LerpAlphaColorImage());
        StartCoroutine(LerpAlphaColorText());
    }
    
    public void GameOver()
    {
        _infoPanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(true);
        _gameEndText.text = "Game Over";
        _endPanel.color = _overColor;
        TextSetting();
        Time.timeScale = 0;
        StartCoroutine(LerpAlphaColorImage());
        StartCoroutine(LerpAlphaColorText());
    }

    private void TextSetting()
    {
        _surviorTimeText.text = $"살아남은 시간 : {string.Format("{0:D2}:{1:D2}", GameManager.Instance.CurrentGameMinute, (int)GameManager.Instance.CurrentGameTime)}";
        _killText.text = $"적 처치 횟수 : {GameManager.Instance.CurrentEnemyKillCount}";
    }

    private IEnumerator LerpAlphaColorImage()
    {
        float time = 0f;
        float startColor = _endPanel.color.a;

        while (time < _lerpTime)
        {
            _endPanel.color = new Color(_endPanel.color.r, _endPanel.color.g, _endPanel.color.b, Mathf.Lerp(startColor, 0.9f, time / _lerpTime));
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        
        _infoPanel.gameObject.SetActive(true);
    }
    
    private IEnumerator LerpAlphaColorText()
    {
        float time = 0f;
        float startColor = _gameEndText.color.a;

        while (time < _lerpTime)
        {
            _gameEndText.color = new Color(_gameEndText.color.r, _gameEndText.color.g, _gameEndText.color.b, Mathf.Lerp(startColor, 1f, time / _lerpTime));
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        
        _infoPanel.gameObject.SetActive(true);
    }
}
