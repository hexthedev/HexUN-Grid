using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// Determines how the size of a hex grid is resolved
    /// </summary>
    public enum EHexGridGenerationMode
    { 
        /// <summary>
        /// Uses the x and y of the grid size provided to 
        /// create a hex grid in a rectangular grid format
        /// with the grid origin being at grid center
        /// </summary>
        Grid = 0,

        /// <summary>
        /// Follows a radial pattern from grid origin. 
        /// Only uses the X coordinate of grid size, since 
        /// radial is a uniform generation technique
        /// </summary>
        Radial = 1
    }
}