using HexCS.Core;

namespace HexUN.Grid
{
    /// <summary>
    /// Contains calculations for a HexGrid with top point configuration. 
    /// The calculators purpose is to calculate Euclidian Hex poisitions and
    /// supply coordinates of related grid spaces based on sematically understandable
    /// enums related to a topPoint hex grid. 
    /// </summary>
    public class HexGridCalculator_TopPoint
    {
        /// <summary>
        /// The Outer Radius of each Grid Hex
        /// </summary>
        public float OuterRadius { get; private set; }

        /// <summary>
        /// The Inner Raius
        /// </summary>
        public float InnerRadius { get; private set; }

        /// <summary>
        /// Caches InnerRadius * 2, this is distance between hex centers
        /// </summary>
        public float HexCenterDistance { get; private set; }

        /// <summary>
        /// Caches InnerRadius * 2, this is distance between hex centers and also horzontal distance
        /// </summary>
        public float HexHorizontalDistance { get; private set; }

        /// <summary>
        /// Since hexes are vertically out of line, their y distance is InnerRadius * 1.5
        /// </summary>
        public float HexVerticalDistance { get; private set; }

        /// <summary>
        /// Point coordinates when hex origin is (0,0). 
        /// </summary>
        public readonly Vector2[] Points;

        /// <summary>
        /// Constructs Hex Calculator and caches important values to
        /// speed up calculations
        /// </summary>
        /// <param name="outerRadius"></param>
        public HexGridCalculator_TopPoint(float outerRadius)
        {
            OuterRadius = outerRadius;
            InnerRadius = outerRadius * UTHexGrid.cInnerRadiusModifier;
            HexCenterDistance = InnerRadius * 2;
            HexHorizontalDistance = HexCenterDistance;
            HexVerticalDistance = OuterRadius * 1.5f;

            Points = UTArray.ConstructArray<Vector2>(
                6,
                (i, e) => new Vector2(
                    outerRadius * UTHexGrid.TopPointModifiers[i].x,
                    outerRadius * UTHexGrid.TopPointModifiers[i].y
                )
            );
        }

        /// <summary>
        /// Uses a basic grid coordinate system to represent where a hex is in the complete grid. This gives a jaggered y. 
        /// Since grids are offset between rows or columns, it's assumed that the center of (0,0)
        /// is at position (0,0). (0,1) diagonally up right to the the original. (0, 2) is in line with the original.
        /// This format assumes a TopPoint configuration
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Vector2 EuclidianPosition(DiscreteVector2 coordinate)
        {
            return new Vector2(
                coordinate.X * HexHorizontalDistance + coordinate.Y % 2 * InnerRadius,
                coordinate.Y * HexVerticalDistance
            );
        }

        /// <summary>
        /// <para>Uses a hex coordinate system to represent where a hex is in the complete grid. 
        /// Since grids are offset between rows or columns, it's assumed that the center of (0,0,0)
        /// is at position (0,0). (0,1,-1) diagonally up right to the the original. (0, 2) is in line with the original.
        /// This format assumes a TopPoint configuration. See below for coords</para>
        /// 
        /// <para>
        ///   z___y
        ///  __z y__
        ///xxxx 0 xxxx
        ///  __y z__
        ///   y___z
        /// </para>
        /// 
        /// <para>
        /// This is remember about this system. x is 0 along the x axis and increase as you go above
        /// or decreases below. Same for z and y. All coordinates will always sum to zero. So coordinates
        /// can be understood as distance  from a line, not notches across the line
        /// </para>
        /// 
        /// <para>Using answer here to calculate: https://stackoverflow.com/questions/2459402/hexagonal-grid-coordinates-to-pixel-coordinates</para>
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Vector2 EuclidianPosition(SHexCoordinate coordinate)
        {
            return new Vector2(
                InnerRadius * (coordinate.X + coordinate.Y * 2),
                coordinate.X * HexVerticalDistance
            );
        }

        /// <summary>
        /// Returns neighbour coordinates of the input coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public SHexCoordinate[] GetNeighbours(SHexCoordinate coordinate)
        {
            return new SHexCoordinate[]
            {
                GetNeighbour(coordinate, EHexDirection.UP_RIGHT),
                GetNeighbour(coordinate, EHexDirection.RIGHT),
                GetNeighbour(coordinate, EHexDirection.DOWN_RIGHT),
                GetNeighbour(coordinate, EHexDirection.DOWN_LEFT),
                GetNeighbour(coordinate, EHexDirection.LEFT),
                GetNeighbour(coordinate, EHexDirection.UP_LEFT)
            };
        }

        /// <summary>
        /// Returns the neighbouring hex coordinate in a direction
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public SHexCoordinate GetNeighbour(SHexCoordinate coordinate, EHexDirection direction)
        {
            switch (direction)
            {
                case EHexDirection.UP_RIGHT: return new SHexCoordinate( coordinate.X + 1, coordinate.Y );
                case EHexDirection.RIGHT: return new SHexCoordinate(coordinate.X, coordinate.Y + 1);
                case EHexDirection.DOWN_RIGHT: return new SHexCoordinate(coordinate.X - 1, coordinate.Y + 1);
                case EHexDirection.DOWN_LEFT: return new SHexCoordinate(coordinate.X - 1, coordinate.Y);
                case EHexDirection.LEFT: return new SHexCoordinate(coordinate.X, coordinate.Y - 1);
                case EHexDirection.UP_LEFT: return new SHexCoordinate(coordinate.X + 1, coordinate.Y - 1);
            }

            return SHexCoordinate.Zero;
        }

        /// <summary>
        /// the 6 direction options when moving from one hex
        /// </summary>
        public enum EHexDirection
        {
            /// <summary>
            /// UP_RIGHT
            /// </summary>
            UP_RIGHT = 0,

            /// <summary>
            /// RIGHT
            /// </summary>
            RIGHT = 1,

            /// <summary>
            /// DOWN_RIGHT
            /// </summary>
            DOWN_RIGHT = 2,

            /// <summary>
            /// DOWN_LEFT
            /// </summary>
            DOWN_LEFT = 3,

            /// <summary>
            /// LEFT
            /// </summary>
            LEFT = 4,

            /// <summary>
            /// UP_LEFT
            /// </summary>
            UP_LEFT = 5
        }
    }
}
