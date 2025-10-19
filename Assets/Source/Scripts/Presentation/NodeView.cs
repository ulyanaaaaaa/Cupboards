using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class NodeView : MonoBehaviour
{
    public Node Model;
    private SpriteRenderer _renderer;
    private Color _originalColor;
    private Tween _pulseTween;
    private readonly GameConfig _config = ServiceContainer.Resolve<GameConfig>();

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _originalColor = _renderer.color;
    }

    public void SetHighlighted(bool on, Color highlightColor)
    {
        if (_pulseTween != null)
        {
            _pulseTween.Kill();
            _pulseTween = null;
        }

        if (on)
        {
            _pulseTween = _renderer.DOColor(highlightColor * _config.PulseScaleFactor, _config.PulseDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        else
        {
            _renderer.DOColor(_originalColor, _config.PulseScaleDuration);
        }
    }
}