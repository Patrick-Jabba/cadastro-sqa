namespace cadastroSqa.Interfaces
{
    public interface IBase<in T, out A>
    {
        A Create(T obj);
        IEnumerable<A> GetAll();

        A GetById(int id);

        bool Remove(int id);
    }
}
