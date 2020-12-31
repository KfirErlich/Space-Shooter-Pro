using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed= 4.0f;
    
    private Player _player;

    private Animator _anim;
   [SerializeField]
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _enemyAudioClip;
    [SerializeField]
    private GameObject _LaserPrefab;

    private float _fireRate=3.0f;
    private float _canfire= -1;
    





    void Start()
    {
        _player= GameObject.Find("Player").GetComponent<Player>();
        if(_player==null)
        {
            Debug.LogError("Player is NULL");
        }
        _anim=GetComponent<Animator>();
        if(_anim==null)
        {
            Debug.LogError("_anim is NULL");
        }
        _AudioSource=GetComponent<AudioSource>();
         if(_AudioSource==null)
        {
           Debug.LogError("Audio Source on the player is NULL");
        }
        else
        {
            _AudioSource.clip=_enemyAudioClip;
        }
    }

    void Update()
    {
        enemyMovment();
        
         if(Time.time > _canfire)
        {
            _fireRate= Random.Range(3f,7f);
            _canfire= Time.time + _fireRate;
           GameObject enemyLaser=Instantiate(_LaserPrefab,transform.position,Quaternion.identity);
           Laser[] lasers= enemyLaser.GetComponentsInChildren<Laser>();

           for(int i=0; i<lasers.Length;i++)
           {
               lasers[i].AssignEnemyLaser();
           }
        }
    }

    void enemyMovment()
    {
        transform.Translate(Vector3.down *_speed * Time.deltaTime);

        if(transform.position.y<= -5.5f)
        {
            float randomX= Random.Range(-11.4f,11.4f);
           transform.position= new Vector3(randomX,7,0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
       if(other.tag=="Player")
       {
           Player player= other.transform.GetComponent<Player>();

           if(player !=null)
           {
               player.Damage();
           }
            _anim.SetTrigger("OnEnemyDeath");
            _speed=0;
             _AudioSource.Play();
           Destroy(this.gameObject,1f);
          
        
       } 
       else if (other.tag=="Laser")
       {
            Destroy(other.gameObject);
           if(_player!=null)
           {
               _player.AddScore(10);
           }

           _anim.SetTrigger("OnEnemyDeath");
           _speed=0;
            _AudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,1f);
       }

    }

}
