using AutoMapper;
using BackEndApi.Repositories.Base;

public class BaseService<TEntity, TDto>
    : IBaseService<TDto>
    where TEntity : class
{
    protected readonly IBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseService(
        IBaseRepository<TEntity> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TDto>> GetAll()
    {
        var entities = await _repository.GetAll();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<TDto> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> Create(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.Set(entity);

        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> Update(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.Update(entity);

        return dto;
    }

    public async Task<bool> Delete(int id)
    {
        return await _repository.DeleteById(id);
    }
}