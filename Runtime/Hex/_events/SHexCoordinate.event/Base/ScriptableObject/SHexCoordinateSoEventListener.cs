using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;


namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/SHexCoordinate/SHexCoordinateSoEventListener")]
   public class SHexCoordinateSoEventListener : ScriptableObjectEventListener<SHexCoordinate, SHexCoordinateSoEvent, SHexCoordinateUnityEvent>
   {
   }
}