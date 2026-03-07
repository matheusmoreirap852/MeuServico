namespace MeuServico.Application.Interfaces;

public interface IBaseService<TEntityDto>
{
    Task<IEnumerable<TEntityDto>> GetAll();
    Task<TEntityDto> GetById(int id);
    Task<TEntityDto> Create(TEntityDto dto);
    Task<TEntityDto> Update(TEntityDto dto);
    Task<bool> Delete(int id);
}