using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerState
{
    Idle,
    WalkingToShelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem
}

public class CustomerFSM : MonoBehaviour
{
    public CustomerState currentState;
    public Timer timer;
    private NavMeshAgent agent;
    public bool IsMoveDone = false;

    public Transform target;        //이동할 목표 위치

    public Transform counter;
    public List<GameObject> targetPos = new List<GameObject>();

    public List<GameObject> myBox = new List<GameObject>();

    private static int nextPriority = 0;                    //다음 에이전트의 우선 순위
    private static readonly object priorityLock = new object();         //우선 순위 할당을 위한 동기화 객체

    public int boxesToPick = 5;
    private int boxesPicked = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
        currentState = CustomerState.Idle;
    }

    void AssignPriority()
    {
        lock (priorityLock)         //동기화 블록을 사용하여 우선 순위를 할당
        {
            agent.avoidancePriority = nextPriority; 
            nextPriority = (nextPriority + 1) % 100;        //NavMeshAgent 우선 순위 번위는 0~99
        }
    }

    void MoveToTarget()
    {
        IsMoveDone = false;
        if (target != null)
        {
            agent.SetDestination(target.position);          //agent에 목적지 타겟 설정
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update(Time.deltaTime);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                IsMoveDone = true;
            }
        }

        switch (currentState)
        {
            case CustomerState.Idle:
                Idle();
                break;
            case CustomerState.WalkingToShelf:
                WalkingToShelf();
                break;
            case CustomerState.PickingItem:
                PickingItem();
                break;
            case CustomerState.WalkingToCounter:
                WalkingToCounter();
                break;
            case CustomerState.PlacingItem:
                PlacingItem();
                break;
        }
    }
    void ChangeState(CustomerState nextState, float waitTime = 0.0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }
    void Idle()
    {
        if (timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            ChangeState(CustomerState.WalkingToShelf, 2.0f);
        }
    }
    void WalkingToShelf()
    {
        if (timer.IsFinished()&&IsMoveDone)
        {
            ChangeState(CustomerState.PickingItem, 2.0f);
        }
    }

    void PickingItem()
    {
        if (timer.IsFinished())
        {

            target = counter;
            MoveToTarget();
            ChangeState(CustomerState.WalkingToCounter, 2.0F);
        }
    }
    void WalkingToCounter()
    {
        if (timer.IsFinished() && IsMoveDone)
        {
            ChangeState(CustomerState.PlacingItem,2.0f);
        }
    }
    void PlacingItem()
    {
        if (timer.IsFinished())
        {
            ChangeState(CustomerState.Idle, 2.0f);
        }
    }
}
