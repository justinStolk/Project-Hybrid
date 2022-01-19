using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GamePaused { get; private set; }
    public float MouseSensitivity { get; private set; }

    [SerializeField] private GameObject settingsScreen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GamePaused = !GamePaused;
            settingsScreen.SetActive(!settingsScreen.activeSelf);
        }
    }

    public void EvaluateMouseSensitivity(float sensitivityValue)
    {
        MouseSensitivity = sensitivityValue;
    }

}
