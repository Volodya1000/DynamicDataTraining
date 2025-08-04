using DynamicDataTraining.ViewModel.Dtos;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using DynamicDataTraining.ViewModel.Interfaces;
using System.Reactive.Linq;

namespace DynamicDataTraining.ViewModel.ViewModelsFolder;

public class LastNameFilterViewModel : ViewModelBase, IFilterViewModel
{
    [Reactive] public string LastName { get; set; }
    [Reactive] public bool IsEnabled { get; set; } = true;

    public string FilterName => "Фамилия";

    public object FilterKey => LastName ?? string.Empty;

    public IObservable<Func<StudentDto, bool>> FilterFunc { get; }

    public LastNameFilterViewModel()
    {
        FilterFunc = this.WhenAnyValue(x => x.LastName, x => x.IsEnabled)
            .Select(t =>
            {
                var (lastName, enabled) = t;
                if (!enabled || string.IsNullOrWhiteSpace(lastName))
                    return new Func<StudentDto, bool>(_ => true);

                return new Func<StudentDto, bool>(student =>
                    student.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
            });
    }
}