using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float wanderRadius; // ���� ������ ����
    public float wanderTimer;  // ���� ������ Ÿ�̸�

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public float detectionRange = 5f; // ������Ʈ ���� ����
    public float pickupRange = 3f; // ������Ʈ ȹ�� ����
    public Transform[] objects; // ������ ������Ʈ��

    private bool isPursuingObject = false; // ���� ������Ʈ�� ���� ������ ����

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        // ������Ʈ ���� ���� Ȯ��
        CheckForObjects();

        // ������Ʈ�� ���� ���� �ƴ� ���� ���� ������ ����
        if (!isPursuingObject)
        {
            WanderRandomly();
        }
    }

    void WanderRandomly()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void CheckForObjects()
    {
        foreach (var obj in objects)
        {
            if (obj.gameObject.activeSelf)
            {
                // ������Ʈ�� �� ������ �Ÿ� ���
                Vector3 objectPosition = new Vector3(obj.position.x, transform.position.y, obj.position.z); // Y ���� ����
                float distance = Vector3.Distance(transform.position, objectPosition);

                if (distance <= detectionRange)
                {
                    isPursuingObject = true;
                    agent.SetDestination(objectPosition);

                    // ������Ʈ�� �������� ��
                    if (distance <= pickupRange)
                    {
                        StartCoroutine(RespawnObject(obj.gameObject, 7f)); // 7�� �ڿ� �����
                        isPursuingObject = false;
                        break;
                    }
                }
                else
                {
                    isPursuingObject = false;
                }
            }
        }
    }

    // NavMesh ������ ������ ��ġ�� ��ȯ�ϴ� �Լ�
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    IEnumerator RespawnObject(GameObject obj, float delay)
    {
        obj.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        obj.SetActive(true); // ������Ʈ ��Ȱ��ȭ
    }

}
