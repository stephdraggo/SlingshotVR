using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    /// <summary>
    /// Some extensions and conversions I want.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Array.ToList
        /// <summary>
        /// Convert array to list.
        /// </summary>
        public static List<T> ToList<T>(this T[] _array)
        {
            return new List<T>(_array);
        }
        #endregion

        #region Array.Add
        #region single element
        /// <summary>
        /// Add element to the end or begining of an existing array.
        /// </summary>
        /// <param name="_front">Should the added element be at the begining of the array? Defaults to end of array.</param>
        public static T[] Add<T>(this T[] _array, T _element, bool _front = false)
        {
            T[] newArray = new T[_array.Length + 1];
            if (_front)
            {
                _array.CopyTo(newArray, 1);
                newArray[0] = _element;
            }
            else
            {
                _array.CopyTo(newArray, 0);
                newArray[_array.Length] = _element;
            }

            return newArray;
        }
        #endregion

        #region array
        /// <summary>
        /// Add array to the end or begining of an existing array.
        /// </summary>
        /// <param name="_front">Should the added array be at the begining of the existing array? Defaults to end of array.</param>
        public static T[] Add<T>(this T[] _array, T[] _addArray, bool _front = false)
        {
            T[] newArray = new T[_array.Length + _addArray.Length];
            if (_front)
            {
                _array.CopyTo(newArray, _addArray.Length);
                _addArray.CopyTo(newArray, 0);
            }
            else
            {
                _array.CopyTo(newArray, 0);
                _addArray.CopyTo(newArray, _array.Length);
            }

            return newArray;
        }
        #endregion
        #endregion

        #region Float.InRange : Float within range of vector2? bool
        /// <summary>
        /// Return true if the float in within the passed range (inclusive).
        /// </summary>
        public static bool InRange(this float _float, Vector2 _range)
        {
            if (_range.x > _range.y) //if wrong way
            {
                //flip
                float temp = _range.x;
                _range.x = _range.y;
                _range.y = temp;
            }

            if (_float >= _range.x && _float <= _range.y)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Vector2.RanFloat
        /// <summary>
        /// Generate random float from a vector2.
        /// x does not have to be less than y.
        /// </summary>
        public static float RanFloat(this Vector2 _vector2)
        {
            if (_vector2.x < _vector2.y)
            {
                return Random.Range(_vector2.x, _vector2.y);
            }
            else if (_vector2.x > _vector2.y)
            {
                return Random.Range(_vector2.y, _vector2.x);
            }
            else return _vector2.x;
        }
        #endregion

        #region Vector3.Multiply
        /// <summary>
        /// Multiply one vector3 by another.
        /// </summary>
        public static Vector3 Multiply(this Vector3 a, Vector3 b)
        {
            float x = a.x * b.x;
            float y = a.y * b.y;
            float z = a.z * b.z;
            return new Vector3(x, y, z);
        }
        #endregion

    }
}