using UnityEngine;

/// <summary>
/// Содержит все дополнительные материалы для генерируемых попапов
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PopupsConfig", order = 1)]
public class PopupsConfig : ScriptableObject
{
    [SerializeField] private Sprite _sadSmileIcon; public Sprite SadSmileIcon { get { return _sadSmileIcon; } }
}
