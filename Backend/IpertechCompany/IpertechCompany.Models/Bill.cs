using IpertechCompany.Models.Validation;
using System;

namespace IpertechCompany.Models
{
    public class Bill : IValidation
    {
        public Guid BillId { get; set; }
        public UserContract UserContract { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CallNum { get; set; }
        public string AccOfRecipient { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public Bill()
        {
            UserContract = new UserContract();
        }

        public Bill(Guid billId, UserContract userContract = null, string callNum = null, string accOfRecipient = null, bool isPaid = false, decimal price = 0, string currency = null)
        {
            BillId = billId.Equals(Guid.Empty) ? Guid.NewGuid() : billId;
            UserContract = userContract;
            StartDate = DateTime.UtcNow;
            EndDate = StartDate.AddDays(30);
            CallNum = callNum;
            AccOfRecipient = accOfRecipient;
            IsPaid = isPaid;
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            return BillId + ", " + UserContract + ", " + StartDate + ", " + EndDate + ", " + CallNum + ", " + AccOfRecipient + ", " + IsPaid + ", " + Price + ", " + Currency;
        }

        public bool IsValid()
        {
            if (!(!BillId.Equals(Guid.Empty) && !UserContract.Equals(null) && !StartDate.Equals(null) &&
                  !EndDate.Equals(null) && CallNum != null && AccOfRecipient != null && Price != 0 && Currency != null))
            {
                return false;
            }

            return true;
        }
    }
}
