using ReactiveUI.Fody.Helpers;

namespace DynamicDataTraining.ViewModel.ViewModelsFolder;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive]
    public ViewModelBase CurrentViewModel { get; set; } = new TableViewModel();
}
