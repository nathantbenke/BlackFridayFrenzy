using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObnoxiousEnemy : Enemy
{
    public ObnoxiousEnemyBehaviour behaviour; 

    public Stall destinationStall; 
    public float arriveDistance; 

    int pickingCount;
    private void Start()
    {
        pickingCount = Random.Range(3, 6);

        //How many stalls will this enemy stop at
        destinationStall = GetRandomStall();
        MoveTo(destinationStall.transform.position);

        behaviour = ObnoxiousEnemyBehaviour.walking;
    }

    private void Update()
    {
        if (behaviour == ObnoxiousEnemyBehaviour.walking)
        {
            //arrived
            if (Vector3.Distance(transform.position, destinationStall.transform.position) <= arriveDistance)
            {
                behaviour = ObnoxiousEnemyBehaviour.picking;
                agent.isStopped = true;
                EnterStall(destinationStall);
                return;
            }
        }
        else if (behaviour == ObnoxiousEnemyBehaviour.exiting)
        {
            if (Vector3.Distance(transform.position, Market.Instance.exitTr.position) <= arriveDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    //arrive in front of stall
    public override void EnterStall(Stall stall)
    {
        StartCoroutine(CoPick(stall));
    }

    IEnumerator CoPick(Stall stall)
    {
        //Take all items in the stall
        for (int i = 0; i < stall.itemPlaces.Length; i++)
        {
            if (stall.itemPlaces[i].curItem != null)
            {
                Debug.Log(stall.itemPlaces[i].curItem.itemName);
                Item item = stall.itemPlaces[i].TakeItem();
                takenItem.Add(item);

                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(2f);
        EndPicking();
    }

    void EndPicking()
    {
        pickingCount--;

        if (pickingCount <= 0)
        {
            behaviour = ObnoxiousEnemyBehaviour.exiting;
            agent.isStopped = false;
            destinationStall = null;
            MoveTo(Market.Instance.exitTr.position);
            return;
        }

        destinationStall = GetRandomStall();
        agent.isStopped = false;

        MoveTo(destinationStall.transform.position);
        behaviour = ObnoxiousEnemyBehaviour.walking;
    }

    Stall GetRandomStall()
    {

        int randonIdx = Random.Range(0, Player.Instance.purchaseList.Count);
        PurchaseElement e = Player.Instance.purchaseList[randonIdx];

        List<Stall> stalls = Market.Instance.GetStalls(e.itemName);
        randonIdx = Random.Range(0, stalls.Count);

        return stalls[randonIdx];
    }

}

public enum ObnoxiousEnemyBehaviour
{
    walking, 
    picking, 
    exiting 
}
