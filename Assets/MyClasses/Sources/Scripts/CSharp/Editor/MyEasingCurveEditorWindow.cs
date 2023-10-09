/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 * Class:       MyEasingCurveEditorWindow (version 1.0)
 */

using UnityEditor;
using UnityEngine;
using System;
using System.IO;

namespace MyClasses.Tool
{
    public class MyEasingCurveEditorWindow : EditorWindow
    {
        #region ----- Define -----

        private readonly float SIZE = 900;
        private readonly float INPUT_HEIGHT = 200;
        private readonly float PADDING = 75;

        #endregion

        #region ----- Variable -----

        private Vector2 _convertedStartPoint = new Vector2(0, 650);
        private Vector2 _convertedMidPoint = new Vector2(328.5f, 650);
        private Vector2 _convertedMidPoint2 = new Vector2(467.5f, 0);
        private Vector2 _convertedEndPoint = new Vector2(850, 0);
        private Vector2 _startPoint;
        private Vector2 _midPoint;
        private Vector2 _midPoint2;
        private Vector2 _endPoint;
        private MyEasing.EEase _pathCurve = MyEasing.EEase.InSine;
        private MyEasing.EEase _velocityCurve = MyEasing.EEase.InSine;
        private bool _isDraggingStartPoint;
        private bool _isDraggingMidPoint;
        private bool _isDraggingMidPoint2;
        private bool _isDraggingEndPoint;
        private bool _isCustomPath;
        private int _smoothness = 30;
        private float _duration = 3;
        private float _moveTime = 0;
        private float _lastTime = 0;

        #endregion
        
        #region ----- EditorWindow Implementation -----

        /// <summary>
        /// OnEnable.
        /// </summary>
        void OnEnable()
        {
            titleContent = new GUIContent("[MyClasses] Easing Curve");
            minSize = new Vector2(SIZE, SIZE);
            
            _lastTime = Time.realtimeSinceStartup;
        }

        /// <summary>
        /// Update.
        /// </summary>
        void Update()
        {
            Repaint(); 
        }
        
