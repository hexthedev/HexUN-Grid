using UnityEngine;
using HexUN.Events;


namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexCoordinateArraySoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexCoordinateArray")]
   public class SHexCoordinateArraySoEvent : ScriptableObjectEvent<SHexCoordinate[]>
   {
   }
}