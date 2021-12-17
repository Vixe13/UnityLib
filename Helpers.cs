using UnityEngine;

public static class Helpers
{
/// <summary>
/// Fonction générique
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// Generic type of variable
/// <param name="owner"></param>
/// <param name="obj"></param>
/// <returns></returns>
    public static bool TryFindObjectOfType<T>(this Object owner, out T obj)
        where T : Object
    {
        obj = Object.FindObjectOfType<T>();
        return obj != null;
    }
}
