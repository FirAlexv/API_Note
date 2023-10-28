using API_Note.Models;
using NuGet.Configuration;

namespace API_Note.Imterface
{
    public interface IOwnerRep
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int id);

        bool OwnerExists(int id);

        bool CreateOwner(Owner newOwner); 

        bool UpdateOwner(Owner updateOwner);

        bool Save();
    }
}
