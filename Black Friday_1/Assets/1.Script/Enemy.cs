using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float wanderRadius; // 랜덤 움직임 범위
    public float wanderTimer;  // 랜덤 움직임 타이머

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public float detectionRange = 5f; // 오브젝트 감지 범위
    public float pickupRange = 3f; // 오브젝트 획득 범위
    public Transform[] objects; // 감지할 오브젝트들

    private bool isPursuingObject = false; // 적이 오브젝트를 추적 중인지 여부

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        // 오브젝트 추적 상태 확인
        CheckForObjects();

        // 오브젝트를 추적 중이 아닐 때만 랜덤 움직임 실행
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
                // 오브젝트와 적 사이의 거리 계산
                Vector3 objectPosition = new Vector3(obj.position.x, transform.position.y, obj.position.z); // Y 축은 무시
                float distance = Vector3.Distance(transform.position, objectPosition);

                if (distance <= detectionRange)
                {
                    isPursuingObject = true;
                    agent.SetDestination(objectPosition);

                    // 오브젝트에 도달했을 때
                    if (distance <= pickupRange)
                    {
                        StartCoroutine(RespawnObject(obj.gameObject, 7f)); // 7초 뒤에 재등장
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

    // NavMesh 위에서 랜덤한 위치를 반환하는 함수
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
        obj.SetActive(false); // 오브젝트 비활성화
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        obj.SetActive(true); // 오브젝트 재활성화
    }

}
