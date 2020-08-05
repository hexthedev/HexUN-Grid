using HexUN.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateArrayReliableEvent : ReliableEvent<SHexCoordinate[], SHexCoordinateArrayUnityEvent>
   {
   }
}