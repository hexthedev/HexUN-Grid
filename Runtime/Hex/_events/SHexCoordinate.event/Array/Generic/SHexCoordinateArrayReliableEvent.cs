using HexUN.Events;
using TobiasCSStandard.Games;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateArrayReliableEvent : ReliableEvent<SHexCoordinate[], SHexCoordinateArrayUnityEvent>
   {
   }
}