namespace ECommerceAPI.Services
{
    public static class ECommerceUtils
    {
        public static int GetTotalPages(int total, int rows)
        {
            var totalPages = total / rows;
            if (total % rows > 0)
                totalPages++;

            return totalPages;
        }
    }
}
