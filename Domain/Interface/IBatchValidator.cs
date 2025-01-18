namespace Domain.Interface;

public interface IBatchValidator<T>
{
    public void Validate(List<T> entity);
}