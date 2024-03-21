using UnityEngine;

// Класс слишком короткий, но на какой-то другой класс данную ответственность перенести сложно, так как иерархия проекта может измениться
// со временем
public class Managers : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
