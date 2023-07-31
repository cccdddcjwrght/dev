//
// Assets.cs
//
// Author:
//       fjy <jiyuan.feng@live.com>
//
// Copyright (c) 2020 fjy
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#define LOG_ENABLE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace libx
{
    public class EditorBoolValue
    {
#if UNITY_EDITOR        
        private string _recordValue;
        private bool _isInit;
        private bool _bValue;
        private bool _defaultValue;
#endif
        private bool _buildValue;


        public EditorBoolValue(string record, bool buildValue, bool defaultValue)
        {
            #if UNITY_EDITOR
            _recordValue = record;
            _defaultValue = defaultValue;
            #endif
            _buildValue = buildValue;
        }

        public bool GetValue()
        {
            #if UNITY_EDITOR
            if (_isInit == false)
            {
                _isInit = true; 
                _bValue = EditorPrefs.GetBool(_recordValue, _defaultValue);
            }
            return _bValue;
            #else
                return _buildValue;
            #endif
        }

        public void SetValue(bool v)
        {
            #if UNITY_EDITOR
            _isInit = true;
            _bValue = v;
            EditorPrefs.SetBool(_recordValue, _bValue);
            #endif
        }
    }
    
}