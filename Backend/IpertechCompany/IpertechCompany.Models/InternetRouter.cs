﻿using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using IpertechCompany.Models.Validation;

namespace IpertechCompany.Models
{
    public class InternetRouter : IValidation
    {
        public Guid InternetRouterId { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }

        public InternetRouter()
        {

        }

        public InternetRouter(Guid internetRouterId, string name = null, string imageLocation = null)
        {
            InternetRouterId = internetRouterId.Equals(Guid.Empty) ? Guid.NewGuid() : internetRouterId;
            Name = name;
            ImageLocation = imageLocation;
        }

        public override string ToString()
        {
            return InternetRouterId + ", " + Name + ", " + ImageLocation;
        }

        public bool IsValid()
        {
            if (!(!InternetRouterId.Equals(null) && Name != null))
            {
                return false;
            }

            return true;
        }
    }
}
