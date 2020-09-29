using HexCS.Core;
using HexUN.Events;
using HexUN.Facade;
using HexUN.MonoB;
using HexUN.Render;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Abstract control for a single hex
    /// </summary>
    public abstract class AHex : AVisualFacade
    {
        [Header("Interaction (AHex)")]
        [SerializeField]
        private bool _interactable = false;

        [Header("Emissions (AHex)")]
        [SerializeField]
        protected EHexStateReliableEvent _onHexState = new EHexStateReliableEvent();

        [SerializeField]
        protected BooleanReliableEvent _onInteractionState = new BooleanReliableEvent();

        [SerializeField]
        protected MonoDataReliableEvent _onClick = new MonoDataReliableEvent();

        [Header("Debugging (AHex)")]
        [SerializeField]
        [Tooltip("The state of the hex")]
        private EHexState _state = EHexState.Neutral;

        #region API
        /// <summary>
        /// Is this hex interactable
        /// </summary>
        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value) return;
                _interactable = value;
                _onInteractionState.Invoke(_interactable);
                Debug.Log(value);
                Render();
            }
        }

        /// <summary>
        /// What is the current state of the hex
        /// </summary>
        public EHexState HexState
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                _onHexState.Invoke(_state);
                Render();
            }
        }

        /// <summary>
        /// Provides the transform of the central position that sits on top of the hex
        /// </summary>
        public abstract Transform GetBaseTransform();

        public void HandleClick()
        {
            _onClick.Invoke(this);
        }
        #endregion

        #region Protected API
        protected override void MonoStart()
        {
            base.MonoStart();
            HexState = _state;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            HexState = _state;
        }
        #endregion
    }
}