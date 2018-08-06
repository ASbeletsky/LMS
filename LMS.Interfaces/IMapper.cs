namespace LMS.Interfaces
{
    public interface IMapper
    {
        TDest Map<TSrc, TDest>(TSrc src);
        void Map<TSrc, TDest>(TSrc src, TDest dest);
    }
}
