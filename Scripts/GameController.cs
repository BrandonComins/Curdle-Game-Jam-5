﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    
    private static int health = 10;
    private static int maxHealth = 10;
    private static float moveSpeed =5f;
    private static float fireRate = .5f;


    public static int Health {get => health; set => health = value;}
    public static int MaxHealth {get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed {get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate {get => fireRate; set => fireRate = value;}

    public Text healthText;
    
    void Awake(){
        if(instance = null){
            instance = this;
        }
    }

    void Update(){
        healthText.text = "Health: " + health;
    }

    public static void damagePlayer(int damage){
        health -= damage;
        if(Health <= 0){
            killPlayer();
        }
    }

    public static void healPlayer(int healAmount){
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void killPlayer(){
        
    }
}
