using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using XamarinFaces.Model;
namespace XamarinFaces.Helper
{
    public class Dibujar
    {
        public static Bitmap DibujarRectangulo(Bitmap imagenBitMap, List<CaraModel> carasDetectadas, string Nombre)
        {
            Bitmap bitmap = imagenBitMap.Copy(Bitmap.Config.Argb8888, true);
            Canvas canvas = new Canvas(bitmap);
            Paint paint = new Paint();
            paint.AntiAlias = true;
            paint.SetStyle(Paint.Style.Stroke);
            paint.Color = Color.White;
            paint.StrokeWidth = 1;
            if(carasDetectadas != null)
            {
                foreach(var cara in carasDetectadas)
                {
                    var Rectangulo = cara.faceRectangle;
                    canvas.DrawRect(Rectangulo.left, Rectangulo.top, Rectangulo.left + Rectangulo.width, Rectangulo.top + Rectangulo.height,paint);
                    DrawTextOnCanvas(canvas, 10, 10, 10, Color.White, Nombre);
                }
            }
            return bitmap;
        }
        private static void DrawTextOnCanvas(Canvas canvas, int tamano, int X, int Y, Color color, string Nombre)
        {
            Paint paint = new Paint();
            paint.AntiAlias = true;
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = color;
            paint.TextSize = tamano;
            canvas.DrawText(Nombre, X, Y, paint);

        }
    }
}