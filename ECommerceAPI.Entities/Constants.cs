namespace ECommerceAPI.Entities
{
    public static class Constants
    {
        public const string NotFound = "No se encontró el registro";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DatePattern = "^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";

        public const string RoleAdministrator = "Administrator";
        public const string RoleCustomer = "Customer";
        public const string RoleMixed = "Customer, Administrator";
    }
}
