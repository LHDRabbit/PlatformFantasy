using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // {enable the variable to become accessable from any classes, but set the instance by only GameManager class}
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }

    private void Awake() // if there is another Singleton, destroy this because Unity game allowes only 1 singletone to use.
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy() 
    {
        if (Instance == null)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame() // (A) Start a new game with 3 lives (life)
    {
        lives = 3;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage) // (A)-1 Load different world and changed stage
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()  // (B) if I have 3 stages for 1 world & there is more than 1 world.
    {
        if (world == 1 && stage == 3)
        {
            LoadLevel(world + 1, 1);
        }
        else
        {
            LoadLevel(world, stage + 1);
        }
        //LoadLevel(world, stage + 1); // if I have only 1 world with 3 stages
    }

    public void ResetLevel(float delay) // (C)-1 Delay ResetLevel happened when lost a live because I do not want to re-set a game immediately.
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel() // (C) Reset Level when lost a life
    {
        lives --;

        if( lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver() // (D) Re-start a new game
    {
        //Invoke(nameof(NewGame), 4f); // Delay the start of NewGame for 4 seconds because I do not want to re-start a new game immediately.
        //SceneManager.LoadScene("GameOver");
        NewGame();
    }
}
