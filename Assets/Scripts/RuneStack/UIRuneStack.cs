using Assets.Scripts.SkillTree;
using DefaultNamespace;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using RuneKey = Assets.Scripts.SkillTree.RuneKey;

namespace RuneStack
{
    public class UIRuneStack : MonoBehaviour, UIRuneStack.ISkillTrigger
    {
        public interface ISkillTrigger
        {
            event Action<RuneBattleActionInfo> OnUseBattleAction;
        }


        public event Action<RuneBattleActionInfo> OnUseBattleAction = delegate { };
        public event Action<List<RuneKey>> OnStackChanged = delegate { };


        [SerializeField] private bool isAutoUse = false;
        [SerializeField] private List<UIRuneKey> uiRuneKeys = new List<UIRuneKey>();
        [SerializeField] private UIBattleActionIcon uiResultBattleAction = null;
        [FormerlySerializedAs("playerController")]
        [SerializeField] private BattleCharacter playerCharacter = null;

        [SerializeField] private UINewSkillSelector playerSkillList = null;

        private RuneBattleActionInfo runeBattleActionInfo = null;
        private List<RuneKey> selectedRuneKeys = new List<RuneKey>();
        private List<RuneKey> availableAsNextRuneKeys = new List<RuneKey>();

        private RuneTree runeTree = null;


        public void init(RuneTree runeTree)
        {
            (playerCharacter?.controllerBase as PlayerCharacterController)?.init(this);

            this.runeTree = runeTree;
            updateData();
            updateUI();
        }

        public void clearSelected()
        {
            Debug.Log("Runes stack was cleared");
            selectedRuneKeys.Clear();
            updateData();
            updateUI();

            OnStackChanged(selectedRuneKeys);
        }

        public void tryUseBattleAction()
        {
            Debug.Log(nameof(tryUseBattleAction));
            if (runeBattleActionInfo == null)
            {
                Debug.Log("There is no battle action for this sequence");
                return;
            }

            Debug.Log($"Battle Action was used: {runeBattleActionInfo}");
            OnUseBattleAction(runeBattleActionInfo);
            clearSelected();
        }

        private void trySelectRuneKey(RuneKey runeKey)
        {
            if (availableAsNextRuneKeys.Contains(runeKey))
            {
                selectedRuneKeys.Add(runeKey);
                updateData();
                updateUI();

                OnStackChanged(selectedRuneKeys);
            }
        }

        private void Update()
        {
            if (isAutoUse && runeBattleActionInfo != null && availableAsNextRuneKeys != null &&
                !availableAsNextRuneKeys.Any())
                tryUseBattleAction();

            handleInput();
        }

        private void updateData()
        {
            availableAsNextRuneKeys = runeTree.getNextValidRuneKeys(selectedRuneKeys).ToList();
            runeTree.isSequenceValid(selectedRuneKeys, out runeBattleActionInfo);
        }

        private void updateUI()
        {
            for (int i = 0; i < uiRuneKeys.Count; i++)
            {
                bool curRuneAvailable = i < selectedRuneKeys.Count;
                uiRuneKeys[i].GetComponent<CanvasGroup>().alpha = curRuneAvailable ? 1 : 0.3f; 
                uiRuneKeys[i].init(curRuneAvailable? selectedRuneKeys[i] : RuneKey.NONE);
            }

            uiResultBattleAction.init(runeBattleActionInfo?.battleActionBase);
        }

        private void handleInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerSkillList.gameObject.SetActive(true);
                playerSkillList.init(PlayerDatabase.Instance.myCurrentSkillSentences.ToList());
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                playerSkillList.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (playerCharacter == null)
                {
                    Debug.Log("DEBUG USE");
                    tryUseBattleAction();
                }
                else
                if (playerCharacter.DelayNormalized > 0.9999)
                    tryUseBattleAction();
                else
                    Debug.Log("COOLDOWN");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                clearSelected();

            if (availableAsNextRuneKeys == null || !availableAsNextRuneKeys.Any())
                return;

            foreach (RuneKey runeKey in availableAsNextRuneKeys)
            {
                if (Input.GetKeyDown(runeKey.ToKeyCode()))
                {
                    trySelectRuneKey(runeKey);
                    return;
                }
            }
        }
    }
}