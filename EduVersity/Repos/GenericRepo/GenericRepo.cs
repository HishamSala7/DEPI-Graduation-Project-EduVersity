using EduVersity.Models.AppContext;

namespace EduVersity.Repos.GenericRepo
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly WebAppContext _Context;

        public GenericRepo(WebAppContext webAppContext)
        {
            _Context = webAppContext;
        }
        public List<TEntity> GetAll()
        {
            return _Context.Set<TEntity>().ToList();
        }

        public TEntity? GetById(int Id)
        {
            return _Context.Set<TEntity>().Find(Id);
        }
        public void Add(TEntity model)
        {
            _Context.Set<TEntity>().Add(model);
        }

        public void Update(TEntity model)
        {

        }

        public void Delete(int Id)
        {
            TEntity? model = GetById(Id);

            if (model != null)
                _Context.Set<TEntity>().Remove(model);
        }
        public void SaveChanges()
        {
            _Context.SaveChanges();
        }


    }
}
