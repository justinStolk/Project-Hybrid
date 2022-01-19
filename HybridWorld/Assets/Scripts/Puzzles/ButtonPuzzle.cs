using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzleButton> buttonsInOrder;
    [SerializeField] private int timerFailCost = 15;
    private int indexer = 0;

    private void Start()
    {
        foreach(PuzzleButton pb in buttonsInOrder)
        {
            pb.SetOwner(this);
        }
    }
    public void OnButtonCalled(PuzzleButton caller)
    {
        if(caller == buttonsInOrder[indexer])
        {
            Debug.Log("Correct button!");
            if (indexer == buttonsInOrder.Count - 1)
            {
                OnPuzzleCleared();
                buttonsInOrder.Clear();
            }
            indexer++;
        }
        else
        {
            Debug.Log("Wrong button!");
            FloatEventSystem.CallEvent(EventType.ON_PUZZLE_ERROR, timerFailCost);
            indexer = 0;
        }
    }
    private void OnPuzzleCleared()
    {
        Debug.Log("The puzzle has been cleared!");
        EventSystem.CallEvent(EventType.ON_BUTTON_PUZZLE_CLEARED);
        Destroy(this);
    }


}
