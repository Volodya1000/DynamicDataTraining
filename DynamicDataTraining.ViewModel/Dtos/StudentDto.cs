namespace DynamicDataTraining.ViewModel.Dtos;

public record StudentDto(string FirstName,
                         string LastName, 
                         string Surname,
                         int GroupNumber,
                         int JustifiedAbsencesCount,
                         int UnjustifiedAbsencesCount,
                         int OtherAbsencesCount)
{
    public string FullName => $"{LastName} {FirstName} {Surname}";

    public int AllAbsencesCount => 
        JustifiedAbsencesCount + UnjustifiedAbsencesCount + OtherAbsencesCount;
}
