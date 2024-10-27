using AutoMapper;
using CursosWebApi.Domain.Entities;
using CursosWebApi.Domain.Models;

namespace CursosWebApi.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TurmaDTO, Turma>()
                .ForMember(turma => turma.Id, opt => opt.Ignore())
                .ForMember(turma => turma.Alunos, opt => opt.Ignore());

            CreateMap<Turma, TurmaDTO>();


            CreateMap<AlunoDTO, Aluno>()                
             .ForMember(aluno => aluno.Id, opt => opt.Ignore());

            CreateMap<Aluno, AlunoDTO>();
                
        }
    }
}
