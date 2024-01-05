using SaveSystem.Core;
using SaveSystem.FileSaverSystem;
using Zenject;

namespace Core
{
    public class ProjectContentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SavingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FileSystemSaverLoader>().AsSingle();
        }
    }
}