using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseThornProperties : MonoBehaviour {
    
    public float lifeTime;

    void Start(){
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Enemy"){
            collider.gameObject.GetComponent<EnemyController>().die();
            Destroy(gameObject);
            
        }
    }
}
