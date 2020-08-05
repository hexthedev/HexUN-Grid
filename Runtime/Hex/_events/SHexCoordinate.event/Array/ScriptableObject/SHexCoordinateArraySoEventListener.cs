using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/SHexCoordinateArray/SHexCoordinateArraySoEventListener")]
   public class SHexCoordinateArraySoEventListener : ScriptableObjectEventListener<SHexCoordinate[], SHexCoordinateArraySoEvent, SHexCoordinateArrayUnityEvent>
   {
   }
}