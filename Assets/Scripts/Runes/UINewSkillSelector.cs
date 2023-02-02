using Assets.Scripts.SkillTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINewSkillSelector : MonoBehaviour
{
    public event Action<RuneBattleActionInfo> OnSkillSelected = delegate{};

    [SerializeField] private List<UINewSkillButton> btnsSelectNewSkill = new List<UINewSkillButton>();

    private bool isSkillAlreadySelected = false;


    public void init(List<RuneBattleActionInfo> skillsInfos)
    {
        isSkillAlreadySelected = false;
        for (int i = 0; i < btnsSelectNewSkill.Count; i++)
        {
            bool isVisible = i < skillsInfos.Count;
            btnsSelectNewSkill[i].gameObject.SetActive(isVisible);
            if (!isVisible)
                continue;

            btnsSelectNewSkill[i].init(
                skillsInfos[i]
            , () => onSkillClicked(skillsInfos[i])
            );
        }
    }
    
    private void onSkillClicked(RuneBattleActionInfo battleActionInfo)
    {
        OnSkillSelected(battleActionInfo);
        isSkillAlreadySelected = true;
    }
}
