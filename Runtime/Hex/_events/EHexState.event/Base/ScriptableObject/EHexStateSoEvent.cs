using UnityEngine;
using HexUN.Events;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "EHexStateSoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/EHexState")]
   public class EHexStateSoEvent : ScriptableObjectEvent<EHexState>
   {
   }
}