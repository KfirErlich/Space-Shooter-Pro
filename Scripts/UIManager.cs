using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImage; 
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _RestartButtonText;
    [SerializeField]
    private GameManager _gameManager;
    

    
   
    void Start()
    {
        _scoreText.text= "Score: "+0;
        _GameOverText.gameObject.SetActive(false);
        _RestartButtonText.gameObject.SetActive(false);
        _gameManager= GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager==null)
        {
            Debug.LogError("Game_Manager is NULL");
        }
    }

   public void UpdateScore(int playerScore)
   {
       _scoreText.text="Score: "+playerScore;
   }

   public void UpdateLives(int currLives)
   {
       _LivesImage.sprite= _liveSprites[currLives];
       if(currLives==0)
       {
           GameOverSequence();
       }
   }

   void GameOverSequence()
   {
       _gameManager.GameOver();
        _GameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _RestartButtonText.gameObject.SetActive(true);

        
   }

   IEnumerator GameOverFlicker()
   {
       while(true)
       {
            _GameOverText.text= "GAME OVER!";
            yield return new WaitForSeconds(0.5f);
            _GameOverText.text= "";
            yield return new WaitForSeconds(0.5f);
       }
   }

   
}
