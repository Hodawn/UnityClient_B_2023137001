using STORYGAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
    
{

    public int storyNumber;
    public Texture2D MainImage;
    public bool storyDone;


    public enum STORYTYPE
    {
        MAIN,
        SUB,
        SERIAL
    }
    public STORYTYPE storyType;

    [TextArea(10, 10)]//�ν����� text ���� ����
    public string storyText;
    [System.Serializable]

    public class Option
    {
        public string optionText;
        public string buttonText;

        public EventCheck eventCheck;
    }
    public class EventCheck
    {
        public int checkValue;

        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType type;

        public Result[] successResult;      //�������� ���� ���� �� �ݿ�
        public Result[] failResult;         //�������� ���� ���� �� �ݿ�
    }
    [System.Serializable]

    public class Result
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GoToEnding
        }
        public ResultType resultType;
        public int value;
        public Stats stats;
    }
}