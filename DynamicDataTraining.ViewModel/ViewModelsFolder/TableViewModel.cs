using DynamicDataTraining.ViewModel.Dtos;
using System.Collections.ObjectModel;

namespace DynamicDataTraining.ViewModel.ViewModelsFolder;

public class TableViewModel:ViewModelBase
{
    public ReadOnlyObservableCollection<StudentDto> Students { get; init; }

    public TableViewModel()
    {
        var studentList = new ObservableCollection<StudentDto>
        {
            new("Иван", "Иванов", "Александрович", 101, 2, 0, 1),
            new("Петр", "Петров", "Игоревич", 101, 0, 3, 0),
            new("Сергей", "Сидоров", "Викторович", 102, 1, 1, 0),
            new("Анна", "Кузнецова", "Дмитриевна", 103, 0, 0, 0),
            new("Мария", "Лебедева", "Андреевна", 104, 4, 1, 2),
            new("Алексей", "Морозов", "Сергеевич", 102, 0, 2, 1),
            new("Ольга", "Кириллова", "Павловна", 101, 1, 0, 0),
            new("Юлия", "Орлова", "Николаевна", 104, 2, 2, 1),
            new("Дмитрий", "Зайцев", "Валерьевич", 103, 0, 1, 0),
            new("Екатерина", "Федорова", "Евгеньевна", 102, 3, 0, 1)
        };

        Students = new ReadOnlyObservableCollection<StudentDto>(studentList);
    }
}
