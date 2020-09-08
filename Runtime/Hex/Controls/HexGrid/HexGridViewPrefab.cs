﻿using System.Collections;
using System.Collections.Generic;
using HexCS.Core;

using HexUN.Deps;
using HexUN.MonoB;
using UnityEngine;
using Vec2 = HexCS.Core.Vector2;

namespace HexUN.Grid
{
    /// <summary>
    /// Controls the visual output of a hex gridw
    /// </summary>
    public class HexGridPrefab : AHexGrid
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

        private GameObject _gridParent;
        //private Dictionary<SHexCoordinate, IHexControl> _map = new Dictionary<SHexCoordinate, IHexControl>();

        protected override void OnValidate()
        {
            base.OnValidate();

            //if (_prefab.GetComponent<IHexControl>() == null)
            //{
            //    Debug.LogError($"Provided hex prefab does not have IHexControl component");
            //    _prefab = null;
            //}
        }

        private void HandleGenerate(SHexCoordinate[] coords)
        {
            if (_gridParent != null) HandleDestroy();
            _gridParent = new GameObject("Grid");
            _gridParent.transform.SetParent(transform);
            //_map.Clear();

            foreach(SHexCoordinate coord in coords)
            {
                GameObject cellInstance = Instantiate(_prefab, GetPosition(coord), Quaternion.identity, _gridParent.transform);
                cellInstance.transform.localScale *= _converter.HexScaleFactor;
                //IHexControl cellControl = cellInstance.GetComponent<IHexControl>();
                //_hexGridProvider.RegisterHexClick(coord, cellControl.OnClick);
                //_map[coord] = cellControl;
            }
        }

        public void HandleDestroy()
        {
            Destroy(_gridParent);
        }

        public void HandleHexState(SHexStateArgs args)
        {
            //if (!_map.TryGetValue(args.Coordinate, out IHexControl control)) Debug.LogError($"Failed to find hex {args.Coordinate} when setting hex state");
            //control.HexState = args.State;
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