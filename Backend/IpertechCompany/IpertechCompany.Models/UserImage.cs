using System;

namespace IpertechCompany.Models
{
    public class UserImage
    {
        public Guid UserId { get; set; }
        public string ImageLocation { get; set; }

        public UserImage()
        {

        }

        public UserImage(Guid userId, string imageLocation = null)
        {
            UserId = userId.Equals(Guid.Empty) ? Guid.NewGuid() : userId;
            ImageLocation = imageLocation;
        }

        public override string ToString()
        {
            return UserId + ", " + ImageLocation;
        }

        public bool IsValid()
        {

            if (!(!UserId.Equals(Guid.Empty) && !ImageLocation.Equals(null)))
            {
                return false;
            }

            return true;
        }
    }
}
