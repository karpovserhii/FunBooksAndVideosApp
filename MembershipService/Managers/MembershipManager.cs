using DataAccess.Implementations;
using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipService.Managers
{
	public class MembershipManager
	{
		MembershipRepository _repo;
		public MembershipManager(MembershipRepository repository)
		{
			_repo = repository;
		}

		public async Task<IEnumerable<MembershipStatus>> GetMembershipStatuses() => await _repo.GetAllAsync();

		public Task<MembershipStatus> GetMembershipById(int id)
        {
            return _repo.GetByIdAsync(id);
        }
        public MembershipStatus GetMembershipForCustomer(int customerId)
		{
            return _repo.GetStatusForCustomer(customerId);
		}

        public async Task AddOrUpdateMembershipForCustomerAsync(MembershipStatus status)
        {
           if(status.Id == 0)
			{
				_repo.Add(status);
			}
			else
			{
				_repo.Update(status);
			}
		   await _repo.SaveChangesAsync();
        }

        public void AddOrUpdateMembershipForCustomer(MembershipStatus status)
        {
            if (status.Id == 0)
            {
                _repo.Add(status);
            }
            else
            {
                _repo.Update(status);
            }
            _repo.SaveChanges();
        }

        public void UpdateStatus(int customerId, Purchase purchase)
		{
			_repo.UpdateMemebershipStatus(customerId, purchase);
			_repo.SaveChanges();
		}
	}
}
