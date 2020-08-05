using UnityEngine;
using HexUN.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexCoordinateArraySoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexCoordinateArray")]
   public class SHexCoordinateArraySoEvent : ScriptableObjectEvent<SHexCoordinate[]>
   {
   }
}