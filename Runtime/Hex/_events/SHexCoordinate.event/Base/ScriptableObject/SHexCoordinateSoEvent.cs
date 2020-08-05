using UnityEngine;
using HexUN.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [CreateAssetMenu(fileName = "SHexCoordinateSoEvent", menuName = "HexUN/Systems/Grid/Hex/Events/SHexCoordinate")]
   public class SHexCoordinateSoEvent : ScriptableObjectEvent<SHexCoordinate>
   {
   }
}