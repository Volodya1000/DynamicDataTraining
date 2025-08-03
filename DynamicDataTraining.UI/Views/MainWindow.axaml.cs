using Avalonia.ReactiveUI;
using DynamicDataTraining.ViewModel.ViewModelsFolder;

namespace DynamicDataTraining.UI.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}