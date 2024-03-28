using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace STORYGAME
{
    public class Enums
    {
        public enum StoryType : byte
        {
            MAIN,
            SUB,
            SERIAL
        }

        public enum EventType
        {
            NONE,
            GoToBattle=100,
            CheckSTR=1000,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA,
        }
        public enum ResultType
        {
            ChangeHp,
            ChangeSp,
            AddExperience=100,
            GoToShop=1000,
            GoToNextStory=2000,
            GoToRandomStory=3000,
            GoToEnding=10000
        }
    }


    [System.Serializable]
    public class Stats
    {
        //Ã¼·Â°ú Á¤½Å·Â
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        //±âº» ½ºÅÝ ¼³Á¤ (EX D&D)
        public int strength;        //strÈû
        public int dexterity;       //DEX ¹ÎÃ¸
        public int consitiution;        //Con°Ç°­
        public int Intelligence;    //INT Áö´É
        public int wisdom;          //WIS ÁöÇý
        public int Charisma;        //CHA ¸Å·Â

    }
}

