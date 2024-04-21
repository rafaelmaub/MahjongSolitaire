using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaterialHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float maxGlow;
    [SerializeField] private float breatheFrequency;

    private const string emission = "_EmissionColor";
    private Coroutine _currentAnimation;
    private MahjongPiece _piece;
    private Material _material;
    [SerializeField] private float _currentGlow;
    public void InitializeHighLight(MahjongPiece pc)
    {
        _piece = pc;
        _material = _piece.Symbol.material;
        _currentGlow = 1;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_piece.IsPlayable());

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }
        

        _currentAnimation = StartCoroutine(BreatheIn());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }

        _currentAnimation = StartCoroutine(BreathOut());
    }



    IEnumerator BreatheIn()
    {
        Color emissionColor = Color.white;
        WaitForSeconds delay = new WaitForSeconds(( 1 / breatheFrequency));
        while(_currentGlow < maxGlow)
        {

            _material.SetColor(emission, emissionColor * _currentGlow);
            _currentGlow += breatheFrequency / maxGlow;
            _currentGlow = Mathf.Clamp(_currentGlow, 1, maxGlow);
            yield return delay;
        }

        _currentGlow = maxGlow;
    }

    IEnumerator BreathOut()
    {
        Color emissionColor = Color.white;
        WaitForSeconds delay = new WaitForSeconds((1 / breatheFrequency));

        while (_currentGlow > 1)
        {

            _material.SetColor(emission, emissionColor * _currentGlow);
            _currentGlow -= breatheFrequency / maxGlow;
            _currentGlow = Mathf.Clamp( _currentGlow, 1, maxGlow);
            yield return delay;
        }
        _material.SetColor(emission, emissionColor);
        _currentGlow = 1;

    }
}
