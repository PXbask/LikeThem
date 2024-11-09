using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T Instance;


    // ATTENTION: It overrides Awake method on the implementation class 
    // use 'protected virtual' and 'private override void' on implementation class
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        DontDestroyOnLoad(gameObject);
    }
}
