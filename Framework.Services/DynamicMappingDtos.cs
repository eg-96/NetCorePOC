using System;
using System.Linq;
using AutoMapper;
using Framework.Utilities.Reflection;

namespace Framework.Services
{
    public class DynamicMappingDtos : Profile
    {
        private string AssemblyNameEntities { get; set; }
        private const string DTOEntitiesSufix = "DTO";
        private static string GetEntitiesAssembleName => AssemblyExplorer.GetAssemblyNamesForSolution().FirstOrDefault(a => a.Contains(".Entities"));

        public DynamicMappingDtos()
        {
            AssemblyNameEntities = GetEntitiesAssembleName;

            if (!string.IsNullOrEmpty(AssemblyNameEntities))
                MapEntitiesToViewModels();
            else
                throw new Exception("Library that contains DTO's should be has at the end of the name: '.Entities'");
        }

        private string GetDataEntityNameForModel(string modelEntityName)
        {
            var indexSufix = modelEntityName.ToLower().IndexOf(DTOEntitiesSufix.ToLower(), StringComparison.Ordinal);
            var dataEntity = indexSufix >= 0 ? modelEntityName.ToLower().Substring(0, indexSufix) : string.Empty;
            return dataEntity;
        }

        private void MapEntitiesToViewModels()
        {
            var dataEntities = AssemblyExplorer.GetClassesInAssemblyNameNotContains(AssemblyNameEntities, DTOEntitiesSufix);
            var viewModels = AssemblyExplorer.GetClassesInAssemblyNameContains(AssemblyNameEntities, DTOEntitiesSufix);

            foreach (var curDataEntity in dataEntities)
            {
                // Principal ViewModel, should be has the same name as the data entity + Model at the end
                var curViewModel = viewModels.FirstOrDefault(c => GetDataEntityNameForModel(c.Name).Equals(curDataEntity.Name.ToLower()));

                if (curViewModel != null)
                {
                    CreateMap(curDataEntity, curViewModel);

                    if (curViewModel.HasProperty("Id"))
                        CreateMap(curViewModel, curDataEntity).ForMember("Id", opt => opt.Ignore());
                    else
                        CreateMap(curViewModel, curDataEntity);

                    // Looking for ViewModels that inherint from principal ViewModel (curViewModel) for mapping to:
                    var ancestorsViewModel = AssemblyExplorer.GetClassesInAssemblyAncestorsOfType(AssemblyNameEntities, curViewModel);

                    ancestorsViewModel.ForEach(ancestorDto => {
                        if (ancestorDto.IsAbstract)
                            return;

                        CreateMap(curDataEntity, ancestorDto);

                        if (ancestorDto.HasProperty("Id"))
                            CreateMap(ancestorDto, curDataEntity).ForMember("Id", opt => opt.Ignore());
                        else
                            CreateMap(ancestorDto, curDataEntity);
                    });
                }
            }
        }
    }
}
