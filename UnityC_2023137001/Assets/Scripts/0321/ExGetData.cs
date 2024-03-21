using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExGetData : MonoBehaviour
{
    public Entity_Monster monster;
    //start is called before the first frame update
    private void Start()
    {

       foreach(Entity_Monster.Param param in monster sheets[0].list)
        {
            Debug.Log(param.index)
        }
    }
}
