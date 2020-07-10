using UnityEngine;
using HexUN.Events;
using TobiasCSStandard.Games;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexCoordinateArraySoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexCoordinateArray")]
   public class SHexCoordinateArraySoEvent : ScriptableObjectEvent<SHexCoordinate[]>
   {
   }
}