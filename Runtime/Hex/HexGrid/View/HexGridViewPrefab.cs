using System.Collections;
using System.Collections.Generic;
using TobiasCSStandard.Core;
using TobiasCSStandard.Games;
using HexUN.Deps;
using HexUN.MonoB;
using UnityEngine;
using Vec2 = TobiasCSStandard.Core.Vector2;

namespace HexUN.Grid
{
    /// <summary>
    /// Controls the visual output of a hex gridw
    /// </summary>
    public class HexGridViewPrefab : MonoDependent
    {
        [Header("Dependencies (HexGridViewPrefab)")]
        [SerializeField]
        private Object _hexGridProviderPrefab = null;

        [SerializeField]
        [Tooltip("Defines how a coordinate is translated to world space")]
        private SHexCoordinateConverter _converter = null;

        [Header("Options (HexGridViewPrefab)")]
        [SerializeField]
        private GameObject _prefab = null;

        private IHexGridProvider _hexGridProvider;
        private GameObject _gridParent;
        private Dictionary<SHexCoordinate, IHexControl> _map = new Dictionary<SHexCoordinate, IHexControl>();

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_prefab.GetComponent<IHexControl>() == null)
            {
                Debug.LogError($"Provided hex prefab does not have IHexControl component");
                _prefab = null;
            }
        }

        protected override void ResolveDependencies()
        {
            UTDependency.Resolve(ref _hexGridProviderPrefab, out _hexGridProvider, this);
        }

        protected override void ResolveEventBindings(EventBindingGroup ebs)
        {
            ebs.Add(_hexGridProvider.OnGenerate.Subscribe(HandleGenerate));
            ebs.Add(_hexGridProvider.OnDestroyGrid.Subscribe(HandleDestroy));
            ebs.Add(_hexGridProvider.OnHexState.Subscribe(HandleHexState));
            ebs.Add(_hexGridProvider.OnEuclidianPositionRequest.Subscribe(HandleEuclidianPositionRequest));
        }

        private void HandleGenerate(SHexCoordinate[] coords)
        {
            if (_gridParent != null) HandleDestroy();
            _gridParent = new GameObject("Grid");
            _gridParent.transform.SetParent(transform);
            _map.Clear();

            foreach(SHexCoordinate coord in coords)
            {
                GameObject cellInstance = Instantiate(_prefab, GetPosition(coord), Quaternion.identity, _gridParent.transform);
                cellInstance.transform.localScale *= _converter.HexScaleFactor;
                IHexControl cellControl = cellInstance.GetComponent<IHexControl>();
                _hexGridProvider.RegisterHexClick(coord, cellControl.OnClick);
                _map[coord] = cellControl;
            }
        }

        public void HandleDestroy()
        {
            Destroy(_gridParent);
        }

        public void HandleHexState(SHexStateArgs args)
        {
            if (!_map.TryGetValue(args.Coordinate, out IHexControl control)) Debug.LogError($"Failed to find hex {args.Coordinate} when setting hex state");
            control.HexState = args.State;
        }

        public void HandleEuclidianPositionRequest(SEuclidianPositionArgs args)
        {
            args.Callback(GetPosition(args.Coordinate));
        }

        /// <summary>
        /// Returns the euclidian position of a hex based on coords
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public Vector3 GetPosition(SHexCoordinate coord)
        {
            Vec2 pos = _converter.Calculator.EuclidianPosition(coord);
            return new Vector3(pos.X + transform.position.x, transform.position.y, pos.Y + transform.position.z);
        }
    }
}