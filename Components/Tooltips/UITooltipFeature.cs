using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.SimpleUI.Components.Tooltips
{
    /// <summary>
    ///     Feature that allows displaying tooltips of specified type
    /// </summary>
    public abstract class UITooltipFeature<TTooltipBase, TTooltipContext> : MonoBehaviour, IPointerEnterHandler,
        IPointerExitHandler,
        IPointerMoveHandler
        where TTooltipBase : UITooltipBase<TTooltipContext>
    {
        /// <summary>
        ///     Tooltip prefab
        /// </summary>
        [field: SerializeField] [NotNull] private TTooltipBase TooltipPrefab { get; set; } = null!;

        private void Awake()
        {
            // Load tooltip prefab if not assigned
            if (!TooltipPrefab) TooltipPrefab = FindAnyObjectByType<TTooltipBase>(FindObjectsInactive.Include);
            if (TooltipPrefab) return;
            
            Debug.LogError($"[UITooltipFeature] Tooltip prefab is not assigned and no instance found in scene for {name}. Disabling.");
            enabled = false;
        }

        public void OnPointerEnter([NotNull] PointerEventData eventData)
        {
            TooltipPrefab.ShouldBeVisible = true;
            TooltipPrefab.CachedContext = GetNewTooltipContext();
            TooltipPrefab.RequestRefresh();
        }

        public void OnPointerExit([NotNull] PointerEventData eventData)
        {
            TooltipPrefab.ShouldBeVisible = false;
            TooltipPrefab.RequestRefresh();
        }

        /// <summary>
        ///     Method to create new tooltip context
        /// </summary>
        protected abstract TTooltipContext GetNewTooltipContext();

        private void OnValidate()
        {
            if (!TooltipPrefab) return; // Optional, can be automatically assigned
            if (string.IsNullOrEmpty(TooltipPrefab.GameObjectReference.scene.name))
            {
                Debug.LogError("Tooltip prefab must be a scene object!");
            }
        }

        public void OnPointerMove([NotNull] PointerEventData eventData)
        {
            TooltipPrefab.ShouldBeVisible = true;
            TooltipPrefab.SetPosition(eventData.position);
        }
    }
}