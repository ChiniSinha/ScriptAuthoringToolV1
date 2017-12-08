#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class SimpleAudioVisualizer : MonoBehaviour
{
    [SerializeField] protected int _bandCount;

    [SerializeField] protected Image _barPrototype;

    protected List<Image> _bars = new List<Image>();

    public AudioSource Source;

    [SerializeField]
    protected float _maxIntensity;
    [SerializeField] protected float _minMaxIntensity;

    protected bool _isPlaying;
    [SerializeField]
    protected UiAnimator _animator;

    private void Awake()
    {
        for (int i = 0; i < _bandCount; i++)
        {
            Image img = Instantiate(_barPrototype);
            img.transform.SetParent(transform);
            img.transform.localScale = Vector3.one;
            Vector3 pos = img.rectTransform.anchoredPosition3D;
            pos.y = 0;
            pos.z = 0;
            img.rectTransform.anchoredPosition3D = pos;
            img.transform.localRotation = Quaternion.identity;
            _bars.Add(img);
        }
        _maxIntensity = _minMaxIntensity;
    }

    private void Update()
    {
        float[] samples = new float[_bandCount];
        Source.GetSpectrumData(samples, 0, FFTWindow.Rectangular);

        float maxIntensity = Mathf.NegativeInfinity;
        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > maxIntensity && samples[i] != 0)
            {
                maxIntensity = samples[i];
            }
        }

        if (_maxIntensity > maxIntensity)
        {
            _maxIntensity = Mathf.Lerp(_maxIntensity, Mathf.Max(_minMaxIntensity, maxIntensity), 0.5f);
        }
        else
        {
            _maxIntensity = maxIntensity;
        }

        RectTransform rect = transform as RectTransform;
        float maxHeight = rect.rect.height;
        float width = rect.rect.width;
        float barWidth = width/_bandCount;
        for (int i = 0; i < samples.Length; i++)
        {
            Image bar = _bars[i];
            float normalized = samples[i]/_maxIntensity;
            float size = Mathf.Lerp(0, maxHeight, normalized);
            bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
            bar.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, barWidth*(i-1), barWidth);
        }

        if (_isPlaying && !Source.isPlaying)
        {
            _animator.Hide();
        }
        else if (!_isPlaying && Source.isPlaying)
        {
            _animator.Show();
        }

        _isPlaying = Source.isPlaying;
    }
}