using Avalonia.ReactiveUI;
using DynamicDataTraining.ViewModel.ViewModelsFolder;
using DynamicDataTraining.ViewModel.ViewModelsFolder.Filters;

namespace DynamicDataTraining.UI.Views.Filters;

public partial class LastNameFilterView : ReactiveUserControl<LastNameFilterViewModel>
{
    public LastNameFilterView()
    {
        InitializeComponent();
    }
}