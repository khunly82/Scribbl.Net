using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Scribbl.Net.Models;

namespace Scribbl.Net.Extensions
{
    public static class JsExtensions
    {
        public static void DrawLine(this IJSRuntime js, 
            ElementReference canvas,
            double fromX,
            double fromY,
            double toX,
            double toY,
            string color,
            int thickness
        )
        {
            js.InvokeVoidAsync("drawLine", canvas, fromX, fromY, toX, toY, color, thickness);
        }

        public static void ClearCanvas(this IJSRuntime js, ElementReference canvas)
        {
            js.InvokeVoidAsync("clearCanvas", canvas);
        }

        public static void RedrawImage(this IJSRuntime js, List<Line> p, ElementReference canvas)
        {
            js.ClearCanvas(canvas);
            foreach (Line l in p)
            {
                js.DrawLine(canvas, l.FromX, l.FromY, l.ToX, l.ToY, l.Color, l.Thickness);
            }
        }
    }
}
