

namespace HexUN.Grid
{
    /// <summary>
    /// Arguments related to hex state switches
    /// </summary>
    public struct SHexStateArgs
    {
        /// <summary>
        /// The coordinate of the hex to switch the state of
        /// </summary>
        public SHexCoordinate Coordinate;

        /// <summary>
        /// Target state of the hex
        /// </summary>
        public EHexState State;
    }
}