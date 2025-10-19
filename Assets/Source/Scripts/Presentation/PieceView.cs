using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class PieceView : MonoBehaviour
{
    public Piece Model;
    private SpriteRenderer _renderer;
    private Color _originalColor;
    private Tween _pulseTweenColor;
    private Tween _pulseTweenScale;
    private readonly GameConfig _config = ServiceContainer.Resolve<GameConfig>();
    
    public void SetColor(Color c)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = c;
        _originalColor = c;
    }

    public void SetHighlighted(bool on)
    {
        _pulseTweenColor?.Kill();
        _pulseTweenScale?.Kill();

        if (on)
        {
            if (_renderer == null) return;

            Color.RGBToHSV(_originalColor, out float h, out float s, out float v);

            _pulseTweenColor = DOTween.To(() => 0f, t =>
                {
                    float newV = Mathf.Lerp(v * 0.8f, v, t); 
                    _renderer.color = Color.HSVToRGB(h, s, newV);
                }, 1f, _config.DurationPerUnit)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        else
        {
            _renderer.DOColor(_originalColor, _config.DurationPerUnit);
            transform.DOScale(Vector3.one, _config.DurationPerUnit);
        }
    }
}