using HexUN.Events;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexStateArgsArrayReliableEvent : ReliableEvent<SHexStateArgs[], SHexStateArgsArrayUnityEvent>
   {
   }
}