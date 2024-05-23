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

            if (GUILayout.Button("Reset Story Models"))
            {
                gamesystem.ResetStoryModels();
            }

        }
    }
#endif
}
