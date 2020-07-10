using System.Collections;
using System.Collections.Generic;
using HexUN.Design;
using HexUN.Systems.Grid;
using HexUN.Input;
using UnityEngine;

namespace HexUN.Grid
{
    public class HexViewMaterial : AHexView
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

        private bool _isChangedSinceLastFrame = false;

        protected override void MonoAwake() {
            ResolveGameColoReferences();
        }
        
        protected override void HandleHexState(EHexState state)
        {
            _hexState = state;
            _isChangedSinceLastFrame = true;
        }

        protected override void HandleHoverableEvent(EHoverableEvent hover)
        {
            _lastHoverEvent = hover;
            _isChangedSinceLastFrame = true;
        }

        protected override void HandleInteractionState(bool interactionState)
        {
            _isInteractable = interactionState;
            _isChangedSinceLastFrame = true;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ResolveGameColoReferences();
            ResolveMaterials();
        }

        private void Update()
        {
            if (_isChangedSinceLastFrame)
            {
                ResolveMaterials();
                _isChangedSinceLastFrame = false;
            }
        }

        private void ResolveGameColoReferences()
        {
            _neutralColor = _colorScheme.GetGameColor(_neutralScheme);
            _highlightedColor = _colorScheme.GetGameColor(_highlightedScheme);
        }

        private void ResolveMaterials()
        {
           _renderer.material = ResolveScheme(_hexState == EHexState.Neutral ? _neutralColor : _highlightedColor);
        }

        private Material ResolveScheme(GameColor color)
        {
            if (!_isInteractable)
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