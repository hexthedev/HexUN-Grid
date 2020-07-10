using HexUN.Deps;
using HexUN.MonoB;
using HexUN.Input;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Uses Hoverable events and Hex Model events to determine the visualization of the hex
    /// </summary>
    public abstract class AHexView : MonoEnhanced
    {
        [Header("Dependencies (HexView)")]
        [SerializeField]
        private Object _hexControl = null;

        [SerializeField]
        private Object _hoverableEventProvider = null;

        [Header("Debugging (HexView)")]
        [SerializeField]
        protected bool _isInteractable;

        [SerializeField]
        protected EHexState _hexState;

        [SerializeField]
        protected EHoverableEvent _lastHoverEvent;


        protected IHexControl HexControl;
        protected IHoverableProvider HoverableProvider;

        protected override void MonoAwake()
        {
            ResolveDependencies();
            EventBindings.Add(HexControl.OnHexState.Subscribe(HandleHexState));
            EventBindings.Add(HoverableProvider.OnHoverableEvent.Subscribe(HandleHoverableEvent));
            EventBindings.Add(HexControl.OnInteractionState.Subscribe(HandleInteractionState));
        }

        protected virtual void OnValidate()
        {
            ResolveDependencies();
            ResolveState();
        }

        /// <summary>
        /// Handle visual changes that occur when the state of the toggle changes
        /// </summary>
        /// <param name="state"></param>
        protected abstract void HandleHexState(EHexState state);

        /// <summary>
        /// Handle visual changes that occur when the state of the interactablility changes
        /// </summary>
        /// <param name="interactionState"></param>
        protected abstract void HandleInteractionState(bool interactionState);

        /// <summary>
        /// Handle visual changes that occur when hover events are fired
        /// </summary>
        /// <param name="interactionState"></param>
        protected abstract void HandleHoverableEvent(EHoverableEvent hover);

        private void ResolveDependencies()
        {
            UTDependency.Resolve(ref _hexControl, out HexControl, this);
            UTDependency.Resolve(ref _hoverableEventProvider, out HoverableProvider, this);
        }

        private void ResolveState()
        {
            _isInteractable = HexControl.IsInteractable;
            _lastHoverEvent = HoverableProvider.LastHoverableEvent;
            _hexState = HexControl.HexState;
        }
    }
}
