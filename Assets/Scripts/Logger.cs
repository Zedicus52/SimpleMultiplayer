
using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger Instance { get; private set; }
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public  void Log(string msg)
    {
        //log
        
    }

}
