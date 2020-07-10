using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;

namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/EHexState/EHexStateSoEventListener")]
   public class EHexStateSoEventListener : ScriptableObjectEventListener<EHexState, EHexStateSoEvent, EHexStateUnityEvent>
   {
   }
}