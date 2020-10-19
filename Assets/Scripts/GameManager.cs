using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    float _PlayerHealth;
    float _timeRemaining;
    int _numCoins;
    float maxTime = 2 * 60;
    int maxHealth = 5;
    int totalCoinsInLevel;
    bool isInvulnerable = false;
    bool gameOver = false;

    void OnEnable()
    {
        DamagePlayerEvent.OnDamagePlayer += DecrementPlayerHealth;
    }

    void OnDisable()
    {
        DamagePlayerEvent.OnDamagePlayer -= DecrementPlayerHealth;
    }

    IEnumerator InvulnerableDelay()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1.0f);
        isInvulnerable = false;
    }

    public float TimeRemaining
    {
        get { return _timeRemaining;  }
        set { _timeRemaining = value; }
    }

    public float PlayerHealth
    {
        get { return _PlayerHealth; }
        set { _PlayerHealth = value; }
    }

    public int NumCoins
    {
        get { return _numCoins; }
        set { _numCoins = value; }
    }

    void Start()
    {
        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;
        totalCoinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    void DecrementPlayerHealth(GameObject player)
    {
        if (isInvulnerable)
        {
            return;
        }

        StartCoroutine(InvulnerableDelay());
        PlayerHealth--;

        if (PlayerHealth <= 0)
        {
            Restart();
        }
    }

    void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;
    }

    void Update()
    {
        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0)
        {
            Restart();
        }
    }

    public float GetPlayerHealthPercentage()
    {
        return PlayerHealth / maxHealth;
    }
}
