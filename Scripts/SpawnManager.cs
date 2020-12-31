using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPreFab;
    [SerializeField]
    private GameObject[] Powerups;
    [SerializeField]
    private GameObject _EnemyContainer;

    private bool _stopSpawning=false;


    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while(!_stopSpawning)
        {
            Vector3 posToSpawn= new Vector3(Random.Range(-11.4f,11.4f),7,0);
           GameObject newEnemy= Instantiate(_EnemyPreFab,posToSpawn ,Quaternion.identity);
            newEnemy.transform.parent= _EnemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
           
        }
    }

     IEnumerator SpawnPowerUpRoutine()
     {
          yield return new WaitForSeconds(2.0f);
         while(_stopSpawning==false)
         {
            Vector3 posToSpawn= new Vector3 (Random.Range(-11,11),7,0);
            int RandomPowerup= Random.Range(0,3);
            Instantiate(Powerups[RandomPowerup], posToSpawn ,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
         }
     }


    public void onPlayerDeath()
    {
        _stopSpawning=true;
    }

}
