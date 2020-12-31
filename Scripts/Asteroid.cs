using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _Rotatespeed= 21.0f;
    [SerializeField]
    private GameObject _ExplosionPreFab;
    private SpawnManager _SpawnManager;

    private void Start()
    {
      _SpawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*_Rotatespeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Laser")
        {
            Instantiate(_ExplosionPreFab,transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            _SpawnManager.StartSpawning();
            Destroy(this.gameObject,0.2f);
        }




    }

    
        

        

    
}
