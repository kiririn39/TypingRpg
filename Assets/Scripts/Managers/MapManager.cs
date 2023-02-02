﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using Map;
using UnityEngine;

namespace Managers
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        [SerializeField] private float tweenHeight = 15;

        [SerializeField] private UIMapNode uiMapNodePrefab;

        [SerializeField] private RectTransform uiNodesHolder;
        [SerializeField] private RectTransform uiPlayerToken;

        private Vector3 _current_node_pos;

        private Dictionary<int, UIMapNode> _nodes_with_ids = new Dictionary<int, UIMapNode>();


        public void spawnMap(List<Stage> stages)
        {
            foreach (Stage stage in stages)
            {
                UIMapNode ui_map_node = Instantiate(uiMapNodePrefab, uiNodesHolder);
                NodeType node_type = stage.getNodeType();
                ui_map_node.init(stage);
                StageManager.Instance.stageChanged += ui_map_node.onStageChanged;
                RectTransform rect_transform = ui_map_node.transform as RectTransform;
                rect_transform.anchoredPosition += new Vector2(300 * (stage.id + 1), rect_transform.anchoredPosition.y);
                _nodes_with_ids[stage.id] = ui_map_node;
                if (node_type == NodeType.CURRENT)
                {
                    _current_node_pos = rect_transform.anchoredPosition;
                }
            }
        }

        private void onStageChanged()
        {
            if (_nodes_with_ids.Count <= StageManager.Instance.curStageIndex)
                return;
            RectTransform rect_transform = _nodes_with_ids[StageManager.Instance.curStageIndex].transform as RectTransform;
            _current_node_pos = rect_transform.anchoredPosition;
            tween();
        }

        private void tween()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Pause();
            var player_jump_up_tween = uiPlayerToken.DOAnchorPosY(tweenHeight, 0.5f).SetEase(Ease.OutBounce);
            player_jump_up_tween.onComplete += enableNodeIcon;
            sequence.Append(player_jump_up_tween);
            var lol = uiNodesHolder.DOAnchorPosX(_current_node_pos.x * -1, 2.0f).SetEase(Ease.InOutCubic);
            lol.onPlay += disableNodeIcon;
            sequence.Append(lol);
            var player_drop_down_tween = uiPlayerToken.DOAnchorPosY(-10, 1.0f).SetEase(Ease.OutCubic);
            sequence.Append(player_drop_down_tween);
            sequence.Play();

            void disableNodeIcon()
            {
                int cur_stage_index = StageManager.Instance.curStageIndex;
                _nodes_with_ids[cur_stage_index].getIconImage().DOFade(0.0f, 1.5f);
            }

            void enableNodeIcon()
            {
                // int cur_stage_index = StageManager.Instance.curStageIndex;
                // _nodes_with_ids[cur_stage_index - 1].getIconImage().DOFade(1.0f, 0.2f);
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            StageManager.Instance.stageChanged += onStageChanged;
        }
    }
}
