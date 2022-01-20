using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float timeValue;
    public Text timerText;
    [SerializeField] private float timerScale = 1.2f;
    [SerializeField] private float timerHighlightTime = 1.5f;

    private void Start()
    {
        FloatEventSystem.Subscribe(EventType.ON_PUZZLE_ERROR, DecreaseTime);
    }
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
        if (timeValue == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    void DecreaseTime(float amount)
    {
        StartCoroutine(ShowTimerDecrease());
        timeValue -= amount;
    }

    IEnumerator ShowTimerDecrease()
    {
        timerText.color = Color.red;
        timerText.gameObject.transform.localScale = new Vector3(timerScale, timerScale, timerScale);
        yield return new WaitForSeconds(timerHighlightTime);
        timerText.color = Color.white;
        timerText.gameObject.transform.localScale = Vector3.one;
    }
}
