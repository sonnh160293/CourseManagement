using Repository.IRepository;

namespace Repository.Repository
{
    public class TermRepository : ITermRepository
    {
        public TermRepository()
        {

        }
        public List<int> GetTerms()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        }
    }
}
