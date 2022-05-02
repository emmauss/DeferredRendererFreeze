using Avalonia.Controls;
using Avalonia.Rendering;

namespace DeferredRendererFreeze
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if(Renderer  is DeferredRenderer renderer)
            {
                renderer.RenderOnlyOnRenderThread = true;
            }
            InitializeComponent();
        }
    }
}
