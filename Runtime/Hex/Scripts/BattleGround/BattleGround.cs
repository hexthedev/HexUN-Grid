using System.Collections.Generic;
using HexCS.Core;
using HexCS.Games;
using HexUN.Utilities;
using HexUN.CSStandard;
using UnityEngine;

namespace HexUN.Grid
{
    /// <summary>
    /// The battleground uses a HexGrid component and manages tokens that act upon
    /// a hex grid. It is responsible for tracking the position of tokens, managing
    /// selected tokens, managing token ability selection based on the grid, etc. 
    /// </summary>
    public class BattleGround : MonoBehaviour
    {
        //private const string cHexOccupiedBy = "OccupiedBy";

        ///// <summary>
        ///// Holds arbitrary info about tokens. Even tokens that do 
        ///// not contain data will be added
        ///// </summary>
        //private Dictionary<AToken, PropertyBag> _tokenProperties = new Dictionary<AToken, PropertyBag>();
        //private Dictionary<AToken, SHexCoordinate> _tokenPlacement = new Dictionary<AToken, SHexCoordinate>();

        //[Header("References")]
        //[Tooltip("The Hex Grid that represents the hexes in the battleground")]
        //[SerializeField]
        //private HexGrid _grid = null;

        //public IEventSubscriber<SHexCoordinate> OnHexClicked => _grid.OnHexClicked;

        ///// <summary>
        ///// Place a token at the target hex. This function will not change the tokens parent, 
        ///// but will move the token to the posiiton of a hex. Returns null if coordinate is invalid
        ///// or space isn't vacant
        ///// </summary>
        ///// <param name="tokenPrefab">token prefab to be instanitated</param>
        ///// <param name="coord"></param>
        ///// <param name="data"></param>
        //public AToken CreateToken(AToken tokenPrefab, SHexCoordinate coord, PropertyBag data = null)
        //{
        //    if (!_grid.IsInBounds(coord)) return default;
        //    if (!TryGetHexPropertiesIfVacant(coord, out PropertyBag properties)) return default;

        //    // Make the token instance
        //    AToken tokenInstance = Instantiate(tokenPrefab);
        //    _tokenProperties[tokenInstance] = data ?? new PropertyBag();

        //    // move the token
        //    if (!MoveToken(tokenInstance, coord))
        //    {
        //        Destroy(tokenInstance);
        //        return default;
        //    };

        //    return tokenInstance;
        //}

        //public bool MoveToken(AToken token, SHexCoordinate coord)
        //{
        //    // if trying to move to occupied or non existant hex, don't do anything
        //    if (!TryGetHexPropertiesIfVacant(coord, out PropertyBag p)) return false;

        //    // unoccupy the current hex
        //    if (_tokenPlacement.TryGetValue(token, out SHexCoordinate currentCoord))
        //    {
        //        if(TryGetHexProperties(currentCoord, out PropertyBag properties))
        //        {
        //            properties.PutOrReplace(cHexOccupiedBy, null);
        //        }
        //    }

        //    // occupy the new hex
        //    if (TryGetHexProperties(coord, out PropertyBag props)) props.Put(cHexOccupiedBy, token);
        //    token.transform.position = _grid.GetPosition(coord);
        //    _tokenPlacement[token] = coord;
        //    return true;
        //}

        ///// <summary>
        ///// Returns true if the hex is vacant, meaning something can occupy it.
        ///// Assumes that if vacant, something will occupy it, so lazy loads a property bag for the hex
        ///// </summary>
        ///// <param name="coord"></param>
        ///// <param name="properties"></param>
        ///// <returns></returns>
        //public bool TryGetHexPropertiesIfVacant(SHexCoordinate coord, out PropertyBag properties)
        //{
        //    // try to get the hex properties
        //    if(!TryGetHexProperties(coord, out properties)) return false;

        //    // --- if you got to properties, now see if the hex is occupied
        //    // if there isn't an occupied property, or occupied property is null, then it's vacant
        //    if (!properties.TryGetValue(cHexOccupiedBy, out object value) || value == null) return true;

        //    // Otherwise a non null occupied value exists
        //    return false;
        //}

        
        










