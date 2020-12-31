using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _speed=3.0f;
    [SerializeField]//0=Tripleshot, 1=Speed, 2=Sheild
    private int powerUpID;
    [SerializeField]
    private AudioClip _powerUpAudioClip;

    void Update()
    {
        transform.Translate(Vector3.down* _speed * Time.deltaTime);

        if(transform.position.y<= -4.5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
         if(other.tag=="Player")
         {
            Player player= other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerUpAudioClip,transform.position);
            if(player!=null)
            {
                switch(powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.speedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                    break;

                
                }
            }
            Destroy(this.gameObject);
         }
    }
}
