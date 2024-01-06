using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingEnemy : Enemy
{
    public ShoppingEnemyBehaviour behaviour; 

    public Stall destinationStall; 
    public float arriveDistance; 

    int pickingCount;
    private void Start()
    {
        // How many stalls will this enemy stop at after pop up
        pickingCount = Random.Range(3, 7);
 
        destinationStall = GetRandomStall();
        MoveTo(destinationStall.transform.position);

        behaviour = ShoppingEnemyBehaviour.walking;
    }

    private void Update()
    {
        if (behaviour == ShoppingEnemyBehaviour.walking)
        {
            //Enemy arrived
            if (Vector3.Distance(transform.position, destinationStall.transform.position) <= arriveDistance)
            {
                behaviour = ShoppingEnemyBehaviour.picking;
                agent.isStopped = true;
                EnterStall(destinationStall);
                return;
            }
        }
        else if (behaviour == ShoppingEnemyBehaviour.exiting)
        {
            if (Vector3.Distance(transform.position, Market.Instance.exitTr.position) <= arriveDistance)
            {
                Destroy(gameObject);
            }
        }
    }


    public override void EnterStall(Stall stall)
    {
        StartCoroutine(CoPick(stall));
    }

    IEnumerator CoPick(Stall stall)
    {
        // Choose how many items to pick up
        int count = Random.Range(2, 4);

        for (int i = 0; i < stall.itemPlaces.Length; i++)
        {
            if (stall.itemPlaces[i].curItem != null)
            {

                Debug.Log(stall.itemPlaces[i].curItem.itemName);
                Item item = stall.itemPlaces[i].TakeItem();
                takenItem.Add(item);

                yield return new WaitForSeconds(1f);
                count--;
                if (count <= 0)
                    break;
            }
        }

        EndPicking();
    }

    void EndPicking()
    {
        pickingCount--;

        if (pickingCount <= 0)
        {
            behaviour = ShoppingEnemyBehaviour.exiting;
            agent.isStopped = false;
            destinationStall = null;
            MoveTo(Market.Instance.exitTr.position);
            return;
        }

        destinationStall = GetRandomStall();
        agent.isStopped = false;

        MoveTo(destinationStall.transform.position);
        behaviour = ShoppingEnemyBehaviour.walking;
    }

    Stall GetRandomStall()
    {
        int randomIdx = Random.Range(0, Market.Instance.stalls.Length);
        return Market.Instance.stalls[randomIdx];
    }

}
public enum ShoppingEnemyBehaviour
{
    walking,
    picking,
    exiting 
}