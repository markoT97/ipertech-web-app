using System;
using System.Collections.Generic;
using System.Text;
using IpertechCompany.Models;

namespace IpertechCompany.IServices
{
    public interface IInternetPacketService
    {
        IEnumerable<InternetPacket> GetAllInternetPackets();
        InternetPacket CreateInternetPacket();
        void UpdateBill(Bill bill);
        bool DeleteBill(Guid billId);
    }
}
