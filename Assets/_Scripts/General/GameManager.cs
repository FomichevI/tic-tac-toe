using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������, �������� � ���� ��� �������� ��������� � ������������ ����.
/// </summary>
// ����� ��������� ���������� ����, ������� � ������� ����� ��������� � ��������� �����
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SessionConfig SessionConfig;

    private void Awake()
    {
        if (Instance == null)        
            Instance = this;        
    }

}
