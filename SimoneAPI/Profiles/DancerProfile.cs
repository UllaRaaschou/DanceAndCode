using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Tobe.Features;
using SimoneAPI.Tobe.Features.Dancer;
using SimoneAPI.Tobe.Features.Dancers;
using static SimoneAPI.Tobe.Features.Dancer.AddTeamToDancersListOfTeams;
using static SimoneAPI.Tobe.Features.Dancer.PostDancer;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;
using static SimoneAPI.Tobe.Features.Dancer.UpdateDancer;
using DancerDataModel = SimoneAPI.DataModels.DancerDataModel;



namespace SimoneAPI.Profiles
{
    public class DancerProfile: Profile
    {
        public DancerProfile()
        {
            CreateMap<PostDancer.PostDancerDto, DancerDataModel>()
                .ForMember(dest => dest.DancerId, opt => opt.Ignore())
                .ForMember(dest => dest.TeamDancerRelations, opt => opt.Ignore());


            //CreateMap<DancerDataModel, SearchDancerFromNameOnly.TeamResponceDto>()
            //    .ForMember(dest => dest.Teams, opt =>
            //    opt.MapFrom(src => src.TeamDancerRelations != null
            //    ? src.TeamDancerRelations.Select(tdr
            //    => new SearchDancerFromNameOnly.TeamDto
            //    {
            //        TeamId = tdr.TeamDataModel.TeamId,
            //        Number = tdr.TeamDataModel.Number.ToString(),
            //        Name = tdr.TeamDataModel.Name,
            //        SceduledTime = tdr.TeamDataModel.SceduledTime
            //    })
            //    : Enumerable.Empty<SearchDancerFromNameOnly.TeamDto>()));



            CreateMap<SearchForDancerByName.DancerDataModel, SearchForDancerByName.SearchDancerResponceDto>()
                .ForMember(dest => dest.Teams, opt =>
                opt.MapFrom(src => src.TeamDancerRelations != null
                ? src.TeamDancerRelations.Select(tdr
                => new SearchForDancerByName.TeamDataDto
                {
                    TeamId = tdr.TeamDataModel.TeamId,
                    Number = tdr.TeamDataModel.Number,
                    Name = tdr.TeamDataModel.Name
                   
                })               
                : Enumerable.Empty<SearchForDancerByName.TeamDataDto>()));

            CreateMap<UpdateDancer.UpdateDancerDto, DancerDataModel>()
                .ForMember(dest => dest.TeamDancerRelations, opt => opt.Ignore())
                .ForMember(dest => dest.DancerId, opt => opt.Ignore());

            CreateMap<DancerDataModel, UpdateDancerResponceDto>();
            //.ForMember(dest => dest.Teams, opt => opt.Ignore());

            //CreateMap<DancerDataModel, DancerDto>()
            //    .ForMember(dest => dest.Teams, opt =>
            //    opt.MapFrom(src => src.TeamDancerRelations != null
            //    ? src.TeamDancerRelations.Select(tdr
            //    => new TeamDataDto
            //    {
            //        TeamId = tdr.TeamDataModel.TeamId,
            //        Number = tdr.TeamDataModel.Number,
            //        Name = tdr.TeamDataModel.Name                   
            //    })
            //    : Enumerable.Empty<TeamDataDto>())); ;

            CreateMap<DancerDataModel, GetDancerResponceDto>()
                .ForMember(dest => dest.Teams, opt =>
                opt.MapFrom(src => src.TeamDancerRelations != null
                ? src.TeamDancerRelations.Select(tdr => tdr.TeamDataModel)
                : Enumerable.Empty<TeamDataModel>()));

            CreateMap<DancerDataModel, AddTeamToDancersListOfTeams.DancerDto>()
                .ForMember(dest => dest.Teams, opt =>
                opt.MapFrom(src => src.TeamDancerRelations != null
                ? src.TeamDancerRelations.Select(tdr => tdr.TeamDataModel.Name)
                : Enumerable.Empty<string>()));

            CreateMap<DancerDataModel, AddTeamToDancersListOfTeams.DancerDto>()
                .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => 
                src.TeamDancerRelations.Select(tr => tr.TeamDataModel)));



            // CreateMap<Dancer, DancerDataModel>();

            CreateMap<DancerDataModel, PostDancerResponseDto>();

            //CreateMap<Dancer, RequestDancerDto>();                

            //CreateMap<Dancer, PostDancerResponseDto>();

            //CreateMap<Dancer, RequestDancerDto>();

            //CreateMap<UpdateDancerDto, DancerDataModel>();

           
            //CreateMap<PostDancerDto, TeamDancerRelations>()
            //    .ForMember(tdr => tdr.
            
            //(CreateMap<PostDancerDto, DancerDataModels>()
            //    .ForMember(ddm => ddm.TeamDancerRelations, opt =>
            //        opt.MapFrom(pdd => pdd.RegisteredTeams.Select()
            
            
            
            //CreateMap<DancerDataModels, RequestDancerDto>();

            //CreateMap<TeamDataModels, GetTeamDto>()
            //.ForMember(dest => dest.EnrolledDancers, opt =>
            //    opt.MapFrom(src =>
            //        src.TeamDancerRelations.Select(tdr => tdr.DancerDataModels)));

            //CreateMap<DancerDataModels, PostResponceDancerDto>();


            //CreateMap<PostDancerDto, DancerDataModels>();

            //CreateMap<PostTeamDto, TeamDancerRelations>()
            //.ForMember(dest => dest.TeamDataModelId, opt => opt.MapFrom(src => src.TeamId))
            //.ForMember(dest => dest.DancerDataModelId, opt => opt.Ignore());



            //CreateMap<PostTeamDto, TeamDataModels>()
            //.ForMember(dest => dest.TeamDancerRelations, opt =>
            //    opt.MapFrom(src =>
            //        src.EnrolledDancers.All)Cre

        }  
    }
}

