namespace PatientsAPI.DTOs;

public record DoctorResponse(
    Guid Id,
    string FullName,
    string CabinetNumber,
    string SpecializationTitle,
    string? AreaNumber);

public record DoctorRequest(
    string FullName,
    Guid CabinetId,
    Guid SpecializationId,
    Guid? AreaId);