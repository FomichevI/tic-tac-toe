using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

// �� ������, ��� ������ ������� - ���������� ������������ � ���������� � ����� ������. ��� �������: �������� ������ �� UiGameplay, �� � ����� ������ ���������� ������ �����������
// ���� ���� ������������ �� ����� ���� _gameplay
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerTmp;
    private float _currentTime = 0; public float CurrentTime { get { return _currentTime; } }
    private bool isPlaying = false;

    public void StartTimer()
    {
        Debug.Log("[Timer] ������ �������");
        isPlaying = true;
    }

    public void StopTimer()
    {
        Debug.Log("[Timer] ������ ����������");
        isPlaying = false;
    }

    public void RefreshTimer()
    {
        Debug.Log("[Timer] ������ ��������");
        _currentTime = 0;
        isPlaying = false;
    }

    // ����� ��������� ���������� � �������� � ��������� ���� ��� �������� ������, �� ��� �� �������� ��� ����� ����������
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            _currentTime += Time.fixedDeltaTime;
            _timerTmp.text = NumbersConverter.GetTimeMinSec(_currentTime);
        }
    }
}
