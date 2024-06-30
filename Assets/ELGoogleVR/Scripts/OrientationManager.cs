using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnjoyLearning.VR.SDK
{
    public class OrientationManager : MonoBehaviour
    {
        [System.Serializable]
        public class ScreenSize
        {
            public static int MinWidth = 320;
            public static int MinHeight = 480;
            
            [SerializeField]
            private int width;
            public int Width { set { width = value > MinWidth ? value : MinWidth; } get { return width; } }

            [SerializeField]
            private int height;
            public int Height { set { height = value > MinHeight ? value : MinHeight; } get { return height; } }
        }

        public bool forceSize;
        public ScreenSize screenSize;
        public ScreenOrientation screenOrientation;

        void Awake()
        {
#if UNITY_EDITOR
            if(forceSize)
            {
                SetScreenWidthAndHeightFromEditorGameViewViaReflection();
            }
#else
            Screen.orientation = screenOrientation;
#endif
        }

#if UNITY_EDITOR
        void SetScreenWidthAndHeightFromEditorGameViewViaReflection()
        {
            var gameView = GetMainGameView();
            var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var gvsize = prop.GetValue(gameView, new object[0] { });
            var gvSizeType = gvsize.GetType();

            int targetWidth;
            int targetHeight;

            //Debug.LogFormat("OrientationManager -> Orientation: {0}", screenOrientation);
            switch (screenOrientation)
            {
                case ScreenOrientation.Landscape:
                case ScreenOrientation.LandscapeRight:
                    targetWidth = screenSize.Height;
                    targetHeight = screenSize.Width;
                    break;

                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    targetWidth = screenSize.Width;
                    targetHeight = screenSize.Height;
                    break;

                default:
                    bool swap = Random.Range(0.0f, 1.0f) >= 0.5f;
                    //Debug.LogFormat("OrientationManager -> Orientation: {0} | Swap: {1}", screenOrientation, swap);
                    targetWidth = swap ? screenSize.Height : screenSize.Width;
                    targetHeight = swap ? screenSize.Width : screenSize.Height;
                    break;
            }

            gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(gvsize, targetHeight, null);
            gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).SetValue(gvsize, targetWidth, new object[0] { });
        }

        UnityEditor.EditorWindow GetMainGameView()
        {
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetMainGameView.Invoke(null, null);

            return (UnityEditor.EditorWindow)Res;
        }
#endif

        private void Reset()
        {
            screenSize.Width = ScreenSize.MinWidth;
            screenSize.Height = ScreenSize.MinHeight;
            screenOrientation = ScreenOrientation.AutoRotation;
        }
    }
}

