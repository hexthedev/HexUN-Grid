using UnityEngine;
using HexUN.Events;
using TobiasCSStandard.Games;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexCoordinateSoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexCoordinate")]
   public class SHexCoordinateSoEvent : ScriptableObjectEvent<SHexCoordinate>
   {
   }
}