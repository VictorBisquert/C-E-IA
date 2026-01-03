namespace Control_Estadistico_Web.Guards
{
    public static class CompanyOwnershipGuard
    {
        public static void Ensure(Guid entityCompanyId, Guid companyId)
        {
            if (entityCompanyId != companyId)
            {
                throw new InvalidOperationException(
                    "Unauthorized resource access."
                );
            }
        }
    }
}