        /// <summary>
        /// OnGUI.
        /// </summary>
        void OnGUI()
        {
            Handles.BeginGUI();

            // input
            EditorGUILayout.BeginHorizontal();
            _convertedStartPoint = EditorGUILayout.Vector2Field("Start Point", _convertedStartPoint);
            _convertedStartPoint.x = Mathf.Clamp(_convertedStartPoint.x, 0, SIZE - PADDING - PADDING);
            _convertedStartPoint.y = Mathf.Clamp(_convertedStartPoint.y, 0, SIZE - INPUT_HEIGHT - PADDING - PADDING);
            GUI.enabled = _isCustomPath;
            _convertedMidPoint = EditorGUILayout.Vector2Field("Mid Point", _convertedMidPoint);
            if (_isCustomPath)
            {
                _convertedMidPoint.x = Mathf.Clamp(_convertedMidPoint.x, 0, SIZE - PADDING - PADDING);
                _convertedMidPoint.y = Mathf.Clamp(_convertedMidPoint.y, 0, SIZE - INPUT_HEIGHT - PADDING - PADDING);
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUI.enabled = _isCustomPath;
            _convertedMidPoint2 = EditorGUILayout.Vector2Field("Mid Point 2", _convertedMidPoint2);
            if (_isCustomPath)
            {
                _convertedMidPoint2.x = Mathf.Clamp(_convertedMidPoint2.x, 0, SIZE - PADDING - PADDING);
                _convertedMidPoint2.y = Mathf.Clamp(_convertedMidPoint2.y, 0, SIZE - INPUT_HEIGHT - PADDING - PADDING);
            }
            GUI.enabled = true;
            _convertedEndPoint = EditorGUILayout.Vector2Field("End Point", _convertedEndPoint);
            _convertedEndPoint.x = Mathf.Clamp(_convertedEndPoint.x, 0, SIZE - PADDING - PADDING);
            _convertedEndPoint.y = Mathf.Clamp(_convertedEndPoint.y, 0, SIZE - INPUT_HEIGHT - PADDING - PADDING);
            EditorGUILayout.EndHorizontal();

            _smoothness = Math.Clamp(EditorGUILayout.IntField("Smoothness", _smoothness), 10, 100);
            _duration = Mathf.Clamp(EditorGUILayout.FloatField("Duration", _duration), 0.1f, 10f);
            _isCustomPath = EditorGUILayout.Toggle("Custom Path Curve", _isCustomPath);
            GUI.enabled = !_isCustomPath;
            _pathCurve = (MyEasing.EEase)EditorGUILayout.EnumPopup("Path Curve", _pathCurve);
            GUI.enabled = true;
            _velocityCurve = (MyEasing.EEase)EditorGUILayout.EnumPopup("Velocity Curve", _velocityCurve);

            if (GUI.changed)
            {
                _moveTime = 0;
            }

            // synctax
            GUI.enabled = false;
            if (_isCustomPath)
            {
                EditorGUILayout.LabelField(string.Format("---> MyUtilities.Move(transform, startPoint, midPoint, midPoint2, endPoint, MyEasing.EEase.{0}, 0, {1});", _velocityCurve.ToString(), _duration));
            }
            else
            {
                EditorGUILayout.LabelField(string.Format("---> MyUtilities.Move(transform, startPoint, endPoint, MyEasing.EEase.{0}, MyEasing.EEase.{1}, 0, {2});", _pathCurve.ToString(), _velocityCurve.ToString(), _duration));
            }
            GUI.enabled = true;

            // convert points
            _startPoint.x = _convertedStartPoint.x + PADDING;
            _startPoint.y = SIZE - PADDING - _convertedStartPoint.y;
            _midPoint.x = _convertedMidPoint.x + PADDING;
            _midPoint.y = SIZE - PADDING - _convertedMidPoint.y;
            _midPoint2.x = _convertedMidPoint2.x + PADDING;
            _midPoint2.y = SIZE - PADDING - _convertedMidPoint2.y;
            _endPoint.x = _convertedEndPoint.x + PADDING;
            _endPoint.y = SIZE - PADDING - _convertedEndPoint.y;

            // draw bezier points
            Vector2[] bezierPoints = _isCustomPath ? new Vector2[] { _startPoint, _midPoint, _midPoint2, _endPoint } : MyEasing.GetBezierPoints(_pathCurve, _startPoint, _endPoint);
            if (bezierPoints.Length >= 3)
            {
                Handles.color = Color.green;
                Handles.DrawAAPolyLine(3, bezierPoints[0], bezierPoints[1]);
                Handles.DrawSolidDisc(bezierPoints[1], Vector3.forward, _isCustomPath ? 5f : 2f);
                Handles.color = Color.red;
                Handles.DrawAAPolyLine(3, bezierPoints[2], bezierPoints[3]);
                Handles.DrawSolidDisc(bezierPoints[2], Vector3.forward, _isCustomPath ? 5f : 2f);

                _midPoint = bezierPoints[1];
                _convertedMidPoint.x = _midPoint.x - PADDING;
                _convertedMidPoint.y = SIZE - PADDING - _midPoint.y;
                _midPoint2 = bezierPoints[2];
                _convertedMidPoint2.x = _midPoint2.x - PADDING;
                _convertedMidPoint2.y = SIZE - PADDING - _midPoint2.y;
            }

            // draw curve
            float deltaTime = Time.realtimeSinceStartup - _lastTime;
            _lastTime = Time.realtimeSinceStartup;
            _moveTime += deltaTime;
            float curProgress = Mathf.Clamp(_moveTime / _duration, 0f, 1f);
            if (_moveTime >= _duration)
            {
                _moveTime = 0;
            }
            Handles.color = Color.yellow;
            Vector2 lastPoint = _startPoint;
            for (int i = 0; i <= _smoothness; ++i)
            {
                float distance = i / (float)_smoothness;
                float t = MyEasing.GetTimeByDistance(_velocityCurve, distance);
                Vector2 newPoint = _pathCurve == MyEasing.EEase.Linear ? MyBezier.GetLinearBezierPoint(bezierPoints[0], bezierPoints[1], t) : MyBezier.GetCubicBezierPoint(bezierPoints[0], bezierPoints[1], bezierPoints[2], bezierPoints[3], t);
                if (distance >= curProgress)
                {
                    Handles.color = Color.white;
                }
                Handles.DrawAAPolyLine(6, lastPoint, newPoint);
                Handles.DrawSolidDisc(newPoint, Vector3.forward, 2f);
                lastPoint = newPoint;
            }

            // drag & drop
            bool isMouseOverStartPoint = Vector2.Distance(Event.current.mousePosition, _startPoint) < 10f;
            bool isMouseOverMidPoint = Vector2.Distance(Event.current.mousePosition, _midPoint) < 10f;
            bool isMouseOverMidPoint2 = Vector2.Distance(Event.current.mousePosition, _midPoint2) < 10f;
            bool isMouseOverEndPoint = Vector2.Distance(Event.current.mousePosition, _endPoint) < 10f;
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    {
                        if (isMouseOverStartPoint)
                        {
                            _isDraggingStartPoint = true;
                            Event.current.Use();
                        }
                        else if (isMouseOverEndPoint)
                        {
                            _isDraggingEndPoint = true;
                            Event.current.Use();
                        }
                        else if (_isCustomPath)
                        {
                            if (isMouseOverMidPoint)
                            {
                                _isDraggingMidPoint = true;
                                Event.current.Use();
                            }
                            else if (isMouseOverMidPoint2)
                            {
                                _isDraggingMidPoint2 = true;
                                Event.current.Use();
                            }
                        }
                    }
                    break;

                case EventType.MouseDrag:
                    {
                        if (_isDraggingStartPoint)
                        {
                            _startPoint = Event.current.mousePosition;
                        }
                        else if (_isDraggingEndPoint)
                        {
                            _endPoint = Event.current.mousePosition;
                        }
                        else if (_isCustomPath)
                        {
                            if (_isDraggingMidPoint)
                            {
                                _midPoint = Event.current.mousePosition;
                            }
                            else if (_isDraggingMidPoint2)
                            {
                                _midPoint2 = Event.current.mousePosition;
                            }
                        }

                        _convertedStartPoint.x = _startPoint.x - PADDING;
                        _convertedStartPoint.y = SIZE - PADDING - _startPoint.y;
                        _convertedMidPoint.x = _midPoint.x - PADDING;
                        _convertedMidPoint.y = SIZE - PADDING - _midPoint.y;
                        _convertedMidPoint2.x = _midPoint2.x - PADDING;
                        _convertedMidPoint2.y = SIZE - PADDING - _midPoint2.y;
                        _convertedEndPoint.x = _endPoint.x - PADDING;
                        _convertedEndPoint.y =  SIZE - PADDING - _endPoint.y;
                    }
                    break;

                case EventType.MouseUp:
                    {
                        _isDraggingStartPoint = false;
                        _isDraggingMidPoint = false;
                        _isDraggingMidPoint2 = false;
                        _isDraggingEndPoint = false;
                    }
                    break;
            }

            // draw start point
            Handles.color = Color.green;
            if (_isDraggingStartPoint)
            {
                EditorGUIUtility.AddCursorRect(new Rect(Event.current.mousePosition.x - 10, Event.current.mousePosition.y - 10, 20, 20), MouseCursor.MoveArrow);
            }
            Handles.DrawSolidDisc(_startPoint, Vector3.forward, 6f);

            if (_isCustomPath)
            {
                // draw mid point
                Handles.color = Color.green;
                if (_isDraggingMidPoint)
                {
                    EditorGUIUtility.AddCursorRect(new Rect(Event.current.mousePosition.x - 10, Event.current.mousePosition.y - 10, 20, 20), MouseCursor.MoveArrow);
                }

                // draw mid point 2
                Handles.color = Color.red;
                if (_isDraggingMidPoint2)
                {
                    EditorGUIUtility.AddCursorRect(new Rect(Event.current.mousePosition.x - 10, Event.current.mousePosition.y - 10, 20, 20), MouseCursor.MoveArrow);
                }
            }

            // draw end point
            Handles.color = Color.red;
            if (_isDraggingEndPoint)
            {
                EditorGUIUtility.AddCursorRect(new Rect(Event.current.mousePosition.x - 10, Event.current.mousePosition.y - 10, 20, 20), MouseCursor.MoveArrow);
            }
            Handles.DrawSolidDisc(_endPoint, Vector3.forward, 6f);

            Handles.EndGUI();
        }

        #endregion
    }
}