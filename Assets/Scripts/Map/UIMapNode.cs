using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class UIMapNode : MonoBehaviour
    {
        [SerializeField] private Color previousColor;
        [SerializeField] private Color currentColor;
        [SerializeField] private Color nextColor;

        [SerializeField] private Image spriteImage;
        [SerializeField] private Image baseImage;

        private Stage _stage;


        public void init(Stage stage)
        {
            _stage = stage;
            spriteImage.sprite = ResourcesManager.Instance.getSpriteForMapNode(stage.type);

            gameObject.SetActive(true);
        }

        public void onStageChangeStarted()
        {
            setColour();
        }

        public Image getIconImage() => spriteImage;

        private void setColour()
        {
            Color colour = Color.black;
            switch (_stage.getNodeType())
            {
                case NodeType.PREVIOUS: colour = previousColor; break;
                case NodeType.CURRENT:  colour = currentColor; break;
                case NodeType.NEXT:     colour = nextColor; break;
            }
            if (baseImage)
                baseImage.color = colour;
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
