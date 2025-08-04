using DynamicDataTraining.ViewModel.Dtos;
using DynamicDataTraining.ViewModel.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;

namespace DynamicDataTraining.ViewModel.ViewModelsFolder.Filters;

public class AbsencesRangeFilterViewModel : ViewModelBase, IFilterViewModel
{
    [Reactive] public string FilterName { get; }
    
    [Reactive] public int? Min { get; set; }
    [Reactive] public int? Max { get; set; }

    public bool IsEnabled { get; set; }= true;

    public IObservable<Func<StudentDto, bool>> FilterFunc { get; }

    public object FilterKey => throw new NotImplementedException();

    private readonly Func<StudentDto, int>  _selector;

    public AbsencesRangeFilterViewModel(string filterName, Func<StudentDto,int> selector)
    {
        FilterName=filterName;
        _selector=selector;

        FilterFunc = this.WhenAnyValue(x => x.Min, x => x.Max)
            .Select(tuple =>
            {
                var (min, max) = tuple;
                return new Func<StudentDto, bool>(student =>
                {
                    int value = _selector(student);
                    return (!min.HasValue || value >= min) &&
                           (!max.HasValue || value <= max);
                });
            });
    }
}
