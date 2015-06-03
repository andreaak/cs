using WorkWithSvn.RepositoryProviders;

namespace WorkWithSvn
{
    public interface IMainView
    {
        ControlsData ControlsData { get; }
        void UpdateItemsList();
    }
}
