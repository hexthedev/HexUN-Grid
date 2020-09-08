using HexUN.Design;
using HexUN.Input;
using UnityEngine;

namespace HexUN.Grid
{
    public class HexMaterial : AHex
    {
        [Header("Dependencies (HexViewMaterial)")]
        [SerializeField]
        [Tooltip("The renderer used to show the material of the hex")]
        private Renderer _renderer = default;

        [SerializeField]
        [Tooltip("Color Scheme (HexViewMaterial)")]
        private GameColorScheme _colorScheme = default;

        [Header("Options (HexViewMaterial)")]
        [SerializeField]
        [Tooltip("Scheme when hex is neutral")]
        private ESchemeColor _neutralScheme = ESchemeColor.Primary;

        [SerializeField]
        [Tooltip("Scheme when hex is hightlighted")]
        private ESchemeColor _highlightedScheme = ESchemeColor.Secondary;
        
        [Header("Debugging (HexViewMaterial)")]
        [SerializeField]
        private GameColor _neutralColor = default;

        [SerializeField]
        private GameColor _highlightedColor = default;

        [SerializeField]
        private EHoverableEvent _lastHoverEvent;

        #region Protected API
        protected override void MonoAwake() {
            base.MonoAwake();
            ResolveGameColoReferences();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ResolveGameColoReferences();
            ResolveMaterials();
        }

        protected override void HandleFrameRender()
        {
            ResolveMaterials();
        }
        #endregion

        #region Public API
        public void HandleHoverableEvent(EHoverableEvent hover)
        {
            _lastHoverEvent = hover;
            Render();
        }
        #endregion

        private void ResolveGameColoReferences()
        {
            _neutralColor = _colorScheme.GetGameColor(_neutralScheme);
            _highlightedColor = _colorScheme.GetGameColor(_highlightedScheme);
        }

        private void ResolveMaterials()
        {
           _renderer.material = ResolveScheme(HexState == EHexState.Neutral ? _neutralColor : _highlightedColor);
        }

        private Material ResolveScheme(GameColor color)
        {
            if (!Interactable)
            {
                return color.GreyedMaterial;
            }

            switch (_lastHoverEvent)
            {
                case EHoverableEvent.Absent: return color.BaseMaterial;
                case EHoverableEvent.Down: return color.LightMaterial;
                case EHoverableEvent.Hovering: return color.DarkMaterial;
            }

            return null; // should never happen
        }
    }
}