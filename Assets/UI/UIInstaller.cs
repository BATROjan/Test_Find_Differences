using UnityEditor;
using Zenject;

namespace UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            UIPlayingWindowInstaller.Install(Container);
        }
    }
}