using UnityEngine;

// ����� ������� ��������, �� �� �����-�� ������ ����� ������ ��������������� ��������� ������, ��� ��� �������� ������� ����� ����������
// �� ��������
public class Managers : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
