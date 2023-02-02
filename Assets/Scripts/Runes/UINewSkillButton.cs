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


   public void init(RuneBattleActionInfo battleActionInfo, Action onClickAction)
   {
      this.onClickAction = onClickAction;
      if (onClickInited)
      {
         btnConfirm.onClick.AddListener(() => this.onClickAction?.Invoke());
         onClickInited = true;
      }

      txtTitle      .text = ResourcesManager.Instance.getSkillTitle      (battleActionInfo?.battleActionBase);
      txtDescription.text = ResourcesManager.Instance.getSkillDescription(battleActionInfo?.battleActionBase);
      imgBattleActionIcon.init(battleActionInfo?.battleActionBase);
   }
}
