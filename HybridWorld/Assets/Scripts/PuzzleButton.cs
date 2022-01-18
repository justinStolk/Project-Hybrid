using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    private ButtonPuzzle owner;

    public void SetOwner(ButtonPuzzle owner)
    {
        this.owner = owner;
    }
    public void OnButtonInteraction()
    {
        owner.OnButtonCalled(this);
    }


}
