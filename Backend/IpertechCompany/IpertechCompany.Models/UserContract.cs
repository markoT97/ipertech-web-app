﻿using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

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
            if (!(!UserContractId.Equals(null) && !PacketCombination.Equals(null) && ContractDurationMonths != 0))
            {
                return false;
            }

            return true;
        }
    }
}
