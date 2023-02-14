using System;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Texture2DExtensionMethods
    {
        /// <summary>
        /// Draw a jagged circle
        /// </summary>
        /// <param name="texture2D"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="r"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Texture2D DrawCircle(this Texture2D texture2D, int centerX, int centerY, int r,
            Color color)
        {
            for (var x = centerX - r; x < centerX + r + 1; x++)
            for (var y = centerY - r; y < centerY + r + 1; y++)
                if (Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2) < r * r)
                    texture2D.SetPixel(x, y, color);
            return texture2D;
        }

        /// <summary>
        /// Texture2D to Sprite
        /// </summary>
        /// <param name="texture2D"></param>
        /// <returns></returns>
        public static Sprite ToSprite(this Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));
        }

        // /// <summary>
        // /// 去除绿幕
        // /// </summary>
        // public static Texture2D RemoveGreen(this Texture2D texture2D)
        // {
        //     // 首先获取一个color列表
        //     var colors = texture2D.GetPixels();
        //     for (int i = 0; i < colors.Length; i++)
        //     {
        //         if (colors[i].r > 0.25f) continue;
        //         if (colors[i].b > 0.25f) continue;
        //         if (colors[i].g < colors[i].r) continue;
        //         if (colors[i].g < colors[i].b) continue;
        //         colors[i] = Color.clear;
        //     }
        //
        //     texture2D.SetPixels(colors);
        //     return texture2D;
        // }

        /// <summary>
        /// Paint bucket
        /// </summary>
        /// <param name="texture2D"></param>
        /// <param name="color"></param>
        public static void SetColor(this Texture2D texture2D, Color color)
        {
            var colors = texture2D.GetPixels();
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }

            texture2D.SetPixels(colors);
        }

        /// <summary>
        /// 将纹理A覆盖在纹理B上
        /// 上面的乘以上面的透明度，这里有问题，待修改
        /// </summary>
        public static void Fuse(Texture2D textureUp, Texture2D textureDown, Texture2D fusedTexture)
        {
            var upColors = textureUp.GetPixels();
            var downColors = textureDown.GetPixels();
            var fusedColors = new Color[downColors.Length];
            for (int i = 0; i < downColors.Length; i++)
            {
                float r = upColors[i].r + (1 - upColors[i].a) * downColors[i].r;
                float g = upColors[i].g + (1 - upColors[i].a) * downColors[i].g;
                float b = upColors[i].b + (1 - upColors[i].a) * downColors[i].b;
                float a = upColors[i].a + (1 - upColors[i].a) * downColors[i].a;
                fusedColors[i] = new Color(r, g, b, a);
            }

            fusedTexture.SetPixels(fusedColors);
            fusedTexture.Apply();
        }
    }
}