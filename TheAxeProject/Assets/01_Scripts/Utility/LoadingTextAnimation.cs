using System.Collections;
using UnityEngine;
using TMPro;

public class LoadingTextAnimation : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public float dotInterval = 0.5f;
    [SerializeField] private string _baseText = "Loading";

    private void Start()
    {
        if (loadingText != null)
        {
            StartCoroutine(AnimateLoadingText());
        }
    }

    private IEnumerator AnimateLoadingText()
    {
        int dotCount = 0;

        while (true)
        {
            loadingText.text = _baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4;
            yield return new WaitForSeconds(dotInterval);
        }
    }
}