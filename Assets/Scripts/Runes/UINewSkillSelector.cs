using Assets.Scripts.SkillTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINewSkillSelector : MonoBehaviour
{
    public event Action<RuneSequenceForBattleAction> OnSkillSelected = delegate{};

    [SerializeField] private List<UINewSkillButton> btnsSelectNewSkill = new List<UINewSkillButton>();

    private bool isSkillAlreadySelected = false;


    public void init(List<RuneSequenceForBattleAction> skillsInfos)
    {
        isSkillAlreadySelected = false;
        for (int i = 0; i < btnsSelectNewSkill.Count; i++)
        {
            bool isVisible = i < skillsInfos.Count;
            btnsSelectNewSkill[i].gameObject.SetActive(isVisible);
            if (!isVisible)
                continue;

            RuneSequenceForBattleAction skill = skillsInfos[i];
            btnsSelectNewSkill[i].init(
                skill
            , () => onSkillClicked(skill)
            );
        }
    }
    
    private void onSkillClicked(RuneSequenceForBattleAction battleActionInfo)
    {
        OnSkillSelected(battleActionInfo);
        isSkillAlreadySelected = true;
    }
}
