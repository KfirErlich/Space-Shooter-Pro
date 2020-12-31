using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //private or public references
    // Data types (int, float,bool,string)
    //optional value assigned

    [SerializeField]
    private float _speed= 3.5f;// _ for private 
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _ShieldPrefab;
    [SerializeField]
    private GameObject _LeftEngine,_RightEngine;
    [SerializeField]
    private float _fireRate=0.5f;
    private float _canFire=-1f;
    [SerializeField]
    private int _lives=3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool isTripleShotActive=false;
    [SerializeField]
    private bool isSpeedBoostActive=false;
    [SerializeField]
    private bool isShieldActive=false;
    [SerializeField]
    private int _score;
    [SerializeField]
    private bool isLeftEngineActive=false; 
    [SerializeField]
    private bool isRightEngineActive=false; 
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _LaserAudioClip;
    [SerializeField]
    private GameManager _gameManager;
    private UIManager _uiManager;


    void Start()
    {
        _spawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource=GetComponent<AudioSource>();
        //_gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();

       

        if(_spawnManager== null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if(_uiManager==null)
        {
           Debug.LogError("UI Manager is NULL");
        }
         if(_audioSource==null)
        {
           Debug.LogError("Audio Source on the player is NULL");
        }
        else
        {
            _audioSource.clip=_LaserAudioClip;
        }
       
        
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovment();

        fireLaser();
    }

    void calculateMovment()
    {
         float horizontalInput= Input.GetAxis("Horizontal");
        float verticalInput= Input.GetAxis("Vertical");
                    //equivelent to -> new Vector3(1,0,0);
        //transform.Translate(Vector3.right *horizontalInput*_speed* Time.deltaTime);// Time.deltaTime -> 1 Second
        //transform.Translate(Vector3.up *verticalInput * _speed* Time.deltaTime);
        Vector3 diraction= new Vector3(horizontalInput,verticalInput,0);

         transform.Translate(diraction* _speed* Time.deltaTime);

        transform.position= new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);

        if(transform.position.x >= 11.4f)
        {
            transform.position= new Vector3(-11.4f,transform.position.y,0);
        }
        else if(transform.position.x <= -11.4f)
        {
            transform.position= new Vector3(11.4f,transform.position.y,0);
        }
    }

    void fireLaser()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time>_canFire)
        {
            _canFire= Time.time+_fireRate;
            if(isTripleShotActive==true)
            {
                Instantiate(_TripleShotPrefab,transform.position,Quaternion.identity);
            }
            else
            {
                Instantiate(_LaserPrefab,transform.position+ new Vector3(0,1.05f,0),Quaternion.identity);
            }

           _audioSource.Play();
        }
    }

    public void Damage()
    {
        if(isShieldActive==true)
        {
            isShieldActive=false;
            _ShieldPrefab.SetActive(false);
            return;
        }
        _lives-=1;
        if(_lives==2) 
        {
            _LeftEngine.SetActive(true);
            isLeftEngineActive=true;
        }
        if(_lives==1)
        {
            _RightEngine.SetActive(true);
           isRightEngineActive=true;
        }

        _uiManager.UpdateLives(_lives);

        if(_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive=true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

     IEnumerator TripleShotPowerDownRoutine()
     {
             yield return new WaitForSeconds(5.0f);
             isTripleShotActive=false;
     }

     public void speedBoostActive()
     {
         isSpeedBoostActive=true;
          StartCoroutine(SpeedBoostDownRoutine());
     }
     IEnumerator SpeedBoostDownRoutine()
     {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive=false;
     }

     public void ShieldActive()
     {
         isShieldActive=true;
         _ShieldPrefab.SetActive(true);
     }

     public void AddScore(int points)
     {
         _score+= points;
         _uiManager.UpdateScore(_score);
     }

}
