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

    public Transform target;        //�̵��� ��ǥ ��ġ

    public Transform counter;
    public List<GameObject> targetPos = new List<GameObject>();

    public List<GameObject> myBox = new List<GameObject>();

    private static int nextPriority = 0;                    //���� ������Ʈ�� �켱 ����
    private static readonly object priorityLock = new object();         //�켱 ���� �Ҵ��� ���� ����ȭ ��ü

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
        lock (priorityLock)         //����ȭ ����� ����Ͽ� �켱 ������ �Ҵ�
        {
            agent.avoidancePriority = nextPriority; 
            nextPriority = (nextPriority + 1) % 100;        //NavMeshAgent �켱 ���� ������ 0~99
        }
    }

    void MoveToTarget()
    {
        IsMoveDone = false;
        if (target != null)
        {
            agent.SetDestination(target.position);          //agent�� ������ Ÿ�� ����
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

            //���� ����
            if (boxesPicked < boxesToPick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);            //���� ���� �������� ��� �ð� ����
            }

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
            if (myBox.Count != 0)
            {
                if(myBox.Count != 0)
                {
                    myBox[0].transform.position = counter.transform.position;
                    myBox[0].transform.parent = counter.transform;
                    myBox.RemoveAt(0);

                    timer.Set(0.1f);
                }

                else
                {
                    ChangeState(CustomerState.Idle, 2.0f);
                }
            }
            ChangeState(CustomerState.Idle, 2.0f);
        }
    }
}
