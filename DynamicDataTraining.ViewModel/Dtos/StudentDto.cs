namespace DynamicDataTraining.ViewModel.Dtos;

public record StudentDto(string firstName, 
                         string lastName, 
                         string surname,
                         int groopNumber,
                         int validAbsencesReasonNumber,
                         int unvalidAbsencesReasonNumber,
                         int otherReasonAbsencesNumber)
{
    public string FullName => $"{firstName} {lastName}  {surname}";
}
