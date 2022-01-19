using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Stance { UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3 }
public class PuzzleSwitch : MonoBehaviour, IInteractable
{
    public Stance stance { get; private set; }
    [SerializeField] private Transform lever;

    void Start()
    {
        stance = (Stance)((lever.rotation.eulerAngles.x / 90) % 4);
        Debug.Log(lever.rotation.eulerAngles.x) ;
     /*   switch (lever.rotation.eulerAngles.x)
        {
            case 0:
                stance = Stance.UP;
                break;
            case 90:
                stance = Stance.RIGHT;
                break;
            case 180:
                stance = Stance.DOWN;
                break;
            case 270:
                stance = Stance.LEFT;
                break;
            default:
                Debug.LogError("Initial rotation incorrect!");
                break;

        } */
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
        lever.Rotate(90, 0, 0);
    }
    public void Interact()
    {
        FlipSwitch();
    }
}
