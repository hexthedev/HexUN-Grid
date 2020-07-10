using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;

namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/EHexStateArray/EHexStateArraySoEventListener")]
   public class EHexStateArraySoEventListener : ScriptableObjectEventListener<EHexState[], EHexStateArraySoEvent, EHexStateArrayUnityEvent>
   {
   }
}