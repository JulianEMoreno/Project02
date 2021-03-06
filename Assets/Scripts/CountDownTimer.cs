﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDownTimer : MonoBehaviour
{
    public float currentTime = 0f;
   public float startingTime = 180f;
    
    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        countdownText.color = Color.green;

        if(currentTime <=0)
        {
            currentTime = 0;
        }
    }
}
