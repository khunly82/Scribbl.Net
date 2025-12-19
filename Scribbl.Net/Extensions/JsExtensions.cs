using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void ClearCanvas(this IJSRuntime js)
        {
            js.InvokeVoidAsync("clearCanvas");
        }
    }
}
