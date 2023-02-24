using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Services
{
    public interface IMembershipActivationService
    {
        MembershipStatus ActivateMembershipAsync(Purchase purchase);
        void CompleteTransaction(MembershipStatus membershipStatus);
    }
}
