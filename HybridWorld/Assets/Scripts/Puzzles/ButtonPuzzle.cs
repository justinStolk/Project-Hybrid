using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzleButton> buttonsInOrder;
    [SerializeField] private int timerFailCost = 15;
    private int indexer = 0;
    public AudioSource WrongBuzzer;
    public AudioSource DoorOpening;
    public AudioSource ButtonPress;


    private void Start()
    {
        foreach (PuzzleButton pb in buttonsInOrder)
        {
            pb.SetOwner(this);
        }
    }
    public void OnButtonCalled(PuzzleButton caller)
    {
        ButtonPress.Play();

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

            WrongBuzzer.Play();


            FloatEventSystem.CallEvent(EventType.ON_PUZZLE_ERROR, timerFailCost);
            indexer = 0;
        }
    }
    private void OnPuzzleCleared()
    {
        DoorOpening.Play();
        Debug.Log("The puzzle has been cleared!");
        EventSystem.CallEvent(EventType.ON_BUTTON_PUZZLE_CLEARED);
        Destroy(this);
    }


}
