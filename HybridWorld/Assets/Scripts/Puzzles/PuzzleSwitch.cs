using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Stance { DOWN = 0, LEFT = 1, UP = 2, RIGHT = 3 }
public class PuzzleSwitch : MonoBehaviour, IInteractable
{
    public Stance stance { get; private set; }
    [SerializeField] private Transform lever;

    void Start()
    {
        stance = (Stance)((lever.localRotation.eulerAngles.y / 90) % 4);
        Debug.Log(lever.rotation.eulerAngles.y) ;
    }

    public void FlipSwitch()
    {
        switch (stance)
        {
            case Stance.UP:
                stance = Stance.RIGHT;
                break;
            case Stance.RIGHT:
                stance = Stance.DOWN;
                break;
            case Stance.DOWN:
                stance = Stance.LEFT;
                break;
            case Stance.LEFT:
                stance = Stance.UP;
                break;
        }
        lever.Rotate(0, 90, 0);
    }
    public void Interact()
    {
        FlipSwitch();
    }
}
