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

    [SerializeField] private UIRuneNode parent = null;
    [SerializeField] private List<UIRuneNode> children = new List<UIRuneNode>();

    private RuneNodeData runeNodeData = null;
    public bool isSelected { get; private set; } = false;


    public void preinit(RuneNodeData runeNodeData )
    {
        this.runeNodeData = runeNodeData;

        imgLineConnectorToParent.enabled = false;
        setRuneSelected(false);
        battleActionIcon.gameObject.SetActive(false);
    }

    public void init()
    {
        uiRuneKey.init(runeNodeData.runeKey);
        battleActionIcon.gameObject.SetActive(runeNodeData.RuneBattleActionInfo?.battleActionBase != null);
        battleActionIcon.init(runeNodeData?.RuneBattleActionInfo?.battleActionBase);

        bool isParentExist = parent != null;

        imgLineConnectorToParent.enabled = isParentExist;
        if (!isParentExist)
            return;
        
        //imgLineConnectorToParent.rectTransform.position = (transform.position + parentRectTransform.position) / 2;
        float newWidth = Vector2.Distance(parent.transform.position, transform.position);
        imgLineConnectorToParent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newWidth);

        Vector3 moveDirection = transform.position - parent.transform.position; 
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;
        imgLineConnectorToParent.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void setParent(UIRuneNode parent)
    {
        this.parent = parent;
        setRuneSelected(isSelected);
    }

    public void addChild(UIRuneNode child)
    {
        children.Add(child);
    }

    public void setRuneSelected( bool isSelected )
    {
        this.isSelected = isSelected;
        imgSelectedOutline.enabled = isSelected;
        if (parent != null)
            imgLineConnectorToParent.color = isSelected ? Color.white : (new Color(0.36f, 0.36f, 0.36f));
    }

    private void OnDrawGizmos()
    {
        if (parent == null)
            return;

        Gizmos.color = isSelected ? Color.green : Color.red;
        Gizmos.DrawLine(parent.transform.position, transform.position);
    }
}
