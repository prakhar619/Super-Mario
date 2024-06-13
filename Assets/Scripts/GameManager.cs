using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public int world {get; private set;}
    public int lives {get; private set;}

    public int coins{get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    public void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLvl(1);
    }

    private void LoadLvl(int world)
    {
        this.world = world;
        SceneManager.LoadScene($"{this.world}");
    }

    public void ResetLvl(int delay)
    {
        Invoke(nameof(ResetLvl), delay);
    }

    public void ResetLvl()
    {
        lives--;
        if(lives > 0)
        {
            LoadLvl(this.world);
        }
        else
        {
            GameOver();
        }
    }

    private void  GameOver()
    {
        NewGame();
    }

    public void NextLvl()
    {
        LoadLvl(world+1);
    }

    public void AddCoin()
    {
        coins++;
        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }
}
