using DynamicDataTraining.Core.ValueObjects;

namespace DynamicDataTraining.Core.StudentsData;

public class Student
{
    public FullName FullName { get; }
    public Student(FullName fullName)
    {
        FullName=fullName;
    }
}
