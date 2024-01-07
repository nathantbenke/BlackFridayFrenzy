using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemy : Enemy // WanderingEnemy
{
    public RoamingEnemyBehaviour behaviour; //Current enemy behavior

    public Vector3 destinationPoint; //destination
    public float arriveDistance; //Distance representing the distance from the destination

    int roamingCount;
    private void Start()
    {
        //Choose Random Stall
        Stall randomStall = GetRandomStall();
        destinationPoint = randomStall.transform.position;
        MoveTo(destinationPoint);

        behaviour = RoamingEnemyBehaviour.walking;
    }

    private void Update()
    {
        //Enemy walking
        if (behaviour == RoamingEnemyBehaviour.walking)
        {
            //Enemy arrived some points
            if (Vector3.Distance(transform.position, destinationPoint) <= arriveDistance)
            {
                behaviour = RoamingEnemyBehaviour.looking;
                agent.isStopped = true;

                // Change status to "walking" after 2-3 seconds

                Invoke("EndLooking", Random.Range(2f, 3f));
                return;
            }
        }
        else if (behaviour == RoamingEnemyBehaviour.looking)
        {
            Invoke("EndLooking", Random.Range(2f, 3f));
            return;
        }
        else if (behaviour == RoamingEnemyBehaviour.exiting)
        {
            if (Vector3.Distance(transform.position, Market.Instance.exitTr.position) <= arriveDistance)
            {
                Destroy(gameObject);
            }
        }
    }
    void EndLooking()
    {
        roamingCount--;

        if (roamingCount <= 0)
        {
            behaviour = RoamingEnemyBehaviour.exiting;
            agent.isStopped = false;
            MoveTo(Market.Instance.exitTr.position);
            return;
        }

        Stall randomStall = GetRandomStall();

        destinationPoint = randomStall.transform.position;
        agent.isStopped = false;

        MoveTo(destinationPoint);
        behaviour = RoamingEnemyBehaviour.walking;
    }

    Stall GetRandomStall() // code for choosing random stall
    {
        int randomIdx = Random.Range(0, Market.Instance.stalls.Length);
        return Market.Instance.stalls[randomIdx];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, arriveDistance);
    }

}

//Define the behavior of roaming enemies
public enum RoamingEnemyBehaviour
{
    walking, //move
    looking, //stop
    exiting //go out
}