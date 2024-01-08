using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;  

    public Rigidbody rb;

    [SerializeField] float moveSpeed; // player move speed
    [SerializeField] float rotationSpeed; //player rotation speed

    public Animator animator;

    public float pickUpRange;
    public Transform pickUpPoint;
    public Item takeItem; 


    //public List<string> needItemNames = new List<string>();

   // public List<Item> takenItems = new List<Item>();

    public List<PurchaseElement> purchaseList = new List<PurchaseElement>();

    private void Start()
    {
       
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        rb = GetComponent<Rigidbody>();


    }

    float onceAnimPlayingTimer;
    private void Update()
    {
        if (onceAnimPlayingTimer >0)
            onceAnimPlayingTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward.normalized * moveSpeed;
            animator.SetInteger("WalkState", 1);
            if(onceAnimPlayingTimer <=0)
            animator.Play("Walk");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward.normalized * moveSpeed;
            animator.SetInteger("WalkState", 1);
            if (onceAnimPlayingTimer <= 0)
                animator.Play("Walk");
        }
        else
        {
            rb.velocity = Vector3.zero;

            animator.SetInteger("WalkState", 0);
            if (onceAnimPlayingTimer <= 0)
                animator.Play("idle");
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("PunchLeft", -1,0);
            onceAnimPlayingTimer = 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("PunchRight",-1,0);
            onceAnimPlayingTimer = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUp();
        }
    }
    public void PickUp()
    {
        if (takeItem != null)
            return;

        animator.Play("PickUp", -1, 0);
        onceAnimPlayingTimer = 1;
        Collider[] cols = Physics.OverlapSphere(pickUpPoint.position, pickUpRange);

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Stall"))
            {
                Stall stall = cols[i].GetComponent<Stall>();

                bool found = FindItem(stall);

                if (found)
                    break;

            }
        }
    }

    bool FindItem(Stall stall)
    {
        for (int i = 0; i < stall.itemPlaces.Length; i++)
        {
            if (stall.itemPlaces[i].curItem == null)
                continue;

            takeItem = stall.itemPlaces[i].TakeItem();
            takeItem.transform.position = pickUpPoint.position;
            takeItem.transform.parent = pickUpPoint;
            
            Collider col = takeItem.GetComponentInChildren<Collider>();
            col.enabled = false;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (pickUpPoint == null)
            return;
        Gizmos.DrawWireSphere(pickUpPoint.position, pickUpRange);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Stall"))
    //    {
    //        Debug.Log("Player OnTriggerEnter Stall");
    //        EnterStall(other.GetComponent<Stall>());
    //    }
    //}
    ////When the player enter the stall 
    //public void EnterStall(Stall stall)
    //{
    //    for (int i = 0; i < stall.itemPlaces.Length; i++)
    //    {
    //        for (int j = 0; j < purchaseList.Count; j++)
    //        {
    //            int curCount = purchaseList[j].count;
    //            int takenCount = GetTakenItemCount(purchaseList[j].itemName);

    //            if (curCount <= takenCount)
    //                continue;

    //            if (stall.itemPlaces[i].curItem == null)
    //                continue;

    //            if (purchaseList[j].itemName == stall.itemPlaces[i].curItem.itemName)
    //            {
    //                Item item = stall.itemPlaces[i].TakeItem();
    //                takenItems.Add(item);
    //            }
    //        }
    //    }
    //}
    //int GetTakenItemCount(string iName)
    //{
    //    int count = 0;
    //    for (int i = 0; i < takenItems.Count; i++)
    //    {
    //        if (takenItems[i].itemName == iName)
    //        {
    //            count++;
    //        }
    //    }
    //    return count;
    //}

}

[System.Serializable]
public class PurchaseElement
{
    public string itemName;
    public int count; 
}