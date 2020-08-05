using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;
using HexCS.Games;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateArrayUnityEvent : UnityEvent<SHexCoordinate[]>
   {
   }
}