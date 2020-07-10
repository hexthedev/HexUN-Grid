using System.Collections;
using System.Collections.Generic;
using TobiasCSStandard.Games;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Options for Coordinate to World space conversion
    /// </summary>
    [CreateAssetMenu(fileName = "HexCoordinateConverter", menuName = "HexUN/Systems/Grid/HexCoordinateConverter")]
    public class SHexCoordinateConverter : ScriptableObject
    {
        private HexGridCalculator_TopPoint _calculator;

        [SerializeField]
        [Tooltip("What rotation do hexes in your hex grid have. ONLY TOP POINT CURRENTLY SUPPORTED")]
        EHexCoordinateConversionType _type = EHexCoordinateConversionType.TopPoint;

        [Header("Prefab Config")]
        [SerializeField]
        [Tooltip("The outer radius in unity units of the provided prefab")]
        private float _prefabOuterRadius = 1f;

        [Header("Output Config")]
        [SerializeField]
        [Tooltip("The outer radius in unity units of spawned hexes")]
        private float _outerRadius = 1f;

        [SerializeField]
        [Tooltip("The spacing between hexes in unity units")]
        private float _spacing = 1f;

        /// <summary>
        /// The factor by which the scale of the the prefab instantiated with scale 1,1,1
        /// needs to be multiplied in order to have the correct outer radius in world space
        /// </summary>
        /// <returns></returns>
        public float HexScaleFactor => _outerRadius/_prefabOuterRadius;

        /// <summary>
        /// Calculator used to convert coordinates to world space
        /// </summary>
        public HexGridCalculator_TopPoint Calculator
        {
            get
            {
                if(_calculator == null)
                {
                    _calculator = new HexGridCalculator_TopPoint(_outerRadius + _spacing);
                }

                return _calculator;
            }
        }
    }
}