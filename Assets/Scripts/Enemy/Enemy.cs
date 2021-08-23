using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [Range(100f, 300f)]
    public int health = 150;

    // Patroling
    [Header("Patron Ramdom Enemy")]
    public Vector3 walkPoint;
    public bool walkpointSet;
    [Range(1f,10f)]
    public float walkpointRange = 5f;

    //Attacking
    [Header("Attack Player")]
    [Range(0f, 1f)]
    public float timeBetweenAttacks = 0.5f;
    [Range(1f, 10f)]
    public float distancePlayer = 5f;
    public bool alreadyAtack;
    public bool stopDistance;


    //States
    [Header("State Enemy")]
    [Range(5f, 25f)]
    public float singhtAttack = 10f;
    [Range(5f, 25f)]
    public float attackRange = 8f;
    public bool playerInSinghtRange, playerInAttackRange;
}
