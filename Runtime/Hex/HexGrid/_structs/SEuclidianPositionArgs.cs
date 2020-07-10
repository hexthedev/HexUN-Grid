﻿using System;
using System.Collections;
using System.Collections.Generic;
using TobiasCSStandard.Games;
using UnityEngine;

namespace HexUN.Grid
{
    public struct SEuclidianPositionArgs
    {
        public SHexCoordinate Coordinate;
        public Action<Vector3> Callback;
    }
}