using System;
using HexCS.Core;
using HexCS.Games;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Interface for all controls required in a Hex Grid
    /// </summary>
    public interface IHexGridControl : IHexGridProvider
    {
        /// <summary>
        /// True if the grid has been generated. False
        /// if still awaiting a generation call. 
        /// </summary>
        bool IsGenerated { get; }

        /// <summary>
        /// Invoked when a hex is clicked
        /// </summary>
        IEventSubscriber<SHexCoordinate> OnHexClicked { get; }

        /// <summary>
        /// Generates a grid
        /// </summary>
        /// <param name="GridSize"></param>
        /// <param name="EHexGridGenerationMode"></param>
        void GenerateGrid(SHexCoordinate[] allHexes);

        /// <summary>
        /// Destroys the grid if it exists
        /// </summary>
        void DestroyGrid();

        /// <summary>
        /// Set the state of a hex in the hex grid
        /// </summary>
        /// <param name="state"></param>
        void SetHexState(SHexCoordinate hex, EHexState state);

        /// <summary>
        /// Set the state of a hex in the hex grid. If hexCoordinates is null, 
        /// sets all hexes
        /// </summary>
        /// <param name="state"></param>
        void SetHexStates(EHexState state, params SHexCoordinate[] hexCoordinates);

        /// <summary>
        /// Set the state of all hexes in the gird to a state
        /// </summary>
        /// <param name="state"></param>
        void SetAllStates(EHexState state);

        /// <summary>
        /// Returns all hexes that return true to query
        /// </summary>
        /// <param name="state"></param>
        SHexCoordinate[] GetHexes(Predicate<SHexCoordinate> _query);

        /// <summary>
        /// returns true if the provided coordinates are valid 
        /// in the grid
        /// </summary>
        /// <returns></returns>
        bool ContainsHexes(params SHexCoordinate[] hexes);

        /// <summary>
        /// returns true if the provided coordinate is valid in grid 
        /// in the grid
        /// </summary>
        /// <returns></returns>
        bool ContainsHex(SHexCoordinate hexes);

        /// <summary>
        /// Request eulclidian position
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        void RequestEuclianPosition(SHexCoordinate coordinate, Action<Vector3> callback);
    }
}