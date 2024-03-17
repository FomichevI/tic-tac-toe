using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SessionConfig", order = 1)]
public class SessionConfig : ScriptableObject
{
    public PlayerData Player;
    public PlayerData Opponent;
    public bool IsOpponentBot = false;
}
