using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState {
    Wander,
    Follow,
    Die
};

public class EnemyController : MonoBehaviour{
    GameObject player;
    public EnemyState currentState; 

    public float range;
    public float speed;
    private bool chooseDirection = false;
    private bool dead = false;
    private Vector3 randomDirection;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.Wander;
    }

    void Update() {
        switch(currentState){
            case(EnemyState.Wander):
            wander();
            break;

            case(EnemyState.Follow):
            follow();
            break;

            case(EnemyState.Die):
            die();
            break;
        }
        if(isPlayerInRange(range) && currentState != EnemyState.Die){
            currentState = EnemyState.Follow;
        }
        else if(!isPlayerInRange(range) && currentState != EnemyState.Die){
            currentState = EnemyState.Wander;
        }
    }
    
    private bool isPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(){
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            nextRotation,
            Random.Range(.5f, 2.5f)
        );
        chooseDirection = false;
    }

    void wander(){
        if(!chooseDirection){
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(isPlayerInRange(range)){
            currentState = EnemyState.Follow;
        }
    }

    void follow(){
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            speed * Time.deltaTime
        );

    }

    public void die(){
        Destroy(gameObject);
    }
}
