using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour, IInteractable
{
    public List<PuzzleSwitch> switches;
    [SerializeField] private Stance[] stances;

    public void EvaluateSwitches()
    {
        for(int i = 0; i < switches.Count; i++)
        {
            if(switches[i].stance != stances[i])
            {
                Debug.Log("Wrong solution, switch " + (i + 1) + " is wrong. It's stance is: " + switches[i].stance + ", while it should be: " + stances[i]);
                return;
            }
        }
        Debug.Log("All switches are correct!");
    }

    public void Interact()
    {
        EvaluateSwitches();
    }
}
