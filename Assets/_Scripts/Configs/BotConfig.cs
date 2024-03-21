using UnityEngine;

public enum BotType{Easy, Normal, Hard}

/// <summary>
/// Конфигурация ботов разной сложности
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BotConfig", order = 1)]
public class BotConfig : ScriptableObject
{
    [SerializeField] private BotType _type; public BotType type { get { return _type;} }
    [SerializeField] private string _name; public string Name { get { return _name; } }
    [SerializeField] private int _randomTurnChance; public int RandomTurnChance { get { return _randomTurnChance; } } // Шанс случайного хода в %
}
