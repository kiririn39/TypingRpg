using Assets.Scripts.SkillTree;
using DefaultNamespace;
using Managers;
using RuneStack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelRunes : MonoBehaviour
{
   public event Action<RuneBattleActionInfo> OnUseBattleAction = delegate{}; 


   [SerializeField] private UIRuneStack        uiRuneStack     = null;
   [SerializeField] private UIRuneTree         uiRuneTree      = null;
   [SerializeField] private UINewSkillSelector uiSkillSelector = null;

   [SerializeField] private Button btnCheatNewLvl = null;


   private void Awake()
   {
      uiRuneTree.init();

      uiRuneStack.init(uiRuneTree.RuneTree);
      uiRuneStack.OnStackChanged += runes => uiRuneTree.trySelectSequence(runes);
      uiRuneStack.OnUseBattleAction += skill => OnUseBattleAction(skill);

      uiSkillSelector.gameObject.SetActive(false);
      uiSkillSelector.OnSkillSelected += onSkillSelectedInPanel;

      btnCheatNewLvl?.onClick.AddListener(openNewSkillSelectorPanel);
   }

   public void openNewSkillSelectorPanel()
   {
      uiSkillSelector.gameObject.SetActive(true);
      uiSkillSelector.init(PlayerDatabase.Instance.availableSkillsForNextLvl);
   }

   private void onSkillSelectedInPanel(RuneSequenceForBattleAction newSkill)
   {
      uiSkillSelector.gameObject.SetActive(false);
      PlayerDatabase.Instance.addNewSkill(newSkill);
      uiRuneTree.RuneTree.addSequence(newSkill);
   }
}
