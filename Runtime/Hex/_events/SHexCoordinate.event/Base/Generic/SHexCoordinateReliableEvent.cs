using HexUN.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateReliableEvent : ReliableEvent<SHexCoordinate, SHexCoordinateUnityEvent>
   {
   }
}