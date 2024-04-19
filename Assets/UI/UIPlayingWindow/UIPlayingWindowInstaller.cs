using Zenject;

namespace UI
{
    public class UIPlayingWindowInstaller : Installer<UIPlayingWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIWindowController>().To<UIPlayingSceneWindowController>().AsSingle().NonLazy();
        }
    }
}