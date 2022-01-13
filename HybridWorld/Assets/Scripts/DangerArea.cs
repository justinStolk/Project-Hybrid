using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArea : MonoBehaviour
{
    [SerializeField] private float damageInterval = 1.5f;
    private float timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FirstPersonController p = other.GetComponent<FirstPersonController>();
            p.TakeHit();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            if(timer >= damageInterval)
            {
                timer = 0;
                FirstPersonController p = other.GetComponent<FirstPersonController>();
                p.TakeHit();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }

}
