﻿using AutoMapper;

namespace OnlineStore.API.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
