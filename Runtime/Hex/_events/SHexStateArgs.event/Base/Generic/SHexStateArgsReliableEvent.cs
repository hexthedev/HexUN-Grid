using HexUN.Events;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexStateArgsReliableEvent : ReliableEvent<SHexStateArgs, SHexStateArgsUnityEvent>
   {
   }
}