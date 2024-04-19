using Levels;
using MainCamera;
using Picture;
using UI;
using Zenject;

public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<MainCamera.MainCamera>()
            .FromComponentInNewPrefabResource("Main Camera")
            .AsSingle()
            .NonLazy();
        
        UIInstaller.Install(Container);
        PictureInstaller.Install(Container);
        LevelInstaller.Install(Container);
        
        Container.InstantiatePrefabResource("UIRoot");
        Container.Bind<GameController.GameController>().AsSingle().NonLazy();
    }
}
