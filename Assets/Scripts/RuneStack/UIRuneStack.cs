using Assets.Scripts.SkillTree;
using DefaultNamespace;
using DG.Tweening;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
        public event Action<List<RuneKey>, bool> OnStackChanged = delegate { };


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

        public void clearSelected(bool tweenAsWrongCombination = false)
        {
            Debug.Log("Runes stack was cleared");
            if (tweenAsWrongCombination)
            {
                Color colorWrongCombination = new Color(0.67f, 0f, 0f);

                foreach (UIRuneKey t in uiRuneKeys.Where(it=> it.GetComponent<CanvasGroup>().alpha > 0.9f))
                {
                    var coroutine = DOTween.Sequence();
                    coroutine.Append(t.GetComponent<Image>().DOColor(colorWrongCombination, 0.1f));
                    coroutine.AppendInterval(0.3f);
                    coroutine.onComplete += () => t.GetComponent<Image>().color = Color.white;
                    coroutine.Play();
                }
            }

            selectedRuneKeys.Clear();
            updateData();
            updateUI();

            OnStackChanged(selectedRuneKeys, !tweenAsWrongCombination);

            
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

                OnStackChanged(selectedRuneKeys, true);
            }
            else
            {
                clearSelected(true);
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

            foreach (RuneKey runeKey in RuneKeyHelper.allValues)
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