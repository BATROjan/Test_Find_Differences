using Zenject;

namespace Picture
{
    public class PictureInstaller : Installer<PictureInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PictureController>().AsSingle().NonLazy();
            Container.BindMemoryPool<PictureView, PictureView.Pool>().FromComponentInNewPrefabResource("PictureView");
            
            Container.Bind<OunLineController>().AsSingle().NonLazy();
            Container.BindMemoryPool<OutLineView, OutLineView.Pool>().FromComponentInNewPrefabResource("OutLineView");
        }
    }
}