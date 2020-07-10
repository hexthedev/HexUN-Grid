using TobiasCSStandard.Core;
using HexUN.Events;
using HexUN.Input;

namespace HexUN.Grid
{
    /// <summary>
    /// Provides events about a hex
    /// </summary>
    public interface IHexControl : IInteractionProvider, IClickProvider
    {
        /// <summary>
        /// Get or set the hex state of the hex
        /// </summary>
        EHexState HexState { get; set; }

        /// <summary>
        /// Invoked when the state of the hex changes
        /// </summary>
        IEventSubscriber<EHexState> OnHexState { get; }
    }
}