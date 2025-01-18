namespace Domain.Interface;

public interface IValidator<T>
{
    public void Validate(T entity);
}