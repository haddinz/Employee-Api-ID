using AutoMapper;
using EmployeeApi.DTO.Request;
using EmployeeApi.Models.Entity;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<EmployeeReq, Employee>();
        CreateMap<EmployeeUpdateReq, Employee>();
        CreateMap<Employee, EmployeeReq>();
    }
}