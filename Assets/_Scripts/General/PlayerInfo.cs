using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, содержащий данные об игроке (имя, аватар, рейтинг и т.д.)
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
