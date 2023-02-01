using Assets.Scripts.SkillTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIRuneNode : MonoBehaviour
{
    [SerializeField] private UIRuneKey uiRuneKey = null;

    [SerializeField] private Image imgSelectedOutline = null;
    [SerializeField] private Image imgLineConnectorToParent = null;
    [SerializeField] private UIBattleActionIcon battleActionIcon = null;

    private RectTransform parentRectTransform = null;
    private RuneNodeData runeNodeData = null;
    private bool isSelected = false;


    public void preinit(Transform parentTransform, RuneNodeData runeNodeData )
    {
        this.parentRectTransform = parentTransform as RectTransform;
        this.runeNodeData        = runeNodeData;

        imgLineConnectorToParent.enabled = false;
        setRuneSelected(false);
        battleActionIcon.gameObject.SetActive(false);
    }

    public void init()
    {
        uiRuneKey.init(runeNodeData.runeKey);
        battleActionIcon.gameObject.SetActive(runeNodeData.RuneBattleActionInfo?.battleActionBase != null);
        battleActionIcon.init(runeNodeData?.RuneBattleActionInfo?.battleActionBase);

        bool isParentExist = parentRectTransform != null;

        imgLineConnectorToParent.enabled = isParentExist;
        if (!isParentExist)
            return;
        
        //imgLineConnectorToParent.rectTransform.position = (transform.position + parentRectTransform.position) / 2;
        float newWidth = Vector2.Distance(parentRectTransform.position, transform.position);
        imgLineConnectorToParent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newWidth);

        Vector3 moveDirection = transform.position - parentRectTransform.position; 
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;
        imgLineConnectorToParent.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void setRuneSelected( bool isSelected )
    {
        this.isSelected = isSelected;
        imgSelectedOutline.enabled = isSelected;
        if (parentRectTransform != null)
            imgLineConnectorToParent.color = isSelected ? Color.green : Color.gray;
    }

    private void OnDrawGizmos()
    {
        if (parentRectTransform == null)
            return;

        Gizmos.color = isSelected ? Color.green : Color.red;
        Gizmos.DrawLine(parentRectTransform.position, transform.position);
    }
}
