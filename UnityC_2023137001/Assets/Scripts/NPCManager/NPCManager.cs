using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Entity_Dialog entity_Dialog;

    public void Start()
    {
        
    }

    public Entity_Dialog.Param GetParamData(int npc, int gamestate)
    {

        foreach(Entity_Dialog.Param param in entity_Dialog.sheets[0].list)
        {
            if (param.npc == npc && param.gamestate == gamestate)
            {
                return param;

            }
        }

        return null;
    }
}
