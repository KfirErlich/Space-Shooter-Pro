using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver= false;
    private SpawnManager _spawnManager;

    private void Update() 
    {
        _spawnManager=GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver==true)
        {
            SceneManager.LoadScene(1);//Current GameScene
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        _isGameOver=true;

    }
}
