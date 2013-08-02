using Axiom.Core;
using Axiom.Graphics;
using System;

namespace ZeldaLTTP
{
    internal static class Program
    {
        private static void Main()
        {
            using (var engine = new Root())
            {
                engine.RenderSystem = engine.RenderSystems[0];
                using (var renderWindow = engine.Initialize(true))
                {
                    var game = new Game(engine, renderWindow);
                    game.OnLoad();
                    game.CreateScene();
                    engine.FrameRenderingQueued += game.OnRenderFrame;
                    engine.RenderOneFrame();
                    game.OnUnload();
                }
            }
            Console.Write("Press any key...");
            Console.ReadLine();
        }
    }
}
