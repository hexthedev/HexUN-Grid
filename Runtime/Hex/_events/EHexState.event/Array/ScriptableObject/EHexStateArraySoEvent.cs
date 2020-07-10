using UnityEngine;
using HexUN.Events;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "EHexStateArraySoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/EHexStateArray")]
   public class EHexStateArraySoEvent : ScriptableObjectEvent<EHexState[]>
   {
   }
}