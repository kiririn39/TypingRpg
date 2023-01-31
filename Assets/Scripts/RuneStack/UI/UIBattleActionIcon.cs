using DefaultNamespace;
using DefaultNamespace.BattleActions;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleActionIcon : MonoBehaviour
{
    [SerializeField] private Image imgBattleAction = null;


    public void init(BattleActionBase battleActionBase)
    {
        if (imgBattleAction)
            imgBattleAction.sprite = getSpriteForBattleAction(battleActionBase);
    }

    private Sprite getSpriteForBattleAction(BattleActionBase battleActionBase)
    {
        return ResourcesManager.Instance.getSpriteForBattleAction(battleActionBase?.GetType());
    }

    private Color getColorForBattleAction(BattleActionBase battleActionBase)//TEMP FOR TEST
    {
        switch (battleActionBase)
        {
        case AttackAction attackAction:
            return Color.red;
        case DefenceAction defenceAction:
            return Color.gray;
        case IdleAction idleAction:
            return Color.green;

        }

        return Color.magenta;
    }
}
