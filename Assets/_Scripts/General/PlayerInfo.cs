using System;

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
