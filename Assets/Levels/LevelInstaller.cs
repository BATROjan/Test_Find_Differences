using Zenject;

namespace Levels
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelController>().AsSingle().NonLazy();
            Container
                .Bind<LevelConfig>()
                .FromScriptableObjectResource("LevelConfig")
                .AsSingle()
                .NonLazy();
        }
    }
}