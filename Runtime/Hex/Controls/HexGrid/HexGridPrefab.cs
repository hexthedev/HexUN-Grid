using System.Collections;
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
        [Header("Options (HexGridViewPrefab)")]
        [SerializeField]
        [Tooltip("Defines how a coordinate is translated to world space")]
        private SHexCoordinateConverter _converter = null;
        
        [SerializeField]
        private AHex _prefab = null;

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

        protected override void HandleGenerateAHexes(SHexCoordinate[] coords, out Dictionary<SHexCoordinate, AHex> map)
        {
            map = new Dictionary<SHexCoordinate, AHex>();

            foreach (SHexCoordinate coord in coords)
            {
                AHex cellInstance = Instantiate(_prefab, GetPosition(coord), Quaternion.identity, transform);
                cellInstance.AddData(coord);
                cellInstance.transform.localScale *= _converter.HexScaleFactor;
            }
        }

        protected override void HandleFrameRender() { }
    }
}