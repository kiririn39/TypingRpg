using Assets.Scripts.SkillTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RuneKey=Assets.Scripts.SkillTree.RuneKey;

namespace RuneStack
{
    public class UIRuneStack : MonoBehaviour
    {
        [SerializeField] private List<UIRuneKey> uiRuneKeys = new List<UIRuneKey>();
        [SerializeField] private UIBattleActionIcon uiResultBattleAction = null;

        private List<RuneKey> selectedRuneKeys = new List<RuneKey>();
        private List<RuneKey> availableAsNextRuneKeys = new List<RuneKey>();

        private RuneTree runeTree = null;


        public void init(RuneTree runeTree)
        {
            this.runeTree = runeTree;
            updateData();
        }

        private void Update()
        {
            if (availableAsNextRuneKeys == null || !availableAsNextRuneKeys.Any())
                return;

            foreach (RuneKey runeKey in availableAsNextRuneKeys)
            {
                if (Input.GetKeyDown(runeKey.ToKeyCode()))
                {
                    selectedRuneKeys.Add(runeKey);
                    updateData();
                    return;
                }
            }
        }

        private void updateData()
        {
            availableAsNextRuneKeys = runeTree.getNextValidRuneKeys(selectedRuneKeys).ToList();
            updateUI();
        }

        private void updateUI()
        {
            for (int i = 0; i < uiRuneKeys.Count; i++)
            {
                bool curRuneAvailable = i < selectedRuneKeys.Count;
                uiRuneKeys[i].gameObject.SetActive(curRuneAvailable);
                if( curRuneAvailable )
                    uiRuneKeys[i].init(selectedRuneKeys[i]);
            }
        }
    }
}
