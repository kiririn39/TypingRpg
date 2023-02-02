using Assets.Scripts.SkillTree;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINewSkillButton : MonoBehaviour
{
   [SerializeField] private UIBattleActionIcon imgBattleActionIcon = null;
   [SerializeField] private TextMeshProUGUI    txtTitle            = null;
   [SerializeField] private TextMeshProUGUI    txtDescription      = null;
   [SerializeField] private Button             btnConfirm          = null;

   private bool onClickInited = false;
   private Action onClickAction = null;
   private RuneSequenceForBattleAction runeSequenceForBattleAction = null;


   public void init(RuneSequenceForBattleAction runeSequenceForBattleAction, Action onClickAction)
   {
      this.onClickAction = onClickAction;
      this.runeSequenceForBattleAction = runeSequenceForBattleAction;
      if (!onClickInited)
      {
         btnConfirm.onClick.AddListener(() => this.onClickAction?.Invoke());
         onClickInited = true;
      }

      txtTitle      .text = ResourcesManager.Instance.getSkillTitle      (runeSequenceForBattleAction.RuneBattleActionInfo.battleActionBase);
      txtDescription.text = ResourcesManager.Instance.getSkillDescription(runeSequenceForBattleAction.RuneBattleActionInfo.battleActionBase, runeSequenceForBattleAction.RuneKeys);
      imgBattleActionIcon.init(runeSequenceForBattleAction.RuneBattleActionInfo.battleActionBase);
   }
}
