using System;
using System.Collections.Generic;
using System.Linq;
using HexCS.Core;

using HexUN.Events;
using HexUN.Facade;
using HexUN.MonoB;
using UnityEngine;
using Event = HexCS.Core.Event;

namespace HexUN.Grid
{
    /// <summary>
    /// Control for a full hex grid
    /// </summary>
    public abstract class AHexGrid : AVisualFacade
    {
        [Header("Debugging (HexGridControl)")]
        [SerializeField]
        private bool _isGenerated = default;

#if UNITY_EDITOR
        [Header("Verbose Debug (HexGridControl)")]
        [SerializeField]
        private bool _allowVerboseDebug = true;
        
        [SerializeField]
        private string _OutClickedHex = default;

        [SerializeField]
        private string _BothHexSet = default;

        [SerializeField]
        private EHexState _BothHexSetState = default;

        [SerializeField]
        private List<string> _InHexSetMulti = default;
#endif
        private Event<SEuclidianPositionArgs> _onEuclidianPostionRequest = new Event<SEuclidianPositionArgs>();
        private Dictionary<SHexCoordinate, HexData> _map = new Dictionary<SHexCoordinate, HexData>();

        #region API
        /// <inheritdoc />
        public bool IsGenerated => _isGenerated;

        /// <inheritdoc />
        public void GenerateGrid(SHexCoordinate[] allHexes)
        {
            if (_isGenerated) DestroyGrid();

            foreach (SHexCoordinate coord in allHexes)
            {
                _map[coord] = new HexData() { Coordinate = coord, OnClickEvent = new Event() };
            }

            _isGenerated = true;
            _onGenerate.Invoke(_map.Keys.ToArray());
        }

        public void DestroyGrid()
        {
            _map.Clear();
            EventBindings.ClearAndUnsubscribeAll();
            _isGenerated = false;

            _onDestroyGrid.Invoke();
        }

        /// <inheritdoc />
        public SHexCoordinate[] GetHexes(Predicate<SHexCoordinate> query)
        {
            if (!IsGenerated) return new SHexCoordinate[0];

            List<SHexCoordinate> hexes = new List<SHexCoordinate>();

            foreach(SHexCoordinate coord in _map.Keys.ToArray())
            {
                if (query(coord)) hexes.Add(coord);
            }

            return hexes.ToArray();
        }

        /// <inheritdoc />
        public void RegisterHexClick(SHexCoordinate hex, IEventSubscriber onHexClick)
        {
            if (!ContainsHex(hex)) return;
            EventBindings.Add(onHexClick.Subscribe(InvokeHexClicked));

            void InvokeHexClicked()
            {
                _onHexClicked.Invoke(hex);
#if UNITY_EDITOR
                if (_allowVerboseDebug) _OutClickedHex = hex.Print();
#endif
            }
        }

        /// <inheritdoc />
        public void SetHexState(SHexCoordinate hex, EHexState state)
        {
            if (!ContainsHex(hex)) return;

            HexData data = _map[hex];
            if (data.State == state) return;
            data.State = state;
            _onHexState.Invoke(new SHexStateArgs { Coordinate = hex, State = state });

#if UNITY_EDITOR
            if (_allowVerboseDebug)
            {
                _BothHexSet = hex.Print();
                _BothHexSetState = state;
            }
#endif
        }

        /// <inheritdoc />
        public void SetHexStates(EHexState state, params SHexCoordinate[] hexCoordinates)
        {
            foreach(SHexCoordinate hex in hexCoordinates)
            {
                SetHexState(hex, state);
            }
        }

        /// <inheritdoc />
        public void SetAllStates(EHexState state) => SetHexStates(state, _map.Keys.ToArray());


        /// <inheritdoc />
        public bool ContainsHex(SHexCoordinate hex)
        {
            if (!IsGenerated) return false;
            return _map.ContainsKey(hex);
        }

        /// <inheritdoc />
        public bool ContainsHexes(params SHexCoordinate[] hexes)
        {
            foreach(SHexCoordinate hex in hexes)
            {
                if (!ContainsHex(hex)) return false;
            }

            return true;
        }

        public void RequestEuclianPosition(SHexCoordinate coordinate, Action<Vector3> callback)
        {
            _onEuclidianPostionRequest.Invoke(new SEuclidianPositionArgs() { Coordinate = coordinate, Callback = callback });
        }
        #endregion

        /// <summary>
        /// Groups events and coordinates
        /// </summary>
        private class HexData
        {
            /// <summary>
            /// The coordinate this event is related to
            /// </summary>
            public SHexCoordinate Coordinate;

            /// <summary>
            /// Should be invoked when hex at coordinate is clicked
            /// </summary>
            public IEvent OnClickEvent;

            /// <summary>
            /// The current state of the hex
            /// </summary>
            public EHexState State;
        }


#region Editor Debug
#if UNITY_EDITOR

        [ContextMenu("GenerateGrid (Play)")]
        private void CMGenerateGrid()
        {
            HexGrid2D grid = new HexGrid2D( new DiscreteVector2(10, 10) );
            SHexCoordinate[] hexes = new SHexCoordinate[grid.Grid.GetLength(1) * grid.Grid.GetLength(0)];

            for(int y = 0; y < grid.Grid.GetLength(1); y++)
            {
                for(int x = 0; x < grid.Grid.GetLength(0); x++)
                {
                    hexes[y * grid.Grid.GetLength(1) + x] = grid.Grid[x, y]; 
                }
            }

            GenerateGrid(hexes);
        }

        [ContextMenu("DestroyGrid (Play)")]
        private void CMDestroyGrid()
        {
            DestroyGrid();
        }

        [ContextMenu("SetHex (Play)")]
        private void CMSetHex()
        {
            SetHexState(UTSHexCoordinate.Read(_BothHexSet), _BothHexSetState);
        }

        [ContextMenu("SetHexMulti (Play)")]
        private void CMSetHexMulti()
        {
            SetHexStates(_BothHexSetState, _InHexSetMulti.Select( s => UTSHexCoordinate.Read(s)).ToArray());
        }

        [ContextMenu("SetHexAll (Play)")]
        private void CMSetHexAll()
        {
            SetAllStates(_BothHexSetState);
        }

        protected override void HandleFrameRender() => throw new NotImplementedException();
#endif
        #endregion
    }
}