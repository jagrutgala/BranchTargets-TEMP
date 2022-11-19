using AutoMapper;

using TargetSettingTool.Application.Features.Branches.Commands.CreateBranch;
using TargetSettingTool.Application.Features.Branches.Commands.DeleteBranch;
using TargetSettingTool.Application.Features.Branches.Commands.UpdateBranch;
using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Features.Categories.Commands.CreateCategory;
using TargetSettingTool.Application.Features.Categories.Commands.StoredProcedure;
using TargetSettingTool.Application.Features.Categories.Queries.GetCategoriesList;
using TargetSettingTool.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using TargetSettingTool.Application.Features.Events.Commands.CreateEvent;
using TargetSettingTool.Application.Features.Events.Commands.Transaction;
using TargetSettingTool.Application.Features.Events.Commands.UpdateEvent;
using TargetSettingTool.Application.Features.Events.Queries.GetEventDetail;
using TargetSettingTool.Application.Features.Events.Queries.GetEventsExport;
using TargetSettingTool.Application.Features.Events.Queries.GetEventsList;
using TargetSettingTool.Application.Features.Orders.Queries.GetOrdersForMonth;
using TargetSettingTool.Application.Features.Roles.Commands.DeleteRole;
using TargetSettingTool.Application.Features.Roles.Commands.UpdateRole;
using TargetSettingTool.Application.Features.Roles.Queries;
using TargetSettingTool.Application.Features.Rights.Commands.CreateRight;
using TargetSettingTool.Application.Features.Rights.Commands.UpdateRight;
using TargetSettingTool.Application.Features.Rights.Queries.GetAllRights;
using TargetSettingTool.Domain.Entities;
using TargetSettingTool.Application.Features.Roles.Commands.CreateRole;
using TargetSettingTool.Application.Features.Rights.Commands.DeleteRight;
using TargetSettingTool.Application.Features.Users.Commands.CreateUser;
using TargetSettingTool.Application.Features.Users.Commands.UpdateUser;
using TargetSettingTool.Application.Features.Users.Commands.DeleteUser;
using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Features.Rights.Queries.GetRightById;
using TargetSettingTool.Application.Features.Roles.Queries.GetAllRoles;
using TargetSettingTool.Application.Features.Roles.Queries.GetRoleById;
using TargetSettingTool.Application.Features.SideBar.Queries.GetSideBar;
using TargetSettingTool.Application.Features.UserBranches.Commands.CreateUserBranch;
using TargetSettingTool.Application.Features.UserBranches.Commands.UpdateUserBranch;
using TargetSettingTool.Application.Features.UserBranches.Commands.DeleteUserBranch;
using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeById;
using TargetSettingTool.Application.Features.ParameterTypes.Commands.CreateParameterType;
using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetAllParamterTypes;
using TargetSettingTool.Application.Features.ParameterTypes.Commands.DeleteParameterType;
using TargetSettingTool.Application.Features.ParameterTypes.Commands.UpdateParameterType;
using TargetSettingTool.Application.Features.Parameters.Commands.CreateParameter;
using TargetSettingTool.Application.Features.Parameters.Commands.UpdateParameter;
using TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters;
using TargetSettingTool.Application.Features.Parameters.Queries.GetParameterById;
using TargetSettingTool.Application.Models.Auth;
using TargetSettingTool.Application.Features.Parameters.Commands.CreateParameters;
using TargetSettingTool.Application.Features.BranchTargets.Common;
using TargetSettingTool.Application.Features.UserParameters.Common;

namespace TargetSettingTool.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, TransactionCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Role, CreateRoleCommand>().ReverseMap();
            CreateMap<Role, UpdateRoleCommand>().ReverseMap();
            CreateMap<Role, DeleteRoleCommand>().ReverseMap();
            CreateMap<Role, GetAllRolesVm>().ReverseMap();
            CreateMap<Role, GetRoleByIdVm>().ReverseMap();

            CreateMap<Right, GetAllRightsQuery>().ReverseMap();
            CreateMap<Right, GetAllRightsVm>().ReverseMap();
            CreateMap<Right, CreateRightCommand>().ReverseMap();
            CreateMap<Right, DeleteRightCommand>().ReverseMap();
            CreateMap<Right, UpdateRightCommand>().ReverseMap();
            CreateMap<Right, GetRightByIdQuery>().ReverseMap();
            CreateMap<Right, GetRightByIdVm>().ReverseMap();

            CreateMap<SideBarMenu, GetSideBarVm>().ReverseMap();

            CreateMap<ParameterType, CreateParameterTypeCommand>().ReverseMap();
            CreateMap<ParameterType, UpdateParameterTypeCommand>().ReverseMap();
            CreateMap<ParameterType, DeleteParameterTypeCommand>().ReverseMap();
            CreateMap<ParameterType, ParameterTypesDto>().ReverseMap();
            CreateMap<ParameterType, GetParameterTypeByIdVm>().ReverseMap();

            CreateMap<Parameter, CreateParameterCommand>().ReverseMap();
            CreateMap<Parameter, UpdateParameterCommand>().ReverseMap();
            CreateMap<Parameter, ParameterDto>().ReverseMap();
            CreateMap<Parameter, GetParameterByIdVm>().ReverseMap();

            //UserLogin
            CreateMap<User, AuthResponse>().ReverseMap();

            //User Mapping
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, DeleteUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleVm>().ReverseMap();
            CreateMap<Right, RightVm>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, StoredProcedureCommand>();
            CreateMap<Category, StoredProcedureDto>();

            CreateMap<Order, OrdersForMonthDto>();

            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();

            CreateMap<Branch, CreateBranchCommand>().ReverseMap();
            CreateMap<Branch, UpdateBranchCommand>().ReverseMap();
            CreateMap<Branch, DeleteBranchCommand>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();

            CreateMap<UserBranch, CreateUserBranchCommand>().ReverseMap();
            CreateMap<UserBranch, UpdateUserBranchCommand>().ReverseMap();
            CreateMap<UserBranch, DeleteUserBranchCommand>().ReverseMap();
            CreateMap<UserBranch, UserBranchDto>().ReverseMap();
            CreateMap<UserBranch, UserBranchVm>().ReverseMap().ForMember(c => c.User, option => option.Ignore()); ;

            CreateMap<BranchTarget, BranchTargetDto>().ReverseMap();

            CreateMap<UserParameter, UserParameterDto>().ReverseMap();

        }
    }
}
