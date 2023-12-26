using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownBarController : MonoBehaviour
{
    private const int SECONDS_FOR_COUNTDOWN = 5;

    [SerializeField] private Slider _countDownBar;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;

    private Coroutine _countDownCoroutine;

    public event Action OnTimeOut;

    public void StartCountDown()
    {
        _countDownCoroutine = StartCoroutine(CountDownCoroutine());
    }

    public void StopCountDown()
    {
        if (_countDownCoroutine == null) return;

        StopCoroutine(_countDownCoroutine);
    }

    public float GetCurrentProcessTime()
    {
        return _countDownBar.normalizedValue;
    }

    private IEnumerator CountDownCoroutine()
    {
        SetMaxValue(SECONDS_FOR_COUNTDOWN);

        while (_countDownBar.value > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SetValue(_countDownBar.value - Time.deltaTime);
        }

        OnTimeOut?.Invoke();
    }

    private void SetMaxValue(float value)
    {
        _countDownBar.maxValue = value;   
        _countDownBar.value = value;
        _fill.color = _gradient.Evaluate(1.0f);
    }

    private void SetValue(float value)
    {
        _countDownBar.value = value;
        _fill.color = _gradient.Evaluate(_countDownBar.normalizedValue);
    }
}