        ///// <summary>
        ///// If a token exists and has a property of input name, returns tru and outs the 
        ///// property. otherwise returns false.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="token"></param>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //public bool TryGetTokenProperty<T>(AToken token, string propertyName, out T property)
        //{
        //    if (TryGetTokenProperties(token, out PropertyBag bag))
        //    {
        //        if(bag.TryGetValue<T>(propertyName, out property))
        //        {
        //            return true;
        //        }
        //    }

        //    property = default;
        //    return false;
        //}

        ///// <summary>
        ///// If a token exists and has a property of input name, returns true and outs the 
        ///// property. otherwise returns false.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="token"></param>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //public bool TrySetTokenProperty<T>(AToken token, string propertyName, T value)
        //{
        //    if (TryGetTokenProperties(token, out PropertyBag bag))
        //    {
        //        bag.PutOrReplace(propertyName, value);
        //        return true;
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// If a hex exists and has a property of input name, returns tru and outs the 
        ///// property. otherwise returns false.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="token"></param>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //public bool TryGetHexProperty<T>(SHexCoordinate coord, string propertyName, out T property)
        //{
        //    if (TryGetHexProperties(coord, out PropertyBag bag))
        //    {
        //        if (bag.TryGetValue<T>(propertyName, out property))
        //        {
        //            return true;
        //        }
        //    }

        //    property = default;
        //    return false;
        //}

        ///// <summary>
        ///// If a token exists and has a property of input name, returns true and outs the 
        ///// property. otherwise returns false.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="token"></param>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //public bool TrySetHexProperty<T>(SHexCoordinate token, string propertyName, T value)
        //{
        //    if (TryGetHexProperties(token, out PropertyBag bag))
        //    {
        //        bag.PutOrReplace(propertyName, value);
        //        return true;
        //    }

        //    return false;
        //}







        //public void DisableAllHexes() => _grid.DisableAllHexInteractions();

        //public void EnableTokenRadius(AToken token, int size)
        //{
        //    if(_tokenPlacement.TryGetValue(token, out SHexCoordinate coord))
        //    {
        //        _grid.EnableHexRadial(coord, size);
        //    }
        //}

        //public AToken[] GetTokensInRange(AToken token, AToken[] selectTokens, int range)
        //{
        //    SHexCoordinate coord = _tokenPlacement[token];
        //    SHexCoordinate[] coords = UTArray.ConstructArray<SHexCoordinate>(selectTokens.Length, (i, e) => _tokenPlacement[selectTokens[i]]);

        //    List<AToken> tokens = new List<AToken>();

        //    for(int i = 0; i<selectTokens.Length; i++)
        //    {
        //        if (SHexCoordinate.Distance(coord, coords[i]) <= range) tokens.Add(selectTokens[i]);
        //    }

        //    return tokens.ToArray();
        //}
        
        //public void DestoryToken(AToken token)
        //{
        //    if (_tokenPlacement.ContainsKey(token)) _tokenPlacement.Remove(token);
        //    if (_tokenProperties.ContainsKey(token)) _tokenProperties.Remove(token);

        //    Destroy(token.gameObject);
        //}


















        ///// <summary>
        ///// Trys to get the properties of a hex. false if hex is not in grid
        ///// </summary>
        ///// <param name="properties"></param>
        ///// <returns></returns>
        //private bool TryGetHexProperties(SHexCoordinate coord, out PropertyBag properties) =>
        //    _grid.TryGetHexProperties(coord, out properties);


        ///// <summary>
        ///// Trys to get the properties of a token. false if no properties found
        ///// </summary>
        ///// <param name="properties"></param>
        ///// <returns></returns>
        //private bool TryGetTokenProperties(AToken coord, out PropertyBag properties) =>
        //    _tokenProperties.TryGetValue(coord, out properties);

        //// TO DO
        //// Manage tokens

        //// Way for token on battle ground to start an ability loop
        //// - Way to show hex selections based on ranges, shapes
        //// - Way to send messages between tokens and extract arbitrary dat

        //// - Way to apply arbitrary functions to tokens
    }
}