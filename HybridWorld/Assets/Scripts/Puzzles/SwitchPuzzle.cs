using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour, IInteractable
{
    public List<PuzzleSwitch> switches;
    [SerializeField] private Stance[] stances;
    [SerializeField] private int timerFailCost = 15;
    [SerializeField] private Material clearedMat;
    public AudioSource wrongBuzzer;
    private bool isCleared;

    public void EvaluateSwitches()
    {
        if (isCleared)
        {
            return;
        }
        for(int i = 0; i < switches.Count; i++)
        {
            if(switches[i].stance != stances[i])
            {
                wrongBuzzer.Play();
                Debug.Log("Wrong solution, switch " + (i + 1) + " is wrong. It's stance is: " + switches[i].stance + ", while it should be: " + stances[i]);
                FloatEventSystem.CallEvent(EventType.ON_PUZZLE_ERROR, timerFailCost);
                return;
            }
        }
        EventSystem.CallEvent(EventType.ON_SWITCH_PUZZLE_CLEARED);
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in meshRenderers)
        {
            if (m.gameObject.CompareTag("SButton"))
            {
                m.material = clearedMat;
            }
        }
        Debug.Log("All switches are correct!");
        isCleared = true;
    }

    public void Interact()
    {
        EvaluateSwitches();
    }
}
