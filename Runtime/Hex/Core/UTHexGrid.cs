using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Utility methods to do with making HexGrids. Much thanks and help from 
    /// https://catlikecoding.com/unity/tutorials
    /// </summary>
    public static class UTHexGrid
    {
        /// <summary>
        /// The number the outer radius must be multiplied by to get the inner radius
        /// </summary>
        public const float cInnerRadiusModifier = 0.866025404f;

        /// <summary>
        /// Calculates the inner radius (center to edge) based on the outer radius
        /// (center to corner) of a hex grid square.
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <returns></returns>
        public static float CalculateInnerRadius(float outerRadius) => outerRadius * cInnerRadiusModifier;

        /// <summary>
        /// Hex has point at heighest y. 6 modifiers starting at top hex point moving clockwise
        /// that must be multiplied by the outer radius to get the 
        /// Vector2 positions of each point in local space. 
        /// </summary>
        public static Vector2[] TopPointModifiers = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(cInnerRadiusModifier, 0.5f),
            new Vector2(cInnerRadiusModifier, -0.5f),
            new Vector2(0, -1),
            new Vector2(-cInnerRadiusModifier, 0.5f),
            new Vector2(-cInnerRadiusModifier, -0.5f)
        };

        /// <summary>
        /// Hex is flat at highest y. 6 modifiers starting at top hex point (rightmost) moving clockwise
        /// that must be multiplied by the outer radius to get the 
        /// Vector2 positions of each point in local space. 
        /// </summary>
        public static Vector2[] TopFlatModifiers = new Vector2[]
        {
            new Vector2(0.5f, cInnerRadiusModifier),
            new Vector2(1, 0),
            new Vector2(0.5f, -cInnerRadiusModifier),
            new Vector2(-0.5f, -cInnerRadiusModifier),
            new Vector2(-1, 0),
            new Vector2(-0.5f, cInnerRadiusModifier)
        };
    }
}
