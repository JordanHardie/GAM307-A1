using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float _timeRemaining;
    private float maxTime = 2 * 60;
    public float TimeRemaining
    {
        get { return _timeRemaining;  }
        set { _timeRemaining = value; }
    }

    void Start()
    {
        TimeRemaining = maxTime;
    }

    void Update()
    {
        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
