using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Subscribe(EventType.ON_BUTTON_PUZZLE_CLEARED, OpenChamberDoor);
        EventSystem.Subscribe(EventType.ON_SWITCH_PUZZLE_CLEARED, OpenSwitchDoor);
    }

    private void OpenChamberDoor()
    {
        Animator[] anims = doors[0].GetComponentsInChildren<Animator>();
        foreach(Animator a in anims)
        {
            a.SetTrigger("OpenDoor");
        }
    }
    private void OpenSwitchDoor()
    {
        if(doors[1] == null)
        {
            return;
        }
        Animator[] anims = doors[1].GetComponentsInChildren<Animator>();
        foreach (Animator a in anims)
        {
            a.SetTrigger("OpenDoor");
        }
    }

}
