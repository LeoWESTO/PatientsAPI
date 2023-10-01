namespace PatientsAPI.DTOs;

public record PatientResponse(
    Guid Id,
    string LastName,
    string FirstName,
    string? MiddleName,
    string Address,
    DateTime BirthDate,
    string Gender,
    string AreaNumber);
public record PatientRequest(
    string LastName,
    string FirstName,
    string? MiddleName,
    string Address,
    DateTime BirthDate,
    string Gender,
    Guid AreaId);