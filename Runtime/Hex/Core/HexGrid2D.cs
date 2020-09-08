using HexCS.Core;

namespace HexUN.Grid
{
    /// <summary>
    /// Responsible for construction and management of a hex grid
    /// that has a 2D Square shape
    /// </summary>
    public class HexGrid2D
    {
        /// <summary>
        /// The underlying grid used by the HexGrid
        /// </summary>
        public SHexCoordinate[,] Grid;

        /// <summary>
        /// Construct a 2D array of Hexcoordinates to represent the grid
        /// </summary>
        /// <param name="dimensions"></param>
        public HexGrid2D(DiscreteVector2 dimensions)
        {
            Grid = SHexCoordinate.Grid2D(dimensions);
        }
    }
}
