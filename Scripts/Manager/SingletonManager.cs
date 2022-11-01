using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static object _lock = new object();
    protected static bool applicationIsQuitting = false;
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.Log("[½Ì±ÛÅæ] °ÔÀÓ Á¾·á " + typeof(T));
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return _instance;
                    }
                    if (_instance == null)
                    {
                        GameObject singleTon = new GameObject();
                        _instance = singleTon.AddComponent<T>();
                        singleTon.name = typeof(T).ToString();

                        DontDestroyOnLoad(singleTon);
                    }
                    else
                    {
                        DontDestroyOnLoad(_instance);
                    }
                }
                return _instance;
            }
        }
    }
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
