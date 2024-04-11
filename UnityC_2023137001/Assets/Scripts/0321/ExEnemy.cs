using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExEnemy : MonoBehaviour
{
    //���� �÷��̾ �ִ� ���ط�
    public int damage = 20;
    public ExPlayer targetPlayer;
    
    public void AttackPlayer(ExPlayer player)
    {
        player.TakeDamage(damage);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Ÿ���÷��̾� ����");
            if (targetPlayer != null)
            {
                AttackPlayer(targetPlayer);
            }
        }
    }
}
