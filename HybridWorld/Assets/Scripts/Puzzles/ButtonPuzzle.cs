using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzleButton> buttonsInOrder;
    [SerializeField] private int timerFailCost = 15;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material emissiveMat;
    [SerializeField] private Material clearedMat;
    private int indexer = 0;
    public AudioSource WrongBuzzer;
    public AudioSource DoorOpening;
    public AudioSource ButtonPress;
    private bool isCleared;

    private void Start()
    {
        foreach (PuzzleButton pb in buttonsInOrder)
        {
            pb.SetOwner(this);
        }
    }
    public void OnButtonCalled(PuzzleButton caller)
    {
        if (isCleared)
        {
            return;
        }
        ButtonPress.Play();

        if(caller == buttonsInOrder[indexer])
        {
            MeshRenderer[] children = caller.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer child in children)
            {
                if (child.gameObject.CompareTag("PButton"))
                {
                    child.material = emissiveMat;
                }
            }
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
            foreach(PuzzleButton button in buttonsInOrder)
            {
                MeshRenderer[] children = button.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer child in children)
                {
                    if (child.gameObject.CompareTag("PButton"))
                    {
                        child.material = defaultMat;
                    }
                }
            }
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
        foreach (PuzzleButton button in buttonsInOrder)
        {
            MeshRenderer[] children = button.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer child in children)
            {
                if (child.gameObject.CompareTag("PButton"))
                {
                    child.material = clearedMat;
                }
            }
        }
        isCleared = true;
    }


}
