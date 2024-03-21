using System;
using UnityEngine;

// � ������� ����� ����� �� ����� (����� ������� �� ��������� ������ �� ����������). ��������� ��� ��������� ���������� � ���� Cell � �����
public class GridPrefab : MonoBehaviour
{
    public Row[] Rows;
}

[Serializable]
public class Row
{
    public Cell[] Cells;
}
