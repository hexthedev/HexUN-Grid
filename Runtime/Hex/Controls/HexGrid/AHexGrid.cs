using System;
using System.Collections.Generic;
using System.Linq;
using HexCS.Core;

using HexUN.Facade;
using UnityEngine;
using Event = HexCS.Core.Event;

namespace HexUN.Grid
{
    /// <summary>
    /// Manages a grid of visual hexes that can be controlled
    /// </summary>
    public abstract class AHexGrid : AVisualFacade
    {
        [Header("Debugging (HexGrid)")]
        [SerializeField]
        private bool _isGenerated = default;

        private SHexCoordinate[] _allHexes;
        private Dictionary<SHexCoordinate, AHex> _hexMap;

        #region API
        /// <inheritdoc />
        public bool IsGenerated => _isGenerated;

        /// <summary>
        /// All hexes
        /// </summary>
        public SHexCoordinate[] AllHexes {
            get => _isGenerated ? _allHexes : null;
            protected set
            {
                if (_allHexes == value) return;
                _allHexes = value;
            }
        }

        /// <summary>
        /// All the hex controls
        /// </summary>
        public AHex[] AllHexControls => _isGenerated ? _hexMap.Values.ToArray() : null;

        /// <summary>
        /// Generate a grid from a set of HexCoordinates
        /// </summary>
        /// <param name="allHexes"></param>
        public void GenerateGrid(SHexCoordinate[] allHexes)
        {
            if (_isGenerated) return;
            _allHexes = allHexes;
            HandleGenerateAHexes(_allHexes, out _hexMap);
            _isGenerated = true;
        }

        /// <summary>
        /// Try to get a hex if the grid contains it. 
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="hex"></param>
        /// <returns></returns>
        public bool TryGetHex(SHexCoordinate coord, out AHex hex)
            => _hexMap.TryGetValue(coord, out hex);

        /// <summary>
        /// Returns AHex for each valid hex coordinate. The returned array will be the same size
        /// as the input array. If no AHex exists for coordinate, sets index to null. Retursn null
        /// if input = null, or hex grid not yet generated
        /// </summary>
        /// <param name="hexes"></param>
        /// <returns></returns>
        public AHex[] GetHexes(params SHexCoordinate[] hexes)
        {
            if (!IsGenerated || hexes == null) return default;

            AHex[] ahexes = new AHex[hexes.Length];

            for(int i = 0; i< ahexes.Length; i++)
            {
                TryGetHex(hexes[i], out ahexes[i]);
            }
            return ahexes;
        }

        /// <summary>
        /// Returns coordinate array for all coordinates that are valid in the grid
        /// </summary>
        /// <param name="hexes"></param>
        /// <returns></returns>
        public SHexCoordinate[] ValidHexes(params SHexCoordinate[] hexes)
        {
            return hexes.Where(h => _allHexes.Contains(h)).ToArray();
        }

        /// <summary>
        /// Are all the hexes contained
        /// </summary>
        /// <param name="hexes"></param>
        /// <returns></returns>
        public bool ContainsHexes(params SHexCoordinate[] hexes)
        {
            if (!IsGenerated) return false;

            foreach(SHexCoordinate hex in hexes)
            {
                if (_allHexes.Contains(hex)) return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Handle the generation of the AHex map
        /// </summary>
        protected abstract void HandleGenerateAHexes(SHexCoordinate[] coords, out Dictionary<SHexCoordinate, AHex> map);
    }
}