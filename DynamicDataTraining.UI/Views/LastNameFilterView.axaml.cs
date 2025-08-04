using Avalonia.ReactiveUI;
using DynamicDataTraining.ViewModel.ViewModelsFolder;

namespace DynamicDataTraining.UI.Views;

public partial class LastNameFilterView : ReactiveUserControl<LastNameFilterViewModel>
{
    public LastNameFilterView()
    {
        InitializeComponent();
    }
}