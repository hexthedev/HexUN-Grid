using HexUN.Events;


namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateArrayReliableEvent : ReliableEvent<SHexCoordinate[], SHexCoordinateArrayUnityEvent>
   {
   }
}