namespace EduVersity.Repos.GenericRepo
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity? GetById(int Id);
        void Add(TEntity model);
        void Update(TEntity model);
        void Delete(int Id);
        void SaveChanges();


    }
}
