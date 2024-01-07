using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public NavMeshAgent agent;

    //Items held by the enemy
    public List<Item> takenItem = new List<Item>();
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void MoveTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

    //Each enemy is dealt with differently when they arrive at the stall.
    public virtual void EnterStall(Stall stall)
    {

    }
}

public enum EnemyType
{
    roaming, //Wandering Enemy
    shopping, //Shoppers who just pick random items
    obnoxious //Shoppers who pick items which the player have to pick
}
