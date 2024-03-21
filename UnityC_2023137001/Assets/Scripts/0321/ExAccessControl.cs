using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{
    //public으로 선언된 변수는 다른 스크립트에서 직접 접근 가능
    public int publicValue;

    //private으로 선언된 변수는 같은 클래스 내에서만 접근 가능
    private int privateValue;

    //protected로 선언된 변수는 같은 클래스 및 파생 클래스에서 접근 가능
    protected int protectedvalue;

    //internal 로 선언된 변수는 같은 어셈블리(프로젝트 내 다른 스크립트) 내에서 접근 가능
    internal int internalValue;

    public class ParentClass
    {
        protected int protectedValueParent;
    }
    public class ChildClass : ParentClass   //parentClass 상속
    {
        void Start()
        {
            Debug.Log(protectedValueParent);
        }
    }
}
