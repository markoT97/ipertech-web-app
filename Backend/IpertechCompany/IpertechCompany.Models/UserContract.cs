using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class UserContract : IValidation
    {
        public Guid UserContractId { get; set; }
        public PacketCombination PacketCombination { get; set; }
        public int ContractDurationMonths { get; set; }

        public UserContract()
        {

        }

        public UserContract(Guid userContractId, PacketCombination packetCombination = null, int contractDurationMonths = 0)
        {
            UserContractId = userContractId.Equals(Guid.Empty) ? Guid.NewGuid() : userContractId;
            PacketCombination = packetCombination;
            ContractDurationMonths = contractDurationMonths;
        }

        public override string ToString()
        {
            return UserContractId + ", " + PacketCombination + ", " + ContractDurationMonths;
        }

        public bool IsValid()
        {
            if (!(!UserContractId.Equals(Guid.Empty) && !PacketCombination.Equals(null) && ContractDurationMonths != 0))
            {
                return false;
            }

            return true;
        }
    }
}
