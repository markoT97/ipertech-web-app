using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class Bill : IValidation
    {
        public Guid BillId { get; set; }
        public Guid UserContractId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CallNum { get; set; }
        public string AccOfRecipient { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public Bill()
        {
            BillId = Guid.NewGuid();
        }

        public Bill(Guid billId, Guid userContractId, DateTime startDate, DateTime endDate, string callNum, string accOfRecipient, bool isPaid, decimal price, string currency)
        {
            BillId = billId;
            UserContractId = userContractId;
            StartDate = startDate;
            EndDate = endDate;
            CallNum = callNum;
            AccOfRecipient = accOfRecipient;
            IsPaid = isPaid;
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            return BillId + ", " + UserContractId + ", " + StartDate + ", " + EndDate + ", " + CallNum + ", " + AccOfRecipient + ", " + IsPaid + ", " + Price + ", " + Currency;
        }

        public bool IsValid()
        {
            if (!(!BillId.Equals(null) && !UserContractId.Equals(null) && !StartDate.Equals(null) &&
                  !EndDate.Equals(null) && CallNum != null && AccOfRecipient != null && Price != 0 && Currency != null))
            {
                return false;
            }

            return true;
        }
    }
}
