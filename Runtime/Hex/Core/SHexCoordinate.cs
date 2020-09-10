using System.Collections.Generic;
using HexCS.Core;

namespace HexUN.Grid
{
    /// <summary>
    /// <para>Look down this article: https://catlikecoding.com/unity/tutorials/hex-map/part-1/. Also https://www.redblobgames.com/grids/hexagons/ </para>
    /// <para>
    /// Basically, HexCordinates use a 3D system of dependent axies that allow 6 directions to be 
    /// expressed easily (in mathematical terms). The coordinates work using x,y,z where each dimension
    /// represents distance from an axis along a natural hex axis. See image here: https://catlikecoding.com/unity/tutorials/hex-map/part-1/hexagonal-coordinates/cube-diagram.png . 
    /// Only two axes are required to infer the third, as such the struct is created using only 2
    /// variables. 
    /// </para>
    /// 
    /// <para>
    /// Based on terminology in the redblobgames website above, this struct hold Axial coordinates and
    /// has the Z function, which allows calculation of the final coordinate required to convert Axial -> Cube coordinates.  
    /// </para>
    /// </summary>
    public struct SHexCoordinate
    {
        /// <summary>
        /// These are all directions that can used to move from one
        /// hex along a dimensional axis
        /// </summary>
        public static readonly int[] AxisDirections = new int[] { -1, 0, 1 };

        /// <summary>
        /// Holds direction SHexCoordinate in order of EDirection array
        /// </summary>
        public static readonly SHexCoordinate[] DirectionSteps = new SHexCoordinate[]
        {
            new SHexCoordinate(1, 0),
            new SHexCoordinate(0, 1),
            new SHexCoordinate(-1, 1),
            new SHexCoordinate(-1, 0),
            new SHexCoordinate(0, -1),
            new SHexCoordinate(1, -1)
        };

        /// <summary>
        /// Assumed directions to take one after the other when steping clockwise around the
        /// hex. It is possible for this to be wrong if x, y, z are not assumed that same as images in comments
        /// </summary>
        public static readonly EDirection[] ClockWiseDirections = new EDirection[]
        {
            EDirection.YNegX,
            EDirection.ZNegX,
            EDirection.ZNegY,
            EDirection.XNegY,
            EDirection.XNegZ,
            EDirection.YNegZ
        };

        #region Instance API
        /// <summary>
        /// X coordinate, input on construction
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Y coordinate, input on construction
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Z coordinate, calculated from x y
        /// </summary>
        public int Z { get; private set; }

        /// <summary>
        /// The sum of all three dimensions
        /// </summary>
        /// <returns></returns>
        public int SumAbsolute => System.Math.Abs(X) + System.Math.Abs(Y) + System.Math.Abs(Z);

        /// <summary>
        /// Constructor. Uses only x and y. z is infered
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SHexCoordinate(int x, int y)
        {
            X = x;
            Y = y;
            Z = -(x + y);
        }

        /// <summary>
        /// Returns all coordinates radial to the coordinate based on radius, including the original coordinate
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public SHexCoordinate[] RadialStep(int radius)
        {
            List<SHexCoordinate> coords = new List<SHexCoordinate>() { this };
            SHexCoordinate currentCoord = this;

            // step in rings to find coords
            for(int ring = 1; ring <= radius; ring++)
            {
                currentCoord = currentCoord.Step(EDirection.XNegZ);

                // each ring has 6 segments
                for(int segment = 0; segment < 6; segment++)
                {
                    // each step needs to be calculated in a particular direction with ring steps
                    for (int step = 0; step < ring; step++)
                    {
                        currentCoord = currentCoord.Step(ClockWiseDirections[segment]);
                        coords.Add(currentCoord);
                    }
                } 
            }

            return coords.ToArray();
        }

        /// <summary>
        /// Get HexCoordinate that represents a step in a direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public SHexCoordinate Step(EDirection direction) => this + DirectionSteps[(int)direction];

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is SHexCoordinate vector && X == vector.X && Y == vector.Y;

        /// <inheritdoc />
        public override int GetHashCode() => UTHash.BasicHash(X, Y);
        #endregion

        #region Static API
        /// <summary>
        /// Construct from x and z instead of x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static SHexCoordinate XZConstruct(int x, int z)
        {
            return new SHexCoordinate(x, -(x + z));
        }

        /// <summary>
        /// construct from y and z instead of x and y
        /// </summary>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static SHexCoordinate YZConstruct(int y, int z)
        {
            return new SHexCoordinate(-(y + z), y);
        }

        /// <summary>
        /// Zero Vector
        /// </summary>
        public static SHexCoordinate Zero => new SHexCoordinate(0, 0);
        
