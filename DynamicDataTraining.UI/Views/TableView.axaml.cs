using Avalonia.ReactiveUI;
using DynamicDataTraining.ViewModel.ViewModelsFolder;

namespace DynamicDataTraining.UI.Views;

public partial class TableView :ReactiveUserControl<TableViewModel>
{
    public TableView()
    {
        InitializeComponent();

        PageSizeNumeric.LostFocus += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(PageSizeNumeric.Text))
            {
                // При пустом поле восстанавливаем значение из VM
                if (DataContext is TableViewModel vm)
                {
                    PageSizeNumeric.Value = vm.PageSize;
                }
            }
        };
    }
}