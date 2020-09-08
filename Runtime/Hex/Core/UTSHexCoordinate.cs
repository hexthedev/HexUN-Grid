using System.Linq;

namespace HexUN.Grid
{
    /// <summary>
    /// Hex Coordinate utilites
    /// </summary>
    public static class UTSHexCoordinate
    {
        /// <summary>
        /// Print the SHexCooridinate in the format [x, y, z]
        /// </summary>
        /// <param name="coord"></param>
        public static string Print(this SHexCoordinate c)
        {
            return $"[{c.X}, {c.Y}, {c.Z}]";
        }

        /// <summary>
        /// Read a hex coordinate from the print format [x, y, z].
        /// Will fail miserably if correct string not provided.
        /// NOT OPTIMAL, only really for qucik editor use.
        /// </summary>
        /// <param name="coord"></param>
        public static SHexCoordinate Read(string s)
        {
            int[] values = s.Substring(1, s.Length - 2).Split(',').Select((c) => int.Parse(c)).ToArray();
            return new SHexCoordinate(values[0], values[1]);
        }
    }
}