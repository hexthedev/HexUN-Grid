using HexCS.Core;
using HexCS.Games;

namespace HexUN.Grid
{
    /// <summary>
    /// Interface for all controls required in a Hex Grid
    /// </summary>
    public interface IHexGridProvider
    {
        /// <summary>
        /// Invoked when the hex is regenerated
        /// </summary>
        IEventSubscriber<SHexCoordinate[]> OnGenerate { get; }

        /// <summary>
        /// Invoked when the hex grid is destroyed
        /// </summary>
        IEventSubscriber OnDestroyGrid { get; }

        /// <summary>
        /// Invoked when hex state changes
        /// </summary>
        IEventSubscriber<SHexStateArgs> OnHexState { get; }

        /// <summary>
        /// Invoked when a hex is clicked
        /// </summary>
        IEventSubscriber<SEuclidianPositionArgs> OnEuclidianPositionRequest { get; }

        /// <summary>
        /// Provide an event subscriber that can be used  by the provider 
        /// to drive the click event of a hex
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="onHexClick"></param>
        void RegisterHexClick(SHexCoordinate hex, IEventSubscriber onHexClick);
    }
}