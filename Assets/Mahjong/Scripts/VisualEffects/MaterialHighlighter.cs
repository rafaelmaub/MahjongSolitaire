using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaterialHighlighter : PieceAttachment
{
    [SerializeField] private float maxGlow;
    [SerializeField] private float breatheFrequency;
    [SerializeField] private float loopBreatheFrequency;

    private const string emission = "_EmissionColor";
    private Coroutine _currentAnimation;

    private Material _material;
    private float _currentGlow;
    private bool _lockAnimation;

    protected override void Awake()
    {
        base.Awake();
        InitializeHighLight(Piece);
    }

    public void InitializeHighLight(MahjongPiece pc)
    {

        _currentGlow = 1;
        Piece.Interactions.OnPieceSelected += StartBreatheLoop;
        Piece.Interactions.OnPieceUnselected += StopBreatheLoop;
        Piece.Interactions.OnPieceHoverEnter += StartHoverAnimation;
        Piece.Interactions.OnPieceHoverExit += StopHoverAnimation;
    }

    private void OnDestroy()
    {
        if(Piece)
        {
            Piece.Interactions.OnPieceSelected -= StartBreatheLoop;
            Piece.Interactions.OnPieceUnselected -= StopBreatheLoop;
            Piece.Interactions.OnPieceHoverEnter -= StartHoverAnimation;
            Piece.Interactions.OnPieceHoverExit -= StopHoverAnimation;
        }
    }

    void StartBreatheLoop(MahjongPiece pc)
    {
        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }
        _lockAnimation = true;
        _material = Piece.Symbol.material;
        _currentAnimation = StartCoroutine(LoopBreathe(_material));
    }

    void StopBreatheLoop(MahjongPiece pc)
    {
        _lockAnimation = false;
        StopAllCoroutines();
        StopHoverAnimation();
    }

    void StartHoverAnimation()
    {
        if (_lockAnimation) return;

        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }
        _material = Piece.Symbol.material;

        _currentAnimation = StartCoroutine(BreatheIn(_material, breatheFrequency));
    }

    void StopHoverAnimation()
    {
        if (_lockAnimation) return;

        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }
        _material = Piece.Symbol.material;

        _currentAnimation = StartCoroutine(BreathOut(_material, breatheFrequency));
    }


    IEnumerator LoopBreathe(Material spriteMat)
    {
        while(true)
        {
            Coroutine breatheIn = StartCoroutine(BreatheIn(spriteMat, loopBreatheFrequency));
            yield return breatheIn;
            Coroutine breatheOut = StartCoroutine(BreathOut(spriteMat, loopBreatheFrequency));
            yield return breatheOut;
        }
    }

    IEnumerator BreatheIn(Material spriteMat, float freq)
    {
        Color emissionColor = Color.white;
        float time = (1 / freq);
        WaitForSeconds delay = new WaitForSeconds(time);
        float step = maxGlow / freq;

        while (_currentGlow < maxGlow)
        {

            spriteMat.SetColor(emission, emissionColor * _currentGlow);
            _currentGlow += step;
            _currentGlow = Mathf.Clamp(_currentGlow, 1, maxGlow);
            yield return delay;
        }

        _currentGlow = maxGlow;
    }

    IEnumerator BreathOut(Material spriteMat, float freq)
    {
        Color emissionColor = Color.white;
        float time = (1 / freq);
        WaitForSeconds delay = new WaitForSeconds(time);
        float step = maxGlow / freq;

        while (_currentGlow > 1)
        {

            spriteMat.SetColor(emission, emissionColor * _currentGlow);
            _currentGlow -= step;
            _currentGlow = Mathf.Clamp( _currentGlow, 1, maxGlow);
            yield return delay;
        }
        _material.SetColor(emission, emissionColor);
        _currentGlow = 1;

    }
}
