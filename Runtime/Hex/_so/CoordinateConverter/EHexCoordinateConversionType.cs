namespace HexUN.Grid
{ 
    /// <summary>
    /// How are hexes rotated. Important for calculating 
    /// hex space to world space
    /// </summary>
    public enum EHexCoordinateConversionType
    {
        /// <summary>
        /// Hexes where the top is pointy
        /// </summary>
        TopPoint,

        /// <summary>
        /// Hexes where the top is flat
        /// </summary>
        TopFlat
    }

}