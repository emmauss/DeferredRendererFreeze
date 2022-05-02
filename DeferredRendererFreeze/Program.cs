using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Rendering;
using Avalonia.Threading;
using System;

namespace DeferredRendererFreeze
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            var renderTimer = new RenderTimer();
            return AppBuilder.Configure<App>()
                           .UsePlatformDetect()
                       .With(new Win32PlatformOptions()
                       {
                           UseDeferredRendering = true,
                           UseWindowsUIComposition = false,
                       })
                       .UseSkia()
                       .AfterSetup(_ =>
                       {
                           AvaloniaLocator.CurrentMutable
                               .Bind<IRenderTimer>().ToConstant(renderTimer)
                               .Bind<IRenderLoop>().ToConstant(new RenderLoop(renderTimer, Dispatcher.UIThread));
                       })
                       .LogToTrace();
        }
    }
}
