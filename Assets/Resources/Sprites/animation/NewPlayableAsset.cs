using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Playable/NewAsset")]
[System.Serializable]
public class NewPlayableAsset : PlayableAsset
{
    [SerializeField] private Animation _animation;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return Playable.Create(graph);
    }
}
