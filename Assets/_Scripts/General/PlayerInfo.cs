using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� ������ �� ������ (���, ������, ������� � �.�.)
/// </summary>
[Serializable]
public class PlayerInfo
{
    public string Name;

    public PlayerInfo(string name)
    {
        Name = name;
    }
}
