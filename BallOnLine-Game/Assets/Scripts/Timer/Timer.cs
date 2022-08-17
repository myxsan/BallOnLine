using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    float time;
    void Start()
    {
        time = Mathf.Epsilon;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        timerText.text = string.Format("{0:00.00}", time);
    }
}
