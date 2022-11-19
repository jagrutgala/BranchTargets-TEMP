using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using TargetSettingTool.UI.Models.Branch;

namespace TargetSettingTool.UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BranchVm, BranchCreateRequestModel>().ReverseMap();
            CreateMap<BranchVm, BranchUpdateRequestModel>().ReverseMap();
            CreateMap<BranchVm, BranchDeleteRequestModel>().ReverseMap();
        }
    }
}
