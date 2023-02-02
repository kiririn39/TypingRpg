using Managers;

namespace Map
{
    public class Stage
    {
        public int       id   { get; private set; }
        public StageType type { get; private set; }


        public Stage(int id, StageType stage_type)
        {
            this.id = id;
            this.type = stage_type;
        }

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
