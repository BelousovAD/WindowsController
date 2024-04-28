namespace WindowsController.Feature.Window
{
    using UnityEngine;

    /// <summary>
    /// Информация об окне.
    /// </summary>
    [CreateAssetMenu]
    public class WindowInfo : ScriptableObject
    {
        [SerializeField]
        private string _id = string.Empty;
        [SerializeField]
        private GameObject _windowPrefab = null;

        /// <summary>
        /// Возвращает идентификатор.
        /// </summary>
        /// <returns>Идентификатор.</returns>
        public string GetId() => _id;

        /// <summary>
        /// Возвращает префаб.
        /// </summary>
        /// <returns>Префаб.</returns>
        public GameObject GetWindowPrefab() => _windowPrefab;
    }
}
