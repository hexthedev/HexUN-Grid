using HexUN.Events;
using UnityEngine;
using UnityEngine.Events;
using TobiasCSStandard.Games;

namespace HexUN.Grid
{
   [System.Serializable]
   public class SHexCoordinateArrayUnityEvent : UnityEvent<SHexCoordinate[]>
   {
   }
}