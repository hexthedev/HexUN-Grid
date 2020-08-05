using HexCS.Core;
using HexUN.Input;
using UnityEngine;

namespace HexUN.Grid
{
    public abstract class AHexControl : AClickInteractor, IHexControl
    {
        [Header("Emissions (HexControl)")]
        [SerializeField]
        protected EHexStateReliableEvent _onHexState = new EHexStateReliableEvent();

        [Header("Debugging (HexControl)")]
        [SerializeField]
        [Tooltip("The state of the hex")]
        private EHexState _state = EHexState.Neutral;

        #region API
        /// <inheritdoc />
        public EHexState HexState
        {
            get => _state;
            set
            {
                if (_state != value) SetState(value);
            }
        }

        /// <inheritdoc />
        public IEventSubscriber<EHexState> OnHexState => _onHexState;

        /// <inheritdoc />
        public IEventSubscriber<EHexState> ProvideEvent => _onHexState;
        #endregion

        protected override void MonoStart()
        {
            SetState(_state);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            SetState(_state);
        }

        private void SetState(EHexState state)
        {
            _state = state;
            _onHexState.Invoke(_state);
        }
    }
}