using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public TypeRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<TypeGetDTO> GetTypes()
        {
            TypeDAO typeDAO = new TypeDAO(_context);
            return _mapper.Map<List<TypeGetDTO>>(typeDAO.GetTypes());
        }
    }
}
