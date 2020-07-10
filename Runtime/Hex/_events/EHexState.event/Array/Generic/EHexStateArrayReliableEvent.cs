using HexUN.Events;

namespace HexUN.Grid
{
   [System.Serializable]
   public class EHexStateArrayReliableEvent : ReliableEvent<EHexState[], EHexStateArrayUnityEvent>
   {
   }
}