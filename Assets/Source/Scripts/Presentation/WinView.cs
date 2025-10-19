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
    [SerializeField] private Button _nextButton;

    private bool _isTransitioning;

    private void Awake()
    {
        gameObject.SetActive(false);
        _canvasGroup.alpha = 0f;
        _window.localScale = Vector3.one * 0.7f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _isTransitioning = false;
        _nextButton.interactable = true;

        DOTween.Kill(_canvasGroup);
        DOTween.Kill(_window);

        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(1f, 0.4f))
            .Join(_window.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
            .Play();
    }

    public void Hide(Action onHidden = null)
    {
        if (_isTransitioning) 
            return;
        _isTransitioning = true;

        _nextButton.interactable = false;

        DOTween.Kill(_canvasGroup);
        DOTween.Kill(_window);

        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(0f, 0.3f))
            .Join(_window.DOScale(0.7f, 0.3f).SetEase(Ease.InBack))
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                onHidden?.Invoke();
            })
            .Play();
    }

    public void SetNextAction(Action onNext)
    {
        _nextButton.onClick.RemoveAllListeners();
        _nextButton.onClick.AddListener(() =>
        {
            if (_isTransitioning) return;
            _nextButton.interactable = false;

            Hide(onNext);
        });
    }
    
    private void OnDisable()
    {
        DOTween.Kill(_canvasGroup);
        DOTween.Kill(_window);
        _nextButton.onClick.RemoveAllListeners();
    }
}