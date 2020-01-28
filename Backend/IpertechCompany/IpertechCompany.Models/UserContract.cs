﻿using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class UserContract : IValidation
    {
        public Guid UserContractId { get; set; }
        public Guid PacketCombinationId { get; set; }
        public int ContractDurationMonths { get; set; }

        public UserContract()
        {
            UserContractId = Guid.NewGuid();
        }

        public UserContract(Guid userContractId, Guid packetCombinationId, int contractDurationMonths)
        {
            UserContractId = userContractId;
            PacketCombinationId = packetCombinationId;
            ContractDurationMonths = contractDurationMonths;
        }

        public override string ToString()
        {
            return UserContractId + ", " + PacketCombinationId + ", " + ContractDurationMonths;
        }

        public bool IsValid()
        {
            if (!(!UserContractId.Equals(null) && !PacketCombinationId.Equals(null) && ContractDurationMonths != 0))
            {
                return false;
            }

            return true;
        }
    }
}
