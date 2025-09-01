using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Components.Tooltips
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
            Assert.IsNotNull(TooltipPrefab, "Tooltip prefab is not assigned (does not exist on scene)!");
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
            if (!TooltipPrefab) return;
            if (!TooltipPrefab.GameObjectReference) return;
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