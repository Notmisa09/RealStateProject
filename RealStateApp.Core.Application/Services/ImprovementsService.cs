using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services
{
    public class ImprovementsService : GenericService<ImprovemetnsViewModel, ImprovementsAddViewModel, Improvements>, IimprovementsService
    {
        private IMapper _mapper;
        private readonly IimprovementsRepository _repository;
        public ImprovementsService(IimprovementsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


    }
}
