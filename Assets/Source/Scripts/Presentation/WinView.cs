using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class WinView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _window;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private Button _restartButton;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
      
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, 0.4f))
            .Join(_window.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
            .Play();
    }

    public void Hide()
    {
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(0f, 0.3f))
            .Join(_window.DOScale(0.7f, 0.3f).SetEase(Ease.InBack))
            .OnComplete(() => gameObject.SetActive(false))
            .Play();
    }

    public void SetRestartAction(Action onRestart)
    {
        _restartButton.onClick.AddListener(() => onRestart?.Invoke());
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveAllListeners();
    }
}