using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenHUD : MonoBehaviour
{
    private Action removeLife;
    [SerializeField] private Text healthText;
    [SerializeField] private Image damageScreen;
    private bool flashScreen = false;
    private int livesLeft = 3;

    void Start()
    {
        healthText.text = "Lives: " + livesLeft;
        removeLife += RemoveLifeOnScreen;
        EventSystem.Subscribe(EventType.ON_PLAYER_DAMAGED, removeLife);
    }
    private void Update()
    {
        if (flashScreen)
        {
            Color targetColor = new Color(1, 0, 0, 1);
            damageScreen.color = Color.Lerp(damageScreen.color, targetColor, 5 * Time.deltaTime);
            if (damageScreen.color.a >= 0.7f)
            {
                flashScreen = false;
                damageScreen.color = new Color(1, 0, 0, 0);
            }
        }
    }
    private void RemoveLifeOnScreen()
    {
        livesLeft -= 1;
        healthText.text = "Lives: " + livesLeft;
        flashScreen = true;
    }

}
