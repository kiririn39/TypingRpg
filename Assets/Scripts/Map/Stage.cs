using System;
using DefaultNamespace;
using Managers;

namespace Map
{
    [Serializable]
    public class Stage
    {
        public int id;
        public StageType type;
        public EnemyConfig enemyConfig;
        

        public NodeType getNodeType()
        {
            int curStageIndex = StageManager.Instance.curStageIndex;
            if (id < curStageIndex)
                return NodeType.PREVIOUS;

            if ( id > curStageIndex )
                return NodeType.NEXT;

            return NodeType.CURRENT;
        }
        
    }
}
