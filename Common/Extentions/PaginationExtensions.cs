namespace MonolitModularLearning.Common.Extentions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ToPaged<T>(this IQueryable<T> query, int page, int size)
        {
            // Zəmanətə alırıq ki, kimsə səhvən mənfi rəqəm göndərə bilməsin
            if (page < 1) page = 1;
            if (size < 1) size = 10;

            int skip = (page - 1) * size;

            return query.Skip(skip).Take(size);
        }
    }
}
