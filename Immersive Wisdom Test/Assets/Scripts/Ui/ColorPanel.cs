using UnityEngine;

namespace Ui
{
    public class ColorPanel : MonoBehaviour
    {
        [SerializeField] private Color m_Color;
        public Color Color
        {
            get => m_Color;
            set
            {
                if (m_Color == value)
                {
                    return;
                }
                m_Color = value;
                UpdateCameraColor();
            }
        }

        private void OnValidate()
        {
            UpdateCameraColor();
        }

        private void UpdateCameraColor()
        {
            Camera.main.backgroundColor = Color;
        }
    }
}