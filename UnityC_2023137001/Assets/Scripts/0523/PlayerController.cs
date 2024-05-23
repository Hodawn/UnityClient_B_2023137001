using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public NPCManager npcManager;
    public GameStateManager gameStateManager;
    private CharacterController characterController;
    private Vector3 moveDirection;          //이동 방향 

    public float range = 2.0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
        moveDirection = move * moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //오버랩된 npc 오브젝트를 가져온다 (tag 사용)
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach(Collider collider in colliders)
            {
                if (collider.CompareTag("NPC"))
                {
                    //NPC 오브젝트에서 다이얼로그 데이터 가져오기

                    Entity_Dialog.Param npcParam =
                        npcManager.GetParamData(collider.GetComponent<NPCActor>().npcNumber, gameStateManager.gameState);

                    if (npcParam != null)
                    {
                        //대화 실행
                        Debug.Log($"Dialg: { npcParam.Dialog} ");
                        //작업 수행
                        if (npcParam.changeState > 0)
                        {
                            gameStateManager.gameState = npcParam.changeState;

                        }

                    }
                    else
                    {
                        Debug.LogWarning("해당하는 데이터가 없습니다. ");
                    }
                }
            }
        }
    }
}
