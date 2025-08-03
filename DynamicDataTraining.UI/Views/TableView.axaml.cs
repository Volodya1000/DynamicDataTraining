using Avalonia.ReactiveUI;
using DynamicDataTraining.ViewModel.ViewModelsFolder;

namespace DynamicDataTraining.UI.Views;

public partial class TableView :ReactiveUserControl<TableViewModel>
{
    public TableView()
    {
        InitializeComponent();
    }
}