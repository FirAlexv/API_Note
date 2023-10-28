using API_Note.Data;
using API_Note.Imterface;
using API_Note.Models;

namespace API_Note.Repository
{
    public class OwnerRep : IOwnerRep
    {
        private readonly DataContext _context;

        public OwnerRep(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner newOwner)
        {
            _context.Add(newOwner);

            return Save();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owner.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owner.OrderBy(p => p.Id).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owner.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner updateOwner)
        {
            _context.Update(updateOwner);

            return Save();
        }
    }
}
