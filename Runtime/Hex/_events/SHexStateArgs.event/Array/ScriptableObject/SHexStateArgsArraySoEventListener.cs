using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;

namespace HexUN.Grid
{
   [AddComponentMenu("HexUN/Systems/Grid/Hex/Events/SHexStateArgsArray/SHexStateArgsArraySoEventListener")]
   public class SHexStateArgsArraySoEventListener : ScriptableObjectEventListener<SHexStateArgs[], SHexStateArgsArraySoEvent, SHexStateArgsArrayUnityEvent>
   {
   }
}