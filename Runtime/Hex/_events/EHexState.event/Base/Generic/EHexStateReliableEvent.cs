using HexUN.Events;

namespace HexUN.Grid
{
   [System.Serializable]
   public class EHexStateReliableEvent : ReliableEvent<EHexState, EHexStateUnityEvent>
   {
   }
}