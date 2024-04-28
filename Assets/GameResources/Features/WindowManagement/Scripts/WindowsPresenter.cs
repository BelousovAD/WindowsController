namespace WindowsController.Feature.WindowManagement
{
    using System.Collections.Generic;
    using UnityEngine;
    using WindowsController.Feature.Window;

    /// <summary>
    /// Контроллер окон.
    /// </summary>
    public sealed class WindowsPresenter : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent = null;
        [SerializeField]
        private List<WindowInfo> _windowInfos = new List<WindowInfo>();
        [SerializeField]
        private string _firstWindow = string.Empty;

        private Window _currentWindow = null;

        private void SetCurrentWindow(Window value)
        {
            if (_currentWindow != value)
            {
                if (_currentWindow != null)
                {
                    _currentWindow.IsInFocus = false;
                }

                _currentWindow = value;
            }
        }

        private Window _nextWindow = null;
        private Window NextWindow
        {
            get => _nextWindow;
            set
            {
                if (_nextWindow != value)
                {
                    _nextWindow = value;

                    if (_nextWindow != null)
                    {
                        _nextWindow.IsInFocus = true;
                    }
                }
            }
        }

        /// <summary>
        /// Открывает окно с заданным идентификатором.
        /// </summary>
        /// <param name="windowId">Идентификатор окна.</param>
        public void OpenWindow(string windowId)
        {
            Transform nextWindowTransform = _parent.Find(windowId);

            if (nextWindowTransform != null)
            {
                nextWindowTransform.SetAsFirstSibling();
                NextWindow = nextWindowTransform.GetComponent<Window>();
            }
            else
            {
                WindowInfo windowInfo = FindWindowInfo(windowId);

                if (windowInfo != null)
                {
                    GameObject nextWindowInstance = Instantiate(windowInfo.GetWindowPrefab(), _parent);
                    nextWindowInstance.GetComponent<ButtonOpenWindow>().Initialize(this);
                    NextWindow = nextWindowInstance.GetComponent<Window>();
                }
                else
                {
                    Debug.LogError($"Cannot open window: " +
                        $"WindowInfo with ID \"{windowId}\" does not exist.");

                    return;
                }
            }

            SetCurrentWindow(NextWindow);
        }

        /// <summary>
        /// Закрывает окно, которое находится в фокусе.
        /// </summary>
        public void CloseWindow()
        {
            int childCount = _parent.childCount;
            if (childCount > 1)
            {
                NextWindow = _parent.GetChild(childCount - 2).GetComponent<Window>();
            }
            else
            {
                NextWindow = null;
            }

            Destroy(_currentWindow.gameObject);
            SetCurrentWindow(NextWindow);
        }

        private void Start() => OpenWindow(_firstWindow);

        private WindowInfo FindWindowInfo(string windowId)
        {
            foreach (WindowInfo windowInfo in _windowInfos)
            {
                if (windowInfo.GetId().Equals(windowId))
                {
                    return windowInfo;
                }
            }

            return null;
        }
    }
}
