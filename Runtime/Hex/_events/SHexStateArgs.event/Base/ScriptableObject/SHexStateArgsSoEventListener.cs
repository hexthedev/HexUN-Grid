using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;

namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/SHexStateArgs/SHexStateArgsSoEventListener")]
   public class SHexStateArgsSoEventListener : ScriptableObjectEventListener<SHexStateArgs, SHexStateArgsSoEvent, SHexStateArgsUnityEvent>
   {
   }
}