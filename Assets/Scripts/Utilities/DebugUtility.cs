using UnityEngine;

namespace Utilities
{
    public enum DebugColor : byte
    {
        White,
        Gray,
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta,
    }


    public class DebugUtility
    {
        // 유니티 디폴트 컬러 정의 enum으로 정의
        // DebugColor → Color 매핑
        private static Color ToUnityColor(DebugColor color)
        {
            return color switch
            {
                DebugColor.White => Color.white,
                DebugColor.Gray => Color.gray,
                DebugColor.Red => Color.red,
                DebugColor.Green => Color.green,
                DebugColor.Blue => Color.blue,
                DebugColor.Yellow => Color.yellow,
                DebugColor.Cyan => Color.cyan,
                DebugColor.Magenta => Color.magenta,
                _ => Color.white
            };
        }

        // 로그 메서드 만들기 (파라미터 : 디폴트 컬러 선택, string title, string message)
        /// <summary>
        /// Unity 기본 색상 기반 로그 출력 (color는 DebugColor로 지정)
        /// </summary>
        public static void Log(DebugColor color, string title, string message)
        {
            Color unityColor = ToUnityColor(color);
            string hexColor = ColorUtility.ToHtmlStringRGB(unityColor);
            Debug.Log($"<color=#{hexColor}>[{title}]</color> {message}");
        }
        
        public static void Log(string message)
        {
            // unity editor에서만 debug 찍히게.........
            
            //Color unityColor = ToUnityColor(DebugColor.Gray);
            //string hexColor = ColorUtility.ToHtmlStringRGB(unityColor);
            // Debug.Log($"<color=#{hexColor}></color> {message}");
            
            Debug.Log($"{message}");
        }


        // 로그 메서드 만들기 (파라미터 : 디폴트 컬러 선택, string title, string message)
        /// <summary>
        /// Unity 기본 색상 기반 로그 출력 (color는 DebugColor로 지정)
        /// </summary>
        public static void LogError(string title, string message)
        {
            Color unityColor = ToUnityColor(DebugColor.Red);
            string hexColor = ColorUtility.ToHtmlStringRGB(unityColor);
            Debug.Log($"<color=#{hexColor}>[{title}]</color> {message}");
        }
    }
}