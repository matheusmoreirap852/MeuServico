using AutoMapper;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Core.Entities;

public class DespesaGeralService
    : BaseService<DespesaGeral, DespesaGeralDto>, IDespesaGeralService
{
    private readonly IDespesaGeralRepository _repository;
    private readonly IMapper _mapper;

    public DespesaGeralService(
        IDespesaGeralRepository repository,
        IMapper mapper
    ) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DespesaGeralDto>> GetAllWithCarro()
    {
        var despesas = await _repository.GetAllWithCarro();

        return _mapper.Map<IEnumerable<DespesaGeralDto>>(despesas);
    }

    public async Task<IEnumerable<DespesaGeralDto>> GetByCarroId(int carroId)
    {
        var despesas = await _repository.GetByCarroId(carroId);
        return _mapper.Map<IEnumerable<DespesaGeralDto>>(despesas);
    }

    public async Task<IEnumerable<DespesaGeralDto>> GetByPeriodo(DateTime inicio, DateTime fim)
    {
        var despesas = await _repository.GetByPeriodo(inicio, fim);
        return _mapper.Map<IEnumerable<DespesaGeralDto>>(despesas);
    }

    public async Task<decimal> GetTotalPorCarro(int carroId)
    {
        return await _repository.GetTotalPorCarro(carroId);
    }
}