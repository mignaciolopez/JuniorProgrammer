using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    //Encapsulation
    public static MainManager instance { get; private set; }

    //Encapsulation
    public bool isRunning { get; private set; }

    [SerializeField] private Button startDemoButton;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        isRunning = false;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnStartDemoClick()
    {
        isRunning = true;
        startDemoButton.gameObject.SetActive(false);
    }
}
