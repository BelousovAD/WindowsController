namespace WindowsController.Feature.Window
{
    using UnityEngine;

    /// <summary>
    /// Компонент окна.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class Window : MonoBehaviour
    {
        private CanvasGroup _canvasGroup = null;
        private bool _isInFocus = true;

        /// <summary>
        /// Возвращает/устанавливает состояние фокусирования.
        /// </summary>
        public bool IsInFocus
        {
            get => _isInFocus;
            set
            {
                if (_isInFocus != value)
                {
                    _isInFocus = value;
                    _canvasGroup.interactable = value;
                }
            }
        }

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();
    }
}
