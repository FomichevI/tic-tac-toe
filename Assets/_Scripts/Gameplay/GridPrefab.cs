using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// В будущем класс будет не нужен. Необходим для получения информации о Cell в сетке
public class GridPrefab : MonoBehaviour
{
    public Row[] Rows;
}

[Serializable]
public class Row
{
    public Cell[] Cells;
}
