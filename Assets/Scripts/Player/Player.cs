using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [Range(5f, 20f)]
    public float speed = 10f;
    [Range(0f, 1f)]
    public float turnSmoothTime = 0.1f;
    [Range(9f, 100f)]
    public float weight = 70f;
    [Range(0f, 1f)]
    public float groundDistance = 0.4f;
    [Range(25f, 350f)]
    public int health = 100;
    [Range(10, 25)]
    public int damageEnemy = 15;
}
