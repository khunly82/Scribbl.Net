using Microsoft.AspNetCore.SignalR.Client;
using Scribbl.Net.Models;

namespace Scribbl.Net.Services
{
    public class GameService
    {
        private HubConnection hubConnection
            = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl("http://10.10.23.45:8838/ws/game").Build();

        public event Action? OnPictureDelete;
        public event Action<Line>? OnPictureUpdate;
        public event Action<List<Line>>? OnPictureRedraw;

        public List<List<Line>> Picture { get; private set; } = [];


        public GameService()
        {

            hubConnection.On<Line>("DrawLine", l =>
            {
                OnPictureUpdate?.Invoke(l);
            });

            hubConnection.On("ClearCanvas", () =>
            {
                Picture.Clear();
                OnPictureDelete?.Invoke();
            });

            hubConnection.On<List<List<Line>>>("Redraw", (p) =>
            {
                Picture = p;
                OnPictureRedraw?.Invoke(p.SelectMany(x => x).ToList());
            });

            hubConnection.On<string>("NewWord", async w =>
            {
                //await JS.InvokeVoidAsync("alert", w);
            });

            hubConnection.StartAsync();
        }

        public void DrawLine(Line l)
        {
            hubConnection.SendAsync("DrawLine", l);
        }

        public void ClearCanvas()
        {
            hubConnection.SendAsync("ClearCanvas");
        }

        public void Back()
        {
            hubConnection.SendAsync("Back");
        }

        public void SendSequence(List<Line> sequence)
        {
            Picture.Add(sequence);
            hubConnection.SendAsync("SendSequence", sequence);
        }
    }
}