        /// <summary>
        /// Returns new GridCoordinate2 with x and y values summed
        /// </summary>
        /// <param name="vec1">First GridCoordinate2</param>
        /// <param name="vec2">Second GridCoordinate2</param>
        /// <returns>addition</returns>
        public static SHexCoordinate operator +(SHexCoordinate vec1, SHexCoordinate vec2)
        {
            return new SHexCoordinate(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        /// <summary>
        /// Returns new GridCoordinate2 with x and y values subtracted.
        /// </summary>
        /// <param name="vec1">Minuhend</param>
        /// <param name="vec2">Subtrahend</param>
        /// <returns>difference</returns>
        public static SHexCoordinate operator -(SHexCoordinate vec1, SHexCoordinate vec2)
        {
            return new SHexCoordinate(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        /// <summary>
        /// Distance heuristic that works with hex grid. It's equivalent to manhattan distance of a cube grid.
        /// </summary>
        /// <param name="hex1"></param>
        /// <param name="hex2"></param>
        /// <returns></returns>
        public static int Distance(SHexCoordinate hex1, SHexCoordinate hex2)
        {
            return (System.Math.Abs(hex1.X - hex2.X) + System.Math.Abs(hex1.Y - hex2.Y) + System.Math.Abs(hex1.Z - hex2.Z)) / 2;
        }

        /// <summary>
        /// Returns an array of hexes representing a line from hex1 to hex 2 including
        /// hex1 and hex 2
        /// </summary>
        /// <param name="hex1"></param>
        /// <param name="hex2"></param>
        /// <returns></returns>
        public static SHexCoordinate[] Line(SHexCoordinate hex1, SHexCoordinate hex2)
        {
            SHexCoordinate diffVector = hex2 - hex1;

            int distance = Distance(hex1, hex2);
            float xStep = (float)diffVector.X / distance;
            float yStep = (float)diffVector.Y / distance;

            SHexCoordinate[] line = new SHexCoordinate[distance+1];
            for(int i = 0; i<distance+1; i++)
            {
                line[i] = new SHexCoordinate(
                    (int)System.Math.Round(hex1.X + xStep * i),
                    (int)System.Math.Round(hex1.Y + yStep * i)
                );
            }

            return line;
        }

        /// <summary>
        /// Creates a hex grid where [0,0,0] is bottom left following
        /// coordinate rules of this struct (i.e) x = upwards distnace, 
        /// y = bottom right distance and z = bottom left distance.
        /// </summary>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        public static SHexCoordinate[,] Grid2D(DiscreteVector2 dimensions)
        {
            SHexCoordinate[,] grid = new SHexCoordinate[dimensions.X, dimensions.Y];

            for(int y = 0; y < dimensions.Y; y++)
            {
                int offset = y / 2;
                for (int x = 0; x < dimensions.X; x++)
                {
                    grid[x, y] = new SHexCoordinate(y, x - offset);
                }
            }

            return grid;
        }

        /// <inheritdoc />
        public static bool operator ==(SHexCoordinate vec1, SHexCoordinate vec2) => vec1.X == vec2.X && vec1.Y == vec2.Y;

        /// <inheritdoc />
        public static bool operator !=(SHexCoordinate vec1, SHexCoordinate vec2) => !(vec1.X == vec2.X && vec1.Y == vec2.Y);
        #endregion

        #region Internal Objects
        /// <summary>
        /// Axis
        /// </summary>
        public enum EAxis
        {
            /// <summary>
            /// UP positive, down negative
            /// </summary>
            X = 0,

            /// <summary>
            /// down right positive, up left negative
            /// </summary>
            Y = 1,

            /// <summary>
            /// down left positive, up right negative
            /// </summary>
            Z = 2
        }

        /// <summary>
        /// Direction are defined as a positive movement away from an axis,
        /// then a left or right choice
        /// </summary>
        public enum EDirection
        {
            /// <summary>
            /// +1 X, right (+1, 0, -1) 
            /// </summary>
            XNegZ = 0,

            /// <summary>
            /// +1 Y, left (0, +1, -1) 
            /// </summary>
            YNegZ = 1,

            /// <summary>
            /// +1 Y, right (-1, +1, 0) 
            /// </summary>
            YNegX = 2,

            /// <summary>
            /// +1 Z, left (-1, 0, +1) 
            /// </summary>
            ZNegX = 3,

            /// <summary>
            /// +1 Z, left (0, -1, +1) 
            /// </summary>
            ZNegY = 4,

            /// <summary>
            /// +1 Y, left (+1, -1, 0) 
            /// </summary>
            XNegY = 5
        }
        #endregion
    }
}
