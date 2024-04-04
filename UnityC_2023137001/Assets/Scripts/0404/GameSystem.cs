using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;
using TMPro;
using STORYGAME;

namespace STORYGAME
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]

        public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gamesystem = (GameSystem)target;

            if(GUILayout.Button("Reset Story Models"))
            {
                gamesystem.ResetStoryModels();
            }
            if(GUILayout.Button("Assing Text Component by name"))
            {
                GameObject textObject = GameObject.Find("StroyTextUI");
                if (textObject != null)
                {
                    Text textComponent = textObject.GetComponent<Text>();
                    if (textComponent != null)
                    {
                        gamesystem.textComponent = textComponent;
                    }
                }
            }
        }
    }

#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance;

        public float delay = 0.1f;
        private string currentText = "";
        public Text textComponent;

        private void Awake()
        {
            Instance = this;
        }
        public enum GAMESTATE
        {
            STORYSHOW,
            STORYEND,
            ENDMODE
        }

        public GAMESTATE state;

        public StoryTableObject[] storyModels;
        public StoryTableObject currentModels;
        private void Start()
        {
            currentModels= FindStoryModel(6);
            StartCoroutine(ShowText());
        }

        StoryTableObject FindStoryModel(int number)
        {
            StoryTableObject tempStoryModel = null;
            for(int i=0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)
                {
                    tempStoryModel = storyModels[i];
                    break;

                }
            }

            return tempStoryModel;
        }

        IEnumerator ShowText()
        {
            for(int i=0; i<= currentModels.storyText.Length; i++)
            {
                currentText = currentModels.storyText.Substring(0, i);
                textComponent.text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(delay);
        }
#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]

        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");
        }
#endif
    }
}

