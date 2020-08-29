using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseThornProperties : MonoBehaviour {
    
    public float lifeTime;

    void Start(){
        StartCoroutine(DeathDelay());
    }

    private void Update() {
        Debug.Log(GetComponent<Collider>());    
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Enemy"){
            Debug.Log("HIT");
            col.gameObject.GetComponent<EnemyController>().die();
            Destroy(gameObject);
            
        }
    }
}
