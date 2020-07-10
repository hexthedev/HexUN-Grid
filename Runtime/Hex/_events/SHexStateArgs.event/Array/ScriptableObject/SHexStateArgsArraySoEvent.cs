using UnityEngine;
using HexUN.Events;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexStateArgsArraySoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexStateArgsArray")]
   public class SHexStateArgsArraySoEvent : ScriptableObjectEvent<SHexStateArgs[]>
   {
   }
}