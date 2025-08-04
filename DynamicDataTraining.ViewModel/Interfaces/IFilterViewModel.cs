using DynamicDataTraining.ViewModel.Dtos;
using System.ComponentModel;

namespace DynamicDataTraining.ViewModel.Interfaces;

public interface IFilterViewModel: INotifyPropertyChanged
{
    string FilterName { get; }

    //включён ли фильтр (пользователь может его выключить, чтобы временно игнорировать).
    bool IsEnabled { get; set; }

    //фильтр для DynamicData
    IObservable<Func<StudentDto, bool>> FilterFunc { get; }

    object FilterKey { get; }
}
