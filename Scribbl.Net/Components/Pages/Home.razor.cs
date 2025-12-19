using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Scribbl.Net.Extensions;
using Scribbl.Net.Models;
using Scribbl.Net.Services;

namespace Scribbl.Net.Components.Pages
{
    public partial class Home
    {
        private ElementReference canvas;

        private bool drawing = false;

        private double fromX;
        private double fromY;

        private string color = "#000000";
        private int thickness = 2;

        private List<Line> lastSequence = [];

        [Inject]
        public IJSRuntime JS { get; set; } = null!;

        [Inject]
        private GameService GameService { get; set; } = null!;

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender) return;

            // nécessaire pour afficher l'image au chargement du composant
            JS.RedrawImage(GameService.Picture.SelectMany(x => x).ToList(), canvas);
            
            GameService.OnPictureUpdate += l =>
            {
                JS.DrawLine(canvas, l.FromX, l.FromY, l.ToX, l.ToY, l.Color, l.Thickness);
            };

            GameService.OnPictureDelete += () => JS.ClearCanvas(canvas);

            GameService.OnPictureRedraw += p => JS.RedrawImage(p, canvas);

            //connection.On<string>("NewWord", async w =>
            //{
            //    //await JS.InvokeVoidAsync("alert", w);
            //});

            // await connection.StartAsync();
        }

        private void StartDraw(MouseEventArgs e)
        {
            drawing = true;
            fromX = e.OffsetX;
            fromY = e.OffsetY;
        }

        private void Draw(MouseEventArgs e)
        {
            if (!drawing) return;

            // 1
            JS.DrawLine(canvas, fromX, fromY, e.OffsetX, e.OffsetY, color, thickness);

            // 2
            lastSequence.Add(new Line
            {
                FromX = fromX,
                FromY = fromY,
                ToX = e.OffsetX,
                ToY = e.OffsetY,
                Color = color,
                Thickness = thickness,
            });
            GameService.DrawLine(lastSequence[^1]);
            fromX = e.OffsetX;
            fromY = e.OffsetY;
        }

        public void ClearCanvas()
        {
            JS.ClearCanvas(canvas);
            GameService.ClearCanvas();
        }

        public void EndDraw()
        {
            drawing = false;
            if (lastSequence.Any())
            {
                GameService.SendSequence(lastSequence);
                lastSequence.Clear();
            }
        }

        public void Back()
        {
            GameService.Back();
        }

        public void PickWord()
        {
            //connection.SendAsync("PickWord");
        }


        
    }
}
