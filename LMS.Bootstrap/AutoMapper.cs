using AutoMapper;

namespace LMS.Bootstrap
{
    public class AutoMapper : Interfaces.IMapper
    {
        private readonly IMapper mapper;
        public AutoMapper()
        {
            var configuration = new MapperConfiguration(config => 
            {
                config.ForAllMaps((map, expresstion) =>
                {
                    foreach (var name in map.GetUnmappedPropertyNames())
                    {
                        expresstion.ForMember(name, member => member.Ignore());
                    }
                });

                config.CreateMissingTypeMaps = true;
            });
            mapper = configuration.CreateMapper();
        }

        public TTo Map<TFrom, TTo>(TFrom source)
        {
            return mapper.Map<TFrom, TTo>(source);
        }

        public void Map<TFrom, TTo>(TFrom source, TTo dest)
        {
            mapper.Map(source, dest);
        }
    }
}
