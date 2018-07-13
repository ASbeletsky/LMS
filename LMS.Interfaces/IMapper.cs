namespace LMS.Interfaces
{
    public interface IMapper
    {
        TTo Map<TFrom, TTo>(TFrom source);
        void Map<TFrom, TTo>(TFrom source, TTo dest);
    }
}
