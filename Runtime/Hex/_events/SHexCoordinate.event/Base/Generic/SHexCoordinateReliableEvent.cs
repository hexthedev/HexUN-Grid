using HexUN.Events;


namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateReliableEvent : ReliableEvent<SHexCoordinate, SHexCoordinateUnityEvent>
   {
   }
}