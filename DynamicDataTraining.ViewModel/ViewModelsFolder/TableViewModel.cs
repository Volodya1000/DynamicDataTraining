using DynamicDataTraining.ViewModel.Dtos;
using System.Collections.ObjectModel;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive;
using DynamicData;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System;
using DynamicData.Binding;

namespace DynamicDataTraining.ViewModel.ViewModelsFolder;

public class TableViewModel:ViewModelBase, IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    private readonly SourceList<StudentDto> _studentSource = new();

    private readonly ReadOnlyObservableCollection<StudentDto> _pagedStudents;

    public ReadOnlyObservableCollection<StudentDto> Students => _pagedStudents;

    private int _pageSize = 5;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            var corrected = Math.Max(1, value);
            this.RaiseAndSetIfChanged(ref _pageSize, corrected);
        }
    }

    private int _currentPage = 1;
    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            var max = Math.Max(1, TotalPages); // защита от TotalPages == 0
            var newValue = Math.Clamp(value, 1, max);
            this.RaiseAndSetIfChanged(ref _currentPage, newValue);
        }
    }

    [ObservableAsProperty]
    public int TotalPages { get; }

    #region command decloration
    public ReactiveCommand<Unit,Unit> FirstPageCommand { get; }
    public ReactiveCommand<Unit, Unit> PreviousPageCommand { get; }
    public ReactiveCommand<Unit, Unit> LastPageCommand { get; }
    public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
    #endregion



    public TableViewModel()
    {
        // Команды перехода активны только если есть хотя бы одна страница
        // и можно перейти вперёд или назад соответственно
        var canGoFirstOrPrevious = this.WhenAnyValue(
            x => x.CurrentPage,
            x => x.TotalPages,
            (page, total) => page > 1 && total > 0
        );

        var canGoNextOrLast = this.WhenAnyValue(
            x => x.CurrentPage,
            x => x.TotalPages,
            (page, total) => page < total && total > 0
        );


        // Автоматический пересчёт TotalPages при изменении _studentSource.Count или PageSize
        _studentSource
        .CountChanged
        .CombineLatest(this.WhenAnyValue(x => x.PageSize),
            (count, pageSize) =>
                Math.Max(1, (int)Math.Ceiling(count / (double)pageSize))) // минимум 1 страница
        .ToPropertyEx(this, x => x.TotalPages)
        .DisposeWith(_disposables);

        // Коррекция CurrentPage при изменении TotalPages и PageSize, чтобы не выйти за допустимые границы
        this.WhenAnyValue(x => x.TotalPages, x => x.CurrentPage)
            .Subscribe(tuple =>
            {
                var (total, current) = tuple;
                if (current > total)
                    CurrentPage = total;
                else if (current < 1)
                    CurrentPage = 1;
            })
            .DisposeWith(_disposables);

        // Создаем поток PageRequest из CurrentPage и PageSize
        var pageRequestObservable = this.WhenAnyValue(x => x.CurrentPage, x => x.PageSize)
          .Select(tuple =>
          {
              var (currentPage, pageSize) = tuple;
              return new PageRequest(currentPage, pageSize);
          });

        // Подключение к исходному списку студентов
        // Сортировка по имени → пагинация → биндинг к _pagedStudents (ObservableCollection)
        _studentSource.Connect()
            .Sort(SortExpressionComparer<StudentDto>.Ascending(s => s.FullName))
            .Page(pageRequestObservable)  // передаем IObservable<PageRequest>
            .Bind(out _pagedStudents)
            .DisposeMany() // удаление объектов при их исключении из списка
            .Subscribe()
            .DisposeWith(_disposables);

        FirstPageCommand = ReactiveCommand.Create<Unit>(_ => { CurrentPage = 1; }, canGoFirstOrPrevious);
        PreviousPageCommand = ReactiveCommand.Create(() => { if (CurrentPage > 1) CurrentPage--; }, canGoFirstOrPrevious);
        LastPageCommand = ReactiveCommand.Create<Unit>(_ => { CurrentPage = TotalPages; }, canGoNextOrLast);
        NextPageCommand = ReactiveCommand.Create(() => { if (CurrentPage < TotalPages) CurrentPage++; }, canGoNextOrLast);

        LoadStudents();
    }

    private void LoadStudents()
    {
        var students = new[]
        {
        new StudentDto("Иван", "Иванов", "Александрович", 101, 2, 0, 1),
        new StudentDto("Петр", "Петров", "Игоревич", 101, 0, 3, 0),
        new StudentDto("Сергей", "Сидоров", "Викторович", 102, 1, 1, 0),
        new StudentDto("Анна", "Кузнецова", "Дмитриевна", 103, 0, 0, 0),
        new StudentDto("Мария", "Лебедева", "Андреевна", 104, 4, 1, 2),
        new StudentDto("Алексей", "Морозов", "Сергеевич", 102, 0, 2, 1),
        new StudentDto("Ольга", "Кириллова", "Павловна", 101, 1, 0, 0),
        new StudentDto("Юлия", "Орлова", "Николаевна", 104, 2, 2, 1),
        new StudentDto("Дмитрий", "Зайцев", "Валерьевич", 103, 0, 1, 0),
        new StudentDto("Екатерина", "Федорова", "Евгеньевна", 102, 3, 0, 1),

        new StudentDto("Роман", "Тарасов", "Петрович", 105, 1, 1, 1),
        new StudentDto("Виктор", "Ковалев", "Анатольевич", 101, 0, 2, 2),
        new StudentDto("Ирина", "Мельникова", "Семеновна", 102, 3, 0, 0),
        new StudentDto("Светлана", "Руднева", "Васильевна", 104, 0, 3, 1),
        new StudentDto("Артем", "Фомин", "Геннадьевич", 103, 2, 1, 0),
        new StudentDto("Тимур", "Григорьев", "Ильич", 101, 0, 0, 1),
        new StudentDto("Лидия", "Волкова", "Евгеньевна", 104, 2, 2, 2),
        new StudentDto("Наталья", "Киселева", "Владимировна", 102, 1, 0, 1),
        new StudentDto("Андрей", "Чернов", "Федорович", 105, 0, 1, 0),
        new StudentDto("Жанна", "Белоусова", "Петровна", 101, 2, 0, 2),

        new StudentDto("Леонид", "Смирнов", "Аркадьевич", 103, 1, 1, 1),
        new StudentDto("Елена", "Галкина", "Михайловна", 104, 3, 1, 1),
        new StudentDto("Илья", "Соловьев", "Владимирович", 102, 0, 0, 3),
        new StudentDto("Галина", "Захарова", "Игоревна", 101, 1, 1, 0),
        new StudentDto("Александра", "Макарова", "Никитична", 104, 0, 2, 1),
        new StudentDto("Максим", "Борисов", "Сергеевич", 105, 2, 2, 2),
        new StudentDto("Кирилл", "Данилов", "Алексеевич", 102, 1, 0, 1),
        new StudentDto("Полина", "Федосеева", "Степановна", 103, 0, 1, 2),
        new StudentDto("Валерия", "Гусева", "Антоновна", 101, 1, 1, 1),
        new StudentDto("Олег", "Савельев", "Юрьевич", 104, 2, 0, 0)
    };

        _studentSource.AddRange(students);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _studentSource.Dispose();
    }
}
