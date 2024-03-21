using System;
using UnityEngine;

// В будущем класс будет не нужен (будет заменен на генерацию уровня по параметрам). Необходим для получения информации о всех Cell в сетке
public class GridPrefab : MonoBehaviour
{
    public Row[] Rows;
}

[Serializable]
public class Row
{
    public Cell[] Cells;
}
