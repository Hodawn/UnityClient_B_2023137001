using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExEnemy : MonoBehaviour
{
    //적이 플레이어에 주는 피해량
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
            Debug.Log("타겟플레이어 공격");
            if (targetPlayer != null)
            {
                AttackPlayer(targetPlayer);
            }
        }
    }
}
